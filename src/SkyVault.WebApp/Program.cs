using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.IdentityModel.Logging;
using SkyVault.WebApp.Middlewares;
using SkyVault.WebApp.Pages;
using SkyVault.WebApp.Proxies;

const string fallbackBaseUri = "https://localhost/api";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();
builder.Services.AddAntiforgery(options => options.HeaderName = "XSRF-TOKEN");
builder.Services.AddRazorPages();
builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders =
        ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
});
builder.Services.AddAuthentication(azureOptions =>
    {
        azureOptions.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        azureOptions.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
    })
    .AddCookie().AddOpenIdConnect(options =>
    {
        builder.Configuration.Bind("AzureAD", options);
        options.Events = new OpenIdConnectEvents
        {
            OnRedirectToIdentityProvider = context =>
            {
                if (context.HttpContext.User.Identity == null || context.HttpContext.User.Identity.IsAuthenticated)
                    return Task.CompletedTask;

                if (LoginModel.RedirectToSso == true) return Task.CompletedTask;
                
                context.Response.Redirect("/login");
                context.HandleResponse();

                return Task.CompletedTask;
            }
        };
    });
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(20);
    options.Cookie.HttpOnly = true;
    options.Cookie.SameSite = SameSiteMode.Lax;
    options.Cookie.IsEssential = true;
});

builder.Services.AddHttpClient("BaseClient", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["BaseApiUrl"] ?? fallbackBaseUri);
});
builder.Services.AddHttpClient<AuthorityProxy>();
builder.Services.AddHttpClient<CustomerProxy>();

var app = builder.Build();

IdentityModelEventSource.ShowPII = true;

app.UseForwardedHeaders();
app.UseHttpsRedirection();
app.UseDefaultFiles();
app.UseStaticFiles();
app.UseRouting();
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();
app.UseMiddleware<ExceptionMiddleware>();
app.Run();