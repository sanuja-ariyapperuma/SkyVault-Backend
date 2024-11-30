using Microsoft.EntityFrameworkCore;
using SkyVault.Payloads.RequestPayloads;
using SkyVault.WebApi.Backend.Models;
using System.Globalization;

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
                    DateOfBirth = DateOnly.FromDateTime(DateTime.ParseExact(newPassport.DateOfBirth, "dd/MM/yyyy", CultureInfo.InvariantCulture)),
                    ExpiryDate = DateOnly.FromDateTime(DateTime.ParseExact(newPassport.ExpiryDate, "dd/MM/yyyy", CultureInfo.InvariantCulture)),
                    CustomerProfileId = Convert.ToInt32(newPassport.CustomerProfileId),
                    Gender = newPassport.Gender!,
                    IsPrimary = newPassport.IsPrimary!,
                    NationalityId = Convert.ToInt32(newPassport.NationalityId)
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
                            DateOnly.FromDateTime(DateTime.ParseExact(passportRequest.DateOfBirth, "dd/MM/yyyy", CultureInfo.InvariantCulture)))
                        .SetProperty(passport => passport.ExpiryDate,
                            DateOnly.FromDateTime(DateTime.ParseExact(passportRequest.ExpiryDate, "dd/MM/yyyy", CultureInfo.InvariantCulture)))
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
                    .AsNoTracking()
                    .Include(c => c.CustomerProfile)
                    .Include(c => c.Country)
                    .Include(c => c.Nationality)
                    .Where(p => p.CustomerProfileId == customerProfileId).ToList();

                return new SkyResult<List<Passport>>().SucceededWithValue(result);
        }
    }
}
