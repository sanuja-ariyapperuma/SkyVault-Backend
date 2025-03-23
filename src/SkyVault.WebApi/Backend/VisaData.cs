using Microsoft.EntityFrameworkCore;
using SkyVault.Payloads.RequestPayloads;
using SkyVault.WebApi.Backend.Models;
using System.Globalization;

namespace SkyVault.WebApi.Backend
{
    public sealed class VisaData(SkyvaultContext db)
    {
        public SkyResult<Models.Visa> AddVisa(VisaReqeust visaRequest, string correlationId)
        {
            var newvisa = new Visa
            {
                VisaNumber = visaRequest.VisaNumber!,
                IssuedPlace = visaRequest.IssuedPlace!,
                IssuedDate = DateOnly.FromDateTime(DateTime.ParseExact(visaRequest.IssuedDate, "dd/MM/yyyy", CultureInfo.InvariantCulture)),
                ExpireDate = DateOnly.FromDateTime(DateTime.ParseExact(visaRequest.ExpiryDate, "dd/MM/yyyy", CultureInfo.InvariantCulture)),
                CountryId = Convert.ToInt32(visaRequest.CountryId),
                PassportId = Convert.ToInt32(visaRequest.PassportId),
                BirthPlace = visaRequest.BirthPlace!,
                DestinationCountryId = Convert.ToInt32(visaRequest.DestinationCountryId)
            };

            db.Visas.Add(newvisa);
            db.SaveChanges();

            var returningVisa =
                db.Visas.Include(v => v.Country)
                .Include(v => v.Passport)
                .ThenInclude(p => p.CustomerProfile)
                .Include(v => v.Passport)
                .ThenInclude(p => p.Country)
                .Include(v => v.DestinationCountry)
                .FirstOrDefaultAsync(v => v.Id == newvisa.Id).Result;

            return new SkyResult<Visa>().SucceededWithValue(returningVisa!);

        }

        public SkyResult<string> UpdateVisa(string visaId, VisaReqeust visaRequest, string correlationId)
        {
            var results = db.Visas
               .Where(v =>
                    v.Id == Convert.ToInt32(visaId)
                    )
               .ExecuteUpdate(updates =>
                    updates.SetProperty(visa => visa.VisaNumber, visaRequest.VisaNumber)
                        .SetProperty(visa => visa.IssuedPlace, visaRequest.IssuedPlace)
                        .SetProperty(visa => visa.IssuedDate, DateOnly.FromDateTime(
                            DateTime.ParseExact(visaRequest.IssuedDate, "dd/MM/yyyy", CultureInfo.InvariantCulture)))
                        .SetProperty(visa => visa.ExpireDate, DateOnly.FromDateTime(
                            DateTime.ParseExact(visaRequest.ExpiryDate, "dd/MM/yyyy", CultureInfo.InvariantCulture)))
                        .SetProperty(visa => visa.CountryId, Convert.ToInt32(visaRequest.CountryId))
                        .SetProperty(visa => visa.PassportId, Convert.ToInt32(visaRequest.PassportId))
                        .SetProperty(visa => visa.BirthPlace, visaRequest.BirthPlace)
                        .SetProperty(visa => visa.DestinationCountryId, Convert.ToInt32(visaRequest.DestinationCountryId))
                        );

            if (results == 0)
                return new SkyResult<string>().Fail("Failed to update visa", "0daa030e-0005", correlationId);

            return new SkyResult<string>().SucceededWithValue("Visa Updated Successfully");
        }

        public SkyResult<Visa> GetVisaById(int visaId, string correlationId)
        {

            var result = db.Visas.AsNoTracking().FirstOrDefault(p => p.Id == visaId);

            if (result == null)
                return new SkyResult<Visa>().Fail("No passport found", "1f7504d7-0005", correlationId);

            return new SkyResult<Visa>().SucceededWithValue(result);
        }
        public SkyResult<List<Visa>> GetVisaByCustomerProfileId(int customerProfileId, string correlationId)
        {
            var result = db.Visas
                .AsNoTracking()
                .Include(v => v.Country)
                .Include(v => v.Passport)
                .ThenInclude(p => p.CustomerProfile)
                .Include(v => v.Passport)
                .ThenInclude(p => p.Country)
                .Include(v => v.DestinationCountry)
                .Where(v => v.Passport.CustomerProfileId == customerProfileId)
                .OrderByDescending(v => v.IssuedDate)
                .ToList();

            if (result.Count == 0)
                return new SkyResult<List<Visa>>().Fail("No VISA found", "1f7504d7-0005", correlationId);

            return new SkyResult<List<Visa>>().SucceededWithValue(result);
        }

        public SkyResult<bool> DeleteVisa(int deletingVisa, string correlationId)
        {
            var result = db.Visas
                .Where(v => v.Id == deletingVisa)
                .ExecuteDelete();

            if (result == 0)
                return new SkyResult<bool>().Fail("Failed to delete visa", "0daa030e-0006", correlationId);

            return new SkyResult<bool>().SucceededWithValue(true);
        }
    }
}
