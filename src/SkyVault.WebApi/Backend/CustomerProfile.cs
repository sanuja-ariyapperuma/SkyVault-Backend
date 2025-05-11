namespace SkyVault.WebApi.Backend.Models
{
    public partial class CustomerProfile
    {

        public string NameWithInitials
        {
            get
            {
                var defaultPassport = Passports.FirstOrDefault();
                if (defaultPassport == null || string.IsNullOrWhiteSpace(defaultPassport.OtherNames))
                    return string.Empty;

                return $"{Salutation.SalutationName}. {GetInitialsFromName(defaultPassport.OtherNames)}{defaultPassport.LastName}";
            }
        }

        private static string GetInitialsFromName(string name) 
        {
            if (string.IsNullOrWhiteSpace(name))
                return string.Empty;

            return string.Join(".", name
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(name => char.ToUpper(name[0]))) + ".";
        }
    }
}
