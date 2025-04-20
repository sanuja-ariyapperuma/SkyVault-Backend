using Microsoft.EntityFrameworkCore;
using SkyVault.Exceptions;
using SkyVault.Payloads.CommonPayloads;
using SkyVault.Payloads.ResponsePayloads;
using SkyVault.WebApi.Backend.Models;
using SkyVault.WebApi.Helper;

namespace SkyVault.WebApi.Backend
{
    public class MessageService
    {
        private readonly SkyvaultContext _db;

        public MessageService(SkyvaultContext db)
        {
            _db = db;
        }

        public async Task<SkyResult<int>> UpdateMessage(
            int userId,
            MessageType messageType,
            string? fileName,
            string? content,
            PreferedCommiunicationMethod? commiunicationMethod)
        {
            try
            {
                // Only deactivate existing templates for non-scheduled messages
                if (messageType != MessageType.Promotion && messageType != MessageType.Emergency)
                {
                    var previousTemplates = await _db.NotificationTemplates
                        .Where(nt => nt.NotificationType == (int)messageType && nt.Active)
                        .ToListAsync();

                    foreach (var template in previousTemplates)
                    {
                        template.Active = false;
                        template.UpdatedAt = DateTime.UtcNow;
                        template.UpdatedBy = userId;
                    }
                }

                var newTemplate = new NotificationTemplate
                {
                    Content = content,
                    File = fileName,
                    NotificationType = (int)messageType,
                    CreatedBy = userId,
                    CreatedAt = DateTime.UtcNow,
                    Active = true
                };

                _db.NotificationTemplates.Add(newTemplate);
                await _db.SaveChangesAsync();

                return new SkyResult<int>().SucceededWithValue(newTemplate.Id);
            }
            catch (Exception ex)
            {
                ex.LogException("");
                return new SkyResult<int>().Fail(ex.Message, "500", null);
            }
        }

        public async Task<SkyResult<string>> DeleteNotificationTemplateById(int templateId)
        {
            try
            {
                var template = await _db.NotificationTemplates.FindAsync(templateId);
                if (template == null)
                {
                    return new SkyResult<string>().Fail("NotificationTemplate not found.", "404", null);
                }

                _db.NotificationTemplates.Remove(template);
                await _db.SaveChangesAsync();

                return new SkyResult<string>().SucceededWithValue("Deleted");
            }
            catch (Exception ex)
            {
                ex.LogException(ex.Message);
                return new SkyResult<string>().Fail("Something went wrong when deleting template", "500", null);
            }
        }

        /***
         * This Function should return date in the response which match to the Sri Lankan Standard Time Zone
         */
        public async Task<SkyResult<Message>> GetMessage(MessageType messageType)
        {
            var storageService = new StorageService();

            var template = await _db.NotificationTemplates
            .Where(nt => nt.NotificationType == (int)messageType && nt.Active)
            .Include(nt => nt.CreatedByUser)
            .FirstOrDefaultAsync();

            if (template == null)
            {
                return new SkyResult<Message>().Fail("No active message found for the given message type.", "404", null);
            }

            // Convert UTC time to Colombo, Sri Lanka time
            var colomboTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Sri Lanka Standard Time");
            var createdAtColombo = TimeZoneInfo.ConvertTimeFromUtc(template.CreatedAt, colomboTimeZone);

            var message = new Message
            {
                Content = template.Content,
                FileUrl = storageService.GetFileUrl(template.File, MessageType.Birthday),
                CreatedAt = createdAtColombo,
                CreatedBy = $"{template.CreatedByUser.FirstName} {template.CreatedByUser.LastName} ({template.CreatedByUser.SamProfileId})"
            };

            return new SkyResult<Message>().SucceededWithValue(message);
        }

        public async Task<SkyResult<AllMessagesPaginatedResponse>> GetAllMessages(int page) 
        {
            // Get the total count of records (without pagination)
            var totalCount = await _db.NotificationTemplates
                .OrderByDescending(nt => nt.Active)
                .ThenBy(nt => nt.NotificationType == 1 ? 0
                      : nt.NotificationType == 2 ? 1
                      : nt.NotificationType == 3 ? 2
                      : nt.NotificationType == 5 ? 3
                      : nt.NotificationType == 4 ? 4
                      : 5)
                .CountAsync();

            // Calculate the total number of pages
            int pageSize = 10;
            int totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            // Fetch the paginated data
            var allMessages = await _db.NotificationTemplates
                .OrderByDescending(nt => nt.Active)
                .ThenBy(nt => nt.NotificationType == 1 ? 0
                      : nt.NotificationType == 2 ? 1
                      : nt.NotificationType == 3 ? 2
                      : nt.NotificationType == 5 ? 3
                      : nt.NotificationType == 4 ? 4
                      : 5)
                .ThenByDescending(nt => nt.CreatedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            // You can now return the data and include the page info
            var response = new AllMessagesPaginatedResponse(
                CurrentPage: page,
                TotalPages: totalPages,
                TotalMessages: totalCount,
                Messages: [.. allMessages.Select(nt => new MessageSummary(
                    nt.Id,
                    nt.NotificationType,
                    nt.Active,
                    Helpers.SummarizeText(nt.Content) 
                ))]
            );

            return new SkyResult<AllMessagesPaginatedResponse>().SucceededWithValue(response);
        }

        public async Task<SkyResult<NotificationTemplate>> GetNotificationTemplateById(int templateId) 
        {
            var notificationTemplate = await _db.NotificationTemplates
                .AsNoTracking()
                .Include(nt => nt.CreatedByUser)
                .Include(nt => nt.UpdatedByUser)
                .Include(nt => nt.NotificationTypeNavigation)
                .Include(nt => nt.CommunicationMethod)
                .FirstOrDefaultAsync(nt => nt.Id == templateId);

            if (notificationTemplate == null) 
            {
                return new SkyResult<NotificationTemplate>().Fail("NotificationTemplate not found.", "404", null);
            }

            return new SkyResult<NotificationTemplate>().SucceededWithValue(notificationTemplate);
        }
    
    }
}
