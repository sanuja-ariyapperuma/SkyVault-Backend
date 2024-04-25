using Microsoft.EntityFrameworkCore;
using SkyVault.Payloads.RequestPayloads;
using SkyVault.WebApi.Backend.Models;

namespace SkyVault.WebApi.Backend
{
    public sealed class VisaData(SkyvaultContext db)
    {

        public SkyResult<String> ValidateVisaDetails(VisaReqeust visaRequest, string correlationId)
        {
            var isPassportExists = db.Passports.Any(p =>
                p.Id == Convert.ToInt32(visaRequest.PassportId) &&
                p.CustomerProfile.SystemUserId == Convert.ToInt32(visaRequest.SystemUserId)
                );

            var isCountryExists = db.Countries.Any(c => c.Id == Convert.ToInt32(visaRequest.CountryId));

            if (!isPassportExists)
                return new SkyResult<String>().Fail("No passport found or unauthorized access", "0daa030e-0000", correlationId);


            if (!isCountryExists)
                return new SkyResult<String>().Fail("Invalid Country Found", "0daa030e-0001", correlationId);
            
            
            return new SkyResult<String>().SucceededWithValue("Validated");

        }

        public SkyResult<String> AddVisa(VisaReqeust visaRequest, string correlationId) 
        {
            try
            {
                var newvisa = new Visa
                {
                    VisaNumber = visaRequest.VisaNumber!,
                    IssuedPlace = visaRequest.VisaIssuedPlace!,
                    IssuedDate = DateOnly.FromDateTime(DateTime.Parse(visaRequest.VisaissuedDate)),
                    ExpireDate = DateOnly.FromDateTime(DateTime.Parse(visaRequest.VisaExpiryDate)),
                    CountryId = Convert.ToInt32(visaRequest.CountryId),
                    PassportId = Convert.ToInt32(visaRequest.PassportId)
                };

                var savevisa = db.Visas.Add(newvisa);
                db.SaveChanges();

                return new SkyResult<String>().SucceededWithValue("Visa Added Successfully");
            }
            catch (Exception ex)
            {

                return new SkyResult<String>().Fail(ex.Message, "0daa030e-0002", correlationId);
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
                        .SetProperty(visa => visa.IssuedPlace, visaRequest.VisaIssuedPlace)
                        .SetProperty(visa => visa.IssuedDate, DateOnly.FromDateTime(
                            DateTime.Parse(visaRequest.VisaissuedDate)))
                        .SetProperty(visa => visa.ExpireDate, DateOnly.FromDateTime(
                            DateTime.Parse(visaRequest.VisaExpiryDate)))
                        .SetProperty(visa => visa.CountryId, Convert.ToInt32(visaRequest.CountryId)));

            if (results == 0)
                return new SkyResult<String>().Fail("Failed to update visa", "0daa030e-0003", correlationId);

            return new SkyResult<String>().SucceededWithValue("Visa Updated Successfully");
        }

    }
}
