using SkyVault.WebApi.Backend;
using SkyVault.WebApi.Backend.Models;

namespace SkyVault.WebApi.Filters
{
    /**
     * This filter is implemented for the authorization of admin and super admin roles.
     * Those are not included in the access token coming from the Azure AD.
     * This class can be implement cache is performance is a concern.
     */
    public class AdminOnlyFilter : IEndpointFilter
    {
        private readonly SkyvaultContext _dbContext;

        public AdminOnlyFilter(SkyvaultContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async ValueTask<object?> InvokeAsync(
            EndpointFilterInvocationContext context,
            EndpointFilterDelegate next)
        {
            var systemUserData = new SystemUserData(_dbContext);
            var userIdentifier = context.HttpContext.User.Identity?.Name;

            if (string.IsNullOrEmpty(userIdentifier))
            {
                context.HttpContext.Response.StatusCode = 401;
                return Task.CompletedTask;
            }

            var userRole = systemUserData.GetUserRoleByUpn(userIdentifier, null);

            if (!userRole.Succeeded)
            {
                context.HttpContext.Response.StatusCode = 401;
                return Task.CompletedTask;
            }

            if (userRole.Value != "Admin" && userRole.Value != "SuperAdmin")
            {
                context.HttpContext.Response.StatusCode = 403;
                return Task.CompletedTask;
            }

            return await next(context);
        }
    }
}
