namespace SkyVault.WebApp.Models;

public record PassportModel(
    string? Id, 
    string? LastName,
    string? OtherNames,
    string? PassportNo,
    string? Gender,
    string? DateOfBirth,
    string? ExpiryDate,
    string? PlaceOfBirth,
    string? NationalityId,
    string? IsPrimary,
    string? CountryId);