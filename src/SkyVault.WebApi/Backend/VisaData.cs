using Microsoft.EntityFrameworkCore;
using SkyVault.Payloads.RequestPayloads;
using SkyVault.WebApi.Backend.Models;

namespace SkyVault.WebApi.Backend
{
    public sealed class VisaData(SkyvaultContext db)
    {
        public SkyResult<string> AddVisa(VisaReqeust visaRequest, string correlationId)
        {
                var newvisa = new Visa
                {
                    VisaNumber = visaRequest.VisaNumber!,
                    IssuedPlace = visaRequest.IssuedPlace!,
                    IssuedDate = DateOnly.FromDateTime(DateTime.Parse(visaRequest.IssuedDate)),
                    ExpireDate = DateOnly.FromDateTime(DateTime.Parse(visaRequest.ExpiryDate)),
                    CountryId = Convert.ToInt32(visaRequest.CountryId),
                    PassportId = Convert.ToInt32(visaRequest.PassportId)
                };

                db.Visas.Add(newvisa);
                db.SaveChanges();

                return new SkyResult<string>().SucceededWithValue(newvisa.Id.ToString());
            
        }

        public SkyResult<string> UpdateVisa(string visaId, VisaReqeust visaRequest, int userId, string correlationId)
        {
            var results = db.Visas
               .Where(v =>
                    v.Id == Convert.ToInt32(visaId) &&
                    v.Passport.CustomerProfile.SystemUserId == userId
                    )
               .ExecuteUpdate(updates =>
                    updates.SetProperty(visa => visa.VisaNumber, visaRequest.VisaNumber)
                        .SetProperty(visa => visa.IssuedPlace, visaRequest.IssuedPlace)
                        .SetProperty(visa => visa.IssuedDate, DateOnly.FromDateTime(
                            DateTime.Parse(visaRequest.IssuedDate)))
                        .SetProperty(visa => visa.ExpireDate, DateOnly.FromDateTime(
                            DateTime.Parse(visaRequest.ExpiryDate)))
                        .SetProperty(visa => visa.CountryId, Convert.ToInt32(visaRequest.CountryId))
                        .SetProperty(visa => visa.PassportId, Convert.ToInt32(visaRequest.PassportId))
                        );

            if (results == 0)
                return new SkyResult<string>().Fail("Failed to update visa", "0daa030e-0005", correlationId);

            return new SkyResult<string>().SucceededWithValue("Visa Updated Successfully");
        }

        public SkyResult<Visa> GetVisaById(int visaId, string correlationId)
        {

            var result = db.Visas.Where(p => p.Id == visaId).FirstOrDefault();

            if (result == null)
                return new SkyResult<Visa>().Fail("No passport found", "1f7504d7-0005", correlationId);

            return new SkyResult<Visa>().SucceededWithValue(result);
        }
        public SkyResult<List<Visa>> GetVisaByCustomerProfileId(int customerProfileId, string correlationId)
        {
            var result = db.Visas
                .Include(v => v.Country)
                .Include(v => v.Passport)
                .ThenInclude(p => p.CustomerProfile)
                .Include(v => v.Passport)
                .ThenInclude(p => p.Country)
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
