using Microsoft.AspNetCore.Authentication.AzureAD.UI;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.IdentityModel.Logging;
using SkyVault.WebApp;
using SkyVault.WebApp.Middlewares;
using SkyVault.WebApp.Pages;
using SkyVault.WebApp.Proxies;
using HttpClientHandler = System.Net.Http.HttpClientHandler;
using Uri = System.Uri;
using WebApplicationBuilder = Microsoft.AspNetCore.Builder.WebApplicationBuilder;

const string fallbackBaseUri = "https://localhost/api";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();
builder.Services.AddAntiforgery(options => options.HeaderName = "XSRF-TOKEN");
builder.Services.AddRouting(options =>
{
    options.LowercaseUrls = true;
    options.LowercaseQueryStrings = true;
});
builder.Services.AddRazorPages();
builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders =
        ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
});
builder.Services.AddAuthentication(sharedOptions =>
{
    sharedOptions.DefaultScheme = AzureADDefaults.AuthenticationScheme;
});
/*builder.Services.AddAuthentication(azureOptions =>
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
    });*/
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(20);
    options.Cookie.HttpOnly = true;
    options.Cookie.SameSite = SameSiteMode.Lax;
    options.Cookie.IsEssential = true;
});
builder.Services.AddTransient<CorrelationIdDelegatingHandler>();

ConfigureHttpClient<AuthorityProxy>(builder);
ConfigureHttpClient<CustomerProxy>(builder);

var app = builder.Build();

//IdentityModelEventSource.ShowPII = true;

app.UseForwardedHeaders();
app.UseHttpsRedirection();
app.UseDefaultFiles();
app.UseStaticFiles();
app.UseRouting();
app.UseSession();
//app.UseAuthentication();
//app.UseAuthorization();
app.MapRazorPages();
app.UseMiddleware<CorrelationIdMiddleware>();
//app.UseMiddleware<ExceptionMiddleware>();
app.Run();
return;

void ConfigureHttpClient<T>(WebApplicationBuilder wab) where T : class
{
    wab.Services.AddHttpClient<T>(client =>
    {
        client.BaseAddress = new Uri(wab.Configuration["BaseApiUrl"] ?? fallbackBaseUri);
        client.DefaultRequestHeaders.Add("X-Api-Key", wab.Configuration["BaseApiKey"]);
    }).ConfigurePrimaryHttpMessageHandler(_ => new HttpClientHandler()
    {
        //To be removed when we deploy to production. The is a breaker for developer certificate validation.
        ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
    }).AddHttpMessageHandler<CorrelationIdDelegatingHandler>();
}