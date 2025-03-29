using Microsoft.AspNetCore.Mvc;
using SkyVault.Exceptions;
using SkyVault.Payloads.CommonPayloads;
using SkyVault.Payloads.RequestPayloads;
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

                        if (!string.IsNullOrEmpty(request.FileName))
                        {
                            var allowedExtensions = new HashSet<string> { ".jpg", ".jpeg", ".png" };
                            var fileExtension = Path.GetExtension(request.FileName).ToLowerInvariant();

                            if (!allowedExtensions.Contains(fileExtension))
                            {
                                return await DeleteFileAndReturnProblem(storageService, request.FileName, "Invalid file format. Only JPG and PNG are allowed.", "30550615-0004");
                            }
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

                        var messageService = new MessageService(dbContext);
                        var response = await messageService.UpdateMessage(
                            userId.Value, request.MessageType, request.FileName, null);

                        return Results.Created("Successfully Updated", response);
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

        private static async Task<IResult> DeleteFileAndReturnProblem(StorageService storageService, string fileName, string detail, string errorCode)
        {
            await storageService.DeleteFileAsync(fileName, Payloads.CommonPayloads.MessageType.Birthday);
            return Results.Problem(new ValidationProblemDetails().ToValidationProblemDetails(detail, errorCode, _correlationId));
        }
    }
}
