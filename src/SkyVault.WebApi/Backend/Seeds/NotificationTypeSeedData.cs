using SkyVault.WebApi.Backend.Models;

namespace SkyVault.WebApi.Backend.Seeds
{
    public sealed class NotificationTypeSeedData
    {
        public static readonly List<NotificationType> notifications =
        [
            new NotificationType { Id = 1, TypeName = "Birthday"},
            new NotificationType { Id = 2, TypeName = "PassportExpiry"},
            new NotificationType { Id = 3, TypeName = "VisaExpiry"},
            new NotificationType { Id = 4, TypeName = "Custom"},
        ];
    }
}
