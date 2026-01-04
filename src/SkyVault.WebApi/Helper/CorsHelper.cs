namespace SkyVault.WebApi.Helper;

public static class CorsHelper
{
    public static string[] GetAllowedOrigins()
    {
        var allowedOriginsEnv = Environment.GetEnvironmentVariable("CORS_ALLOWED_ORIGINS");
        var validOrigins = new List<string>();

        if (!string.IsNullOrWhiteSpace(allowedOriginsEnv))
        {
            var origins = allowedOriginsEnv.Split(',', StringSplitOptions.RemoveEmptyEntries);
            foreach (var origin in origins)
            {
                var trimmedOrigin = origin.Trim();
                if (Uri.TryCreate(trimmedOrigin, UriKind.Absolute, out var uriResult) &&
                    (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps))
                {
                    validOrigins.Add(trimmedOrigin);
                }
                else
                {
                    Console.WriteLine($"Warning: Invalid CORS origin found in environment variables: '{trimmedOrigin}'. It will be ignored.");
                }
            }
        }

        if (validOrigins.Count == 0)
        {
            Console.WriteLine("Warning: No valid CORS origins found in CORS_ALLOWED_ORIGINS. Using default fallback origins.");
        }

        return validOrigins.ToArray();
    }
}
