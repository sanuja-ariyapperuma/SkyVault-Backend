using Microsoft.EntityFrameworkCore;
using SkyVault.Payloads.CommonPayloads;
using SkyVault.Payloads.ResponsePayloads;
using SkyVault.WebApi.Backend.Models;

namespace SkyVault.WebApi.Backend
{
    public class MessageService
    {
        private readonly SkyvaultContext _db;

        public MessageService(SkyvaultContext db)
        {
            _db = db;
        }

        public async Task<SkyResult<string>> UpdateMessage(int userId, MessageType messageType, string? fileName, string? content)
        {
            try
            {
                // Deactivate previous templates of the same MessageType
                var previousTemplates = await _db.NotificationTemplates
                    .Where(nt => nt.NotificationType == (int)messageType && nt.Active)
                    .ToListAsync();

                previousTemplates.ForEach(template =>
                {
                    template.Active = false;
                    template.UpdatedAt = DateTime.UtcNow;
                    template.UpdatedBy = userId;
                });

                // Create new template
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

                return new SkyResult<string>().SucceededWithValue("Message updated successfully.");
            }
            catch (Exception ex)
            {
                return new SkyResult<string>().Fail(ex.Message, "500", null);
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
    }
}
