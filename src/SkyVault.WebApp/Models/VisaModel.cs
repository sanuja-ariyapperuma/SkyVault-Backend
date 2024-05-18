namespace SkyVault.WebApp.Models;

public record VisaModel(int Id, 
    string VisaNumber, 
    string IssuedPlace,
    string IssuedDate,
    string ExpireDate,
    string CountryId,
    string PassportId);