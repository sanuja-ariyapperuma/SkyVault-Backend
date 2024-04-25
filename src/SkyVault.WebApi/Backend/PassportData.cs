using Microsoft.EntityFrameworkCore;
using SkyVault.Payloads.RequestPayloads;
using SkyVault.WebApi.Backend.Models;

namespace SkyVault.WebApi.Backend
{
    public sealed class PassportData(SkyvaultContext db)
    {
        public SkyResult<Passport> AddNewPassport(PassportRequest newPassport, string correlationId)
        {

            try
            {
                var passport = new Passport
                {
                    PassportNumber = newPassport.PassportNumber!,
                    LastName = newPassport.LastName!,
                    OtherNames = newPassport.OtherNames,
                    CountryId = Convert.ToInt32(newPassport.CountryId),
                    DateOfBirth = DateOnly.FromDateTime(DateTime.Parse(newPassport.DateOfBirth)),
                    ExpiryDate = DateOnly.FromDateTime(DateTime.Parse(newPassport.ExpiryDate)),
                    CustomerProfileId = Convert.ToInt32(newPassport.CustomerProfileId),
                    Gender = newPassport.Gender!,
                    IsPrimary = newPassport.IsPrimary!,
                    NationalityId = Convert.ToInt32(newPassport.NationalityId),
                    PlaceOfBirth = newPassport.PlaceOfBirth
                };

                var savepassport = db.Passports.Add(passport);
                db.SaveChanges();

                return new SkyResult<Passport>().SucceededWithValue(savepassport.Entity);
            }
            catch (Exception ex)
            {
                return new SkyResult<Passport>().Fail(ex.Message, "1f7504d7-0004", correlationId);
            }
        }

        public SkyResult<String> UpdatePassport(PassportRequest passportRequest, string correlationId)
        {
            try
            {

                var rowsAffected = db.Passports
                    .Include(p => p.CustomerProfile)
                    .Where(p =>
                        p.Id == Convert.ToInt32(passportRequest.Id) &&
                        p.CustomerProfile.SystemUserId == Convert.ToInt32(passportRequest.SystemUserId)
                    ).ExecuteUpdate(updates =>
                        updates.SetProperty(passport => passport.PassportNumber, passportRequest.PassportNumber)
                        .SetProperty(passport => passport.LastName, passportRequest.LastName)
                        .SetProperty(passport => passport.OtherNames, passportRequest.OtherNames)
                        .SetProperty(passport => passport.Gender, passportRequest.Gender)
                        .SetProperty(passport => passport.DateOfBirth,
                            DateOnly.FromDateTime(DateTime.Parse(passportRequest.DateOfBirth)))
                        .SetProperty(passport => passport.ExpiryDate,
                            DateOnly.FromDateTime(DateTime.Parse(passportRequest.ExpiryDate)))
                        .SetProperty(passport => passport.PlaceOfBirth, passportRequest.PlaceOfBirth)
                        .SetProperty(passport => passport.NationalityId,
                            Convert.ToInt32(passportRequest.NationalityId))
                        .SetProperty(passport => passport.CountryId,
                            Convert.ToInt32(passportRequest.CountryId))
                        .SetProperty(passport => passport.IsPrimary, passportRequest.IsPrimary)
                        );

                if (rowsAffected == 0)
                    return new SkyResult<String>().Fail("Failed to update passport", "1f7504d7-0000", correlationId);

                return new SkyResult<String>().SucceededWithValue("Successfully Updated");
            }
            catch (Exception ex)
            {
                return new SkyResult<String>().Fail(ex.Message, "1f7504d7-0001", correlationId);
            }
        }

        public SkyResult<String> ValidatePassportDetails(PassportRequest passportRequest, string correlationId) 
        {
            var isNationality = db.Nationalities.Any(n => n.Id == Convert.ToInt32(passportRequest.NationalityId));
            var isCountry = db.Countries.Any(c => c.Id == Convert.ToInt32(passportRequest.CountryId));
            var isSalutaion = db.Salutations.Any(s => s.Id == Convert.ToInt32(passportRequest.SalutationId));

            if (!isNationality)
                return new SkyResult<String>().Fail("Invalid Nationality Found", "1f7504d7-0001", correlationId);

            if(!isCountry)
                return new SkyResult<String>().Fail("Invalid Country Found", "1f7504d7-0002", correlationId);

            if(!isSalutaion)
                return new SkyResult<String>().Fail("Invalid Salutation Found", "1f7504d7-0003", correlationId);

            return new SkyResult<String>().SucceededWithValue("Validated");
                
        }
    }
}
