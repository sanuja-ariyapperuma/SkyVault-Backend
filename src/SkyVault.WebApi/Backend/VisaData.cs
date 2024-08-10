using Microsoft.EntityFrameworkCore;
using SkyVault.Payloads.RequestPayloads;
using SkyVault.WebApi.Backend.Models;

namespace SkyVault.WebApi.Backend
{
    public sealed class VisaData(SkyvaultContext db)
    {
        public SkyResult<String> AddVisa(VisaReqeust visaRequest, string correlationId) 
        {
            try
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

                return new SkyResult<String>().SucceededWithValue("Visa Added Successfully");
            }
            catch (Exception ex)
            {

                return new SkyResult<String>().Fail(ex.Message, "0daa030e-0004", correlationId);
            }
        }

        public SkyResult<String> UpdateVisa(VisaReqeust visaRequest, string correlationId) 
        {
            var results = db.Visas
               .Where(v => 
                    v.Id == Convert.ToInt32(visaRequest.Id) &&
                    v.PassportId == Convert.ToInt32(visaRequest.PassportId) &&
                    v.Passport.CustomerProfile.SystemUserId == Convert.ToInt32(visaRequest.SystemUserId))
               .ExecuteUpdate(updates =>
                    updates.SetProperty(visa => visa.VisaNumber, visaRequest.VisaNumber)
                        .SetProperty(visa => visa.IssuedPlace, visaRequest.IssuedPlace)
                        .SetProperty(visa => visa.IssuedDate, DateOnly.FromDateTime(
                            DateTime.Parse(visaRequest.IssuedDate)))
                        .SetProperty(visa => visa.ExpireDate, DateOnly.FromDateTime(
                            DateTime.Parse(visaRequest.ExpiryDate)))
                        .SetProperty(visa => visa.CountryId, Convert.ToInt32(visaRequest.CountryId)));

            if (results == 0)
                return new SkyResult<String>().Fail("Failed to update visa", "0daa030e-0005", correlationId);

            return new SkyResult<String>().SucceededWithValue("Visa Updated Successfully");
        }

        public SkyResult<Visa> GetVisaById(int visaId, string correlationId){

            var result = db.Visas.Where(p => p.Id == visaId).FirstOrDefault();

            if(result == null)
                return new SkyResult<Visa>().Fail("No passport found", "1f7504d7-0005", correlationId);

            return new SkyResult<Visa>().SucceededWithValue(result);
        }
    }
}
