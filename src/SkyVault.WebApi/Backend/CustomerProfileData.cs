using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using SkyVault.WebApi.Backend.Models;

namespace SkyVault.WebApi.Backend
{
    public class CustomerProfileData(SkyvaultContext db)
    {
        public CustomerProfile? Get(int profileId, int systemUserId) => 
            db.CustomerProfiles.Include(p => p.Passports)
                .ThenInclude(o => o.Visas)
                .FirstOrDefault(p => p.Id == profileId && p.SystemUserId == systemUserId);
   
 
        public CustomerProfile Create(CustomerProfile newProfile) 
        {
            var savedprofile = db.CustomerProfiles.Add(newProfile);
            db.SaveChanges();

            return savedprofile.Entity;
        }
    }
}
