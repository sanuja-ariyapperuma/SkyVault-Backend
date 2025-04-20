namespace SkyVault.WebApi.Helper
{
    public static class Helpers
    {
        public static string SummarizeText(string text, int maxLength = 50)
        {
            if (string.IsNullOrWhiteSpace(text))
                return string.Empty;

            // Try to find the '|' and safely handle its absence
            var pipeIndex = text.IndexOf('|');
            if (pipeIndex >= 0)
            {
                text = text.Substring(pipeIndex + 1).Trim();
            }

            // If the text is short enough after trimming, return it
            if (text.Length <= maxLength)
                return text;

            // Otherwise, return a truncated version with "..."
            return text.Substring(0, maxLength).Trim() + "...";
        }
    }
}
