using Microsoft.EntityFrameworkCore;
using SkyVault.Payloads.RequestPayloads;
using SkyVault.Payloads.ResponsePayloads;
using SkyVault.WebApi.Backend.Models;

namespace SkyVault.WebApi.Backend
{
    public class FFNData(SkyvaultContext db)
    {
        public SkyResult<string> AddFFN(int customerId, string ffn, string correlationId)
        {


                var newFFN = new FrequentFlyerNumber
                {
                    CustomerProfileId = customerId,
                    FlyerNumber = ffn
                };

                db.FrequentFlyerNumbers.Add(newFFN);
                db.SaveChanges();

                return new SkyResult<string>().SucceededWithValue(newFFN.Id.ToString());

        }

        public SkyResult<string> UpdateFFN(int ffnId, string ffn, string correlationId)
        {
            var results = db.FrequentFlyerNumbers
               .Where(ffn => ffn.Id == ffnId)
               .ExecuteUpdate(updates =>
                   updates.SetProperty(ffn => ffn.FlyerNumber, ffn)
               );

            if (results == 0)
                return new SkyResult<string>().Fail("Failed to update Frequent Flying Number", "0daa030e-0005", correlationId);

            return new SkyResult<string>().SucceededWithValue(ffnId.ToString());
        }

        public SkyResult<string> DeleteFFN(int ffnId, string correlationId)
        {
            var ffn = db.FrequentFlyerNumbers
                .Where(ffn => ffn.Id == ffnId)
                .FirstOrDefault();

            if (ffn == null)
                return new SkyResult<string>().Fail("Frequent Flying Number not found", "0daa030e-0006", correlationId);

            db.FrequentFlyerNumbers.Remove(ffn);
            db.SaveChanges();

            return new SkyResult<string>().SucceededWithValue("Frequent Flying Number deleted successfully");
        }

        public SkyResult<List<FFNResponse>> GetFFNByCustomerId(int customerId, string correlationId)
        {
            var ffnList = db.FrequentFlyerNumbers
                .Where(ffn => ffn.CustomerProfileId == customerId)
                .Select(ffn => new FFNResponse
                {
                    FFNId = ffn.Id.ToString(),
                    FFN = ffn.FlyerNumber
                })
                .ToList();

            return new SkyResult<List<FFNResponse>>().SucceededWithValue(ffnList);
        }
    }
}
