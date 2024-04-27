namespace SkyVault.WebApp.Models;

public record PassportModel(string Id, 
    string LastName,
    string OtherNames,
    string PassportNo,
    string Gender,
    DateOnly DateOfBirth,
    DateOnly ExpiryDate,
    string PlaceOfBirth,
    int NationalityId,
    string IsPrimary,
    string CountryId);