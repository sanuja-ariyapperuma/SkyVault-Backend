using Microsoft.AspNetCore.Mvc;
using SkyVault.Exceptions;
using SkyVault.Payloads.CommonPayloads;
using SkyVault.Payloads.RequestPayloads;
using SkyVault.Payloads.ResponsePayloads;
using SkyVault.WebApi.Backend;
using SkyVault.WebApi.Backend.Models;
using SkyVault.WebApi.Helper;

namespace SkyVault.WebApi.Workloads
{
    internal static class MessageWorkload
    {
        private static string _correlationId = string.Empty;

        public async static Task<IResult> GetPreSignedUrl(
            [FromBody] PreSignedUrlRequest request,
            SkyvaultContext dbContext,
                HttpContext context
            )
        {

            StorageService storageService = new StorageService();
            var preSignedUrl = await storageService.GetPreSignedUrlAsync(request.FileTypeEnum, request.MessageTypeEnum);

            return Results.Ok(preSignedUrl);
        }

        /***
         * The file is uploading from the frontend with a pre-signed URL.
         * Since frontend may be compromised, validate file extensions again here is mandetory.
         * On any failure, delete already uploaded file from the storage.
         */
        public async static Task<IResult> UpdateAttachmentFile(
            [FromBody] UpdateBirthdayFileRequest request,
            SkyvaultContext dbContext,
            HttpContext context
        )
        {
            var storageService = new StorageService();

            try
            {
                var systemUserData = new SystemUserData(dbContext);
                var messageService = new MessageService(dbContext);
                var userIdentifier = context.User.Identity?.Name;

                if (!Enum.IsDefined(typeof(MessageType), request.MessageType))
                {
                    return Results.Problem(new ValidationProblemDetails().ToValidationProblemDetails(
                        "Invalid MessageType", "30550615-0004", _correlationId
                    ));
                }

                if (request.MessageType == MessageType.Birthday && string.IsNullOrEmpty(request.FileName))
                {
                    return Results.Problem(new ValidationProblemDetails().ToValidationProblemDetails(
                        "FileName Not Found", "30550615-0003", _correlationId
                    ));
                }

                if (!string.IsNullOrEmpty(request.FileName) && !IsFileExtentionAllowed(request.FileName))
                {
                    return await DeleteFileAndReturnProblem(storageService, request.FileName, "Invalid file format. Only JPG and PNG are allowed.", "30550615-0004");
                }

                if (string.IsNullOrEmpty(userIdentifier))
                {
                    return await DeleteFileAndReturnProblem(storageService, request.FileName, "Unauthorized action", "30550615-0005");
                }

                var userId = systemUserData.GetUserIdByUpn(userIdentifier, _correlationId);

                if (!userId.Succeeded)
                {
                    await storageService.DeleteFileAsync(request.FileName, MessageType.Birthday);
                    return Results.Unauthorized();
                }


                var response = await messageService.UpdateMessage(
                    userId.Value, 
                    request.MessageType, 
                    request.FileName, 
                    null,
                    PreferedCommiunicationMethod.Email);

                return Results.Created("Successfully Updated", "");
            }
            catch (Exception ex)
            {
                ex.LogException(_correlationId);
                await storageService.DeleteFileAsync(request.FileName, Payloads.CommonPayloads.MessageType.Birthday);
                return Results.Problem(new ValidationProblemDetails().ToValidationProblemDetails(
                    "Image did not upload successfully", "30550615-0003", _correlationId
                ));
            }
        }

        public async static Task<IResult> GetMessage(
            [FromQuery] string messageType,
            SkyvaultContext dbContext
        )
        {
            if (!Enum.TryParse<MessageType>(messageType, true, out var parsedMessageType) || !Enum.IsDefined(typeof(MessageType), parsedMessageType))
            {
                return Results.Problem(new ValidationProblemDetails().ToValidationProblemDetails(
                    "Invalid MessageType", "30550615-0006", _correlationId
                ));
            }

            MessageService messageService = new MessageService(dbContext);
            var response = await messageService.GetMessage(parsedMessageType);

            if (!response.Succeeded)
            {
                return Results.Problem(new ValidationProblemDetails().ToValidationProblemDetails(
                    response.Message, "30550615-0007", _correlationId
                ));
            }

            return Results.Ok(response.Value);
        }

        public async static Task<IResult> SaveAndSendPromotion(
            [FromBody] BroadcastMessageRequest request,
            SkyvaultContext dbContext,
            HttpContext context,
            IApiClient apiClient)
        {
            var systemUserData = new SystemUserData(dbContext);
            var messageService = new MessageService(dbContext);
            var storageService = new StorageService();
            var userIdentifier = context.User.Identity?.Name;

            try
            {
                var validationResult = await ValidateRequest(request, storageService);
                if (validationResult is not null)
                    return validationResult;

                var parsedBroadcastType = Enum.Parse<PreferedCommiunicationMethod>(request.BroadcastType, true);
                var messageType = request.IsEmergency ? MessageType.Emergency : MessageType.Promotion;
                var userId = systemUserData.GetUserIdByUpn(userIdentifier, _correlationId);

                if (!userId.Succeeded)
                {
                    return await HandleErrorWithFileCleanup(storageService, request.FileName, "Unauthorized action", "30550615-0005");
                }

                var content = $"{request.Subject} | {request.Content}";
                var response = await messageService.UpdateMessage(
                    userId.Value, messageType, request.FileName, content, parsedBroadcastType);

                if (!response.Succeeded)
                {
                    return Results.Problem(new ProblemDetails
                    {
                        Title = "An error occurred",
                        Detail = response.Message,
                        Status = 500,
                        Extensions = { ["errorCode"] = response.ErrorCode, ["correlationId"] = _correlationId }
                    });
                }

                try
                {
                    await apiClient.PostBroardcastMessageAsync(response.Value, parsedBroadcastType.ToString());
                }
                catch (Exception ex)
                {
                    await messageService.DeleteNotificationTemplateById(response.Value);
                    ex.LogException(_correlationId);
                    return await HandleErrorWithFileCleanup(storageService, request.FileName,
                        "An error occurred while processing the request.", "30550615-0010");
                }

                return Results.Ok("Broadcast Initiated Successfully");
            }
            catch (Exception ex)
            {
                ex.LogException(_correlationId);
                return await HandleErrorWithFileCleanup(storageService, request.FileName,
                    "An error occurred while processing the request.", "30550615-0010");
            }
        }

        public async static Task<IResult> UpdatePassportExpiryMessage(
            [FromBody] PassportVisaExpireMessageRequest request,
            SkyvaultContext dbContext,
            HttpContext context
        )
        {
            try
            {
                var systemUserData = new SystemUserData(dbContext);
                var messageService = new MessageService(dbContext);
                var userIdentifier = context.User.Identity?.Name;

                // Use the updated validation method
                var validationResult = ValidatePassportAndVisaContent(request.Content, isVisa: false);
                if (validationResult is not null)
                {
                    return validationResult;
                }

                var userId = systemUserData.GetUserIdByUpn(userIdentifier, _correlationId);
                var messageType = MessageType.PassportExpiry;

                if (!userId.Succeeded)
                {
                    return Results.Unauthorized();
                }

                var response = await messageService.UpdateMessage(
                    userId.Value, messageType, null, request.Content, PreferedCommiunicationMethod.Email);

                // Check if the response indicates success
                if (!response.Succeeded)
                {
                    return Results.Problem(new ValidationProblemDetails().ToValidationProblemDetails(
                        response.Message, response.ErrorCode ?? "30550615-0016", _correlationId
                    ));
                }

                return Results.Created("Successfully Updated", "");
            }
            catch (Exception ex)
            {
                ex.LogException(_correlationId);
                return Results.Problem(new ValidationProblemDetails().ToValidationProblemDetails(
                    "Passport Message Save Not Succeeded", "30550615-0003", _correlationId
                ));
            }
        }

        public async static Task<IResult> UpdateVisaExpiryMessage(
            [FromBody] PassportVisaExpireMessageRequest request,
            SkyvaultContext dbContext,
            HttpContext context
        )
        {
            try
            {
                var systemUserData = new SystemUserData(dbContext);
                var messageService = new MessageService(dbContext);
                var userIdentifier = context.User.Identity?.Name;

                // Use the updated validation method
                var validationResult = ValidatePassportAndVisaContent(request.Content, isVisa: true);
                if (validationResult is not null)
                {
                    return validationResult;
                }

                var userId = systemUserData.GetUserIdByUpn(userIdentifier, _correlationId);
                var messageType = MessageType.VisaExpiry;

                if (!userId.Succeeded)
                {
                    return Results.Unauthorized();
                }

                var response = await messageService.UpdateMessage(
                    userId.Value, messageType, null, request.Content, PreferedCommiunicationMethod.Email);

                // Check if the response indicates success
                if (!response.Succeeded)
                {
                    return Results.Problem(new ValidationProblemDetails().ToValidationProblemDetails(
                        response.Message, response.ErrorCode ?? "30550615-0016", _correlationId
                    ));
                }

                return Results.Created("Successfully Updated", "");
            }
            catch (Exception ex)
            {
                ex.LogException(_correlationId);
                return Results.Problem(new ValidationProblemDetails().ToValidationProblemDetails(
                    "Visa Message Save Not Succeeded", "30550615-0003", _correlationId
                ));
            }
        }

        public async static Task<IResult> GetAllMessages(
            SkyvaultContext dbContext, 
            HttpContext context,
            [FromQuery] int page = 1) 
        {
            var messageService = new MessageService(dbContext);

            var response = await messageService.GetAllMessages(page);

            if (!response.Succeeded)
            {
                return Results.Problem(new ValidationProblemDetails().ToValidationProblemDetails(
                    "Error retriving messages", "" ?? "30550615-0016", _correlationId
                ));
            }

            return Results.Ok(response.Value);
        }

        public async static Task<IResult> GetMessageById(
            SkyvaultContext dbContext,
            HttpContext context,
            [FromQuery] int messageId)
        {
            if (messageId <= 0) 
            {
                return Results.Problem(new ValidationProblemDetails().ToValidationProblemDetails(
                    "Invalid messageId", "30550615-0015", _correlationId
                ));
            }

            var messageService = new MessageService(dbContext);

            var response = await messageService.GetNotificationTemplateById(messageId);

            if (!response.Succeeded)
            {
                return Results.Problem(new ValidationProblemDetails().ToValidationProblemDetails(
                    "Error retriving notification", "" ?? "30550615-0016", _correlationId
                ));
            }

            var responseValue = response.Value!;
            var subjectAndContent = responseValue.Content?.Split('|', StringSplitOptions.RemoveEmptyEntries);
            
            var responseObject = new MessageHistoryDetailedResponse(
                responseValue.Id,
                subjectAndContent != null ? subjectAndContent[0] : "",
                subjectAndContent != null ? subjectAndContent[1] : "" ,
                responseValue.NotificationTypeNavigation.TypeName,
                GetFileUrl(responseValue.File, responseValue.NotificationType),
                responseValue.Active,
                ConvertToSriLankanTime(responseValue.CreatedAt),
                FormatUsersNameWithEmail(responseValue.CreatedByUser),
                ConvertToSriLankanTime(responseValue.UpdatedAt),
                FormatUsersNameWithEmail(responseValue.UpdatedByUser),
                responseValue.CommunicationMethod.CommTitle
            );

            return Results.Ok(responseObject);
        }

        #region Private Methods

        private static async Task<IResult> DeleteFileAndReturnProblem(StorageService storageService, string fileName, string detail, string errorCode)
        {
            await storageService.DeleteFileAsync(fileName, MessageType.Birthday);
            return Results.Problem(new ValidationProblemDetails().ToValidationProblemDetails(detail, errorCode, _correlationId));
        }

        private static bool IsFileExtentionAllowed(string fileName)
        {
            var allowedExtensions = new HashSet<string> { ".jpg", ".jpeg", ".png" };
            var fileExtension = Path.GetExtension(fileName).ToLowerInvariant();

            return allowedExtensions.Contains(fileExtension);
        }

        private static async Task<IResult?> ValidateRequest(BroadcastMessageRequest request, StorageService storageService)
        {
            if (string.IsNullOrWhiteSpace(request.Subject))
                return await HandleErrorWithFileCleanup(storageService, request.FileName, "Subject cannot be null or empty.", "30550615-0008");

            if (string.IsNullOrWhiteSpace(request.Content))
                return await HandleErrorWithFileCleanup(storageService, request.FileName, "Content cannot be null or empty.", "30550615-0009");

            if (!Enum.TryParse<PreferedCommiunicationMethod>(request.BroadcastType, true, out var parsedBroadcastType) ||
                (parsedBroadcastType != PreferedCommiunicationMethod.WhatsApp && parsedBroadcastType != PreferedCommiunicationMethod.Email))
            {
                return await HandleErrorWithFileCleanup(storageService, request.FileName,
                    "Invalid BroadcastType. Only WhatsApp and Email are allowed.", "30550615-0011");
            }

            if (!string.IsNullOrEmpty(request.FileName) && !IsFileExtentionAllowed(request.FileName))
                return await HandleErrorWithFileCleanup(storageService, request.FileName,
                    "Invalid file format. Only JPG and PNG are allowed.", "30550615-00012");

            return null;
        }

        private static async Task<IResult> HandleErrorWithFileCleanup(StorageService storageService, string? fileName, string message, string errorCode)
        {
            if (!string.IsNullOrEmpty(fileName))
                return await DeleteFileAndReturnProblem(storageService, fileName, message, errorCode);

            return Results.Problem(new ValidationProblemDetails().ToValidationProblemDetails(message, errorCode, _correlationId));
        }

        private static IResult? ValidatePassportAndVisaContent(string content, bool isVisa)
        {
            if (string.IsNullOrWhiteSpace(content))
            {
                return Results.Problem(new ValidationProblemDetails().ToValidationProblemDetails(
                    "Content cannot be null or empty.", "30550615-0009", _correlationId
                ));
            }

            var pipeCount = content.Count(c => c == '|');
            if (pipeCount != 1)
            {
                return Results.Problem(new ValidationProblemDetails().ToValidationProblemDetails(
                    "Content must contain exactly one pipe ('|') character.", "30550615-0018", _correlationId
                ));
            }

            var contentParts = content.Split('|', 2);
            if (contentParts.Length < 2 || string.IsNullOrWhiteSpace(contentParts[1]))
            {
                return Results.Problem(new ValidationProblemDetails().ToValidationProblemDetails(
                    "Email body cannot be null or empty.", "30550615-0014", _correlationId
                ));
            }

            if (contentParts[0].Length > 60)
            {
                return Results.Problem(new ValidationProblemDetails().ToValidationProblemDetails(
                    "Email subject cannot exceed 60 characters.", "30550615-0013", _correlationId
                ));
            }

            if (contentParts[1].Length > 1000)
            {
                return Results.Problem(new ValidationProblemDetails().ToValidationProblemDetails(
                    "Email body cannot exceed 1000 characters.", "30550615-0015", _correlationId
                ));
            }

            // Apply "country_name" validation only for Visa
            if (isVisa && !contentParts[1].Contains("country_name", StringComparison.OrdinalIgnoreCase))
            {
                return Results.Problem(new ValidationProblemDetails().ToValidationProblemDetails(
                    "Email body must contain the string 'country_name'.", "30550615-0017", _correlationId
                ));
            }

            return null;
        }

        private static string FormatUsersNameWithEmail(SystemUser? user) 
        {
            if (user == null) 
                return string.Empty;

            return $"{user.FirstName} {user.LastName} ({user.SamProfileId})";
        }

        private static string GetFileUrl(string? fileName, int messageTypeId) 
        {
            if (string.IsNullOrEmpty(fileName))
                return string.Empty;

            var storageService = new StorageService();

            switch (messageTypeId)
            {
                case 1:
                    return storageService.GetFileUrl(fileName, MessageType.Birthday);
                case 2:
                    return storageService.GetFileUrl(fileName, MessageType.PassportExpiry);
                case 3:
                    return storageService.GetFileUrl(fileName, MessageType.VisaExpiry);
                case 4:
                    return storageService.GetFileUrl(fileName, MessageType.Promotion);
                case 5:
                    return storageService.GetFileUrl(fileName, MessageType.Emergency);
                default:
                    return string.Empty;
            }
        }

        private static string ConvertToSriLankanTime(DateTime? time) 
        {
            if (time.HasValue)
            {
                var sriLankaTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Sri Lanka Standard Time");
                var sriLankaDate = TimeZoneInfo.ConvertTimeFromUtc(time.Value, sriLankaTimeZone);
                var formatted = sriLankaDate.ToString("yyyy-MM-dd hh:mm tt");

                return formatted;
            }
            else 
            {
                return "";
            }

            
        }


        #endregion
    }
}
