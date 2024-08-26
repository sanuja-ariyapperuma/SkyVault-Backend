using Microsoft.EntityFrameworkCore;
using SkyVault.Payloads.RequestPayloads;
using SkyVault.WebApi.Backend.Models;

namespace SkyVault.WebApi.Backend
{
    public sealed class PassportData(SkyvaultContext db)
    {
        public SkyResult<Passport> AddNewPassport(PassportRequest newPassport, string correlationId)
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

        public SkyResult<string> UpdatePassport(PassportRequest passportRequest, string correlationId)
        {

                var rowsAffected = db.Passports
                    .Include(p => p.CustomerProfile)
                    .Where(p =>
                        p.Id == Convert.ToInt32(passportRequest.Id)
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
                    return new SkyResult<string>().Fail("No records updated", "1f7504d7-0000", correlationId);

                return new SkyResult<string>().SucceededWithValue("Successfully Updated");
        }
        public SkyResult<List<Passport>> GetPassportByCustomerProfileId(int customerProfileId, string correlationId)
        {
                var result = db.Passports
                    .Include(c => c.CustomerProfile)
                    .Where(p => p.CustomerProfileId == customerProfileId).ToList();

                return new SkyResult<List<Passport>>().SucceededWithValue(result);
        }

        public SkyResult<string> CheckPassportExists(string passportNumber, string correlationId)
        {
            var isPassportExists = db.Passports.Any(p => p.PassportNumber == passportNumber);

            if (isPassportExists)
                return new SkyResult<string>().Fail("Passport Number Already Exists", "1f7504d7-0006", correlationId);

            return new SkyResult<string>().SucceededWithValue("Passport Number can be use");
        }
    }
}
