namespace SkyVault.WebApp.Models;

public record VisaModel(int Id, 
    string VisaNumber, 
    string IssuedPlace,
    DateOnly IssuedDate,
    DateOnly ExpireDate,
    int CountryId,
    string PassportId);