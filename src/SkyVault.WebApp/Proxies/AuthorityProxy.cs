using SkyVault.Payloads.RequestPayloads;
using SkyVault.Payloads.ResponsePayloads;
using SkyVault.WebApp.Models;

namespace SkyVault.WebApp.Proxies;

public sealed class AuthorityProxy(HttpClient httpClient)
{
    public SkyResult<WelcomeUserResponse>? GetUserInfo(ValidateUserRequest validateUserRequest)
    {
        //Get user information from API
        var response = httpClient.PostAsJsonAsync("/auth/user", validateUserRequest).Result;

        //At all times a user will exist or be created if NOT!
        //Access is dictated through Azure AD App Registration IAM Access to this application
        response.EnsureSuccessStatusCode();

        var result = response.Content.ReadFromJsonAsync<SkyResult<WelcomeUserResponse>>().Result;

        return result;
    }

    public IEnumerable<SkyVaultMenuItem> GetMenus(string role)
    {
        var menus = new[]
        {
            new SkyVaultMenuItem(MenuName: "Profile Management", MenuUri: "#", MenuIconUri: "~/img/icons/pf_icon.png",
                Role: SkyVaultConfigurationKeys.SkyVaultSuperAdminRoleName),
            new SkyVaultMenuItem(MenuName: "Notifications", MenuUri: "#", MenuIconUri: "~/img/icons/noti_icon.png",
                Role: SkyVaultConfigurationKeys.SkyVaultSuperAdminRoleName,
                Children: new[]
                {
                    new SkyVaultMenuItem(MenuName: "Birthday Greetings", MenuUri: "#",
                        MenuIconUri: "~/img/icons/bd_icon.png", Role: string.Empty),
                    new SkyVaultMenuItem(MenuName: "Passport Expiration", MenuUri: "#",
                        MenuIconUri: "~/img/icons/psprt_icon.png", Role: string.Empty),
                    new SkyVaultMenuItem(MenuName: "VISA Expiration", MenuUri: "#",
                        MenuIconUri: "~/img/icons/visa_icon.png", Role: string.Empty),
                    new SkyVaultMenuItem(MenuName: "Send Notification", MenuUri: "#",
                        MenuIconUri: "~/img/icons/sendnoti_icon.png", Role: string.Empty),
                    new SkyVaultMenuItem(MenuName: "History", MenuUri: "#", MenuIconUri: "~/img/icons/hstry_icon.png",
                        Role: string.Empty)
                }),
            new SkyVaultMenuItem(MenuName: "Transfer Profiles", MenuUri: "#",
                MenuIconUri: "~/img/icons/trnprof_icon.png", Role: SkyVaultConfigurationKeys.SkyVaultSuperAdminRoleName)
        };

        var filteredMenus = menus.Where(menu => menu.Role == role || role == "All");

        return filteredMenus;
    }
}