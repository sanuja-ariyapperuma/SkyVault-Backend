using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using SkyVault.WebApi.Backend.Models;

namespace SkyVault.WebApi.Backend
{
    public class CustomerProfileData
    {
        private readonly SkyvaultContext db;

        public CustomerProfileData(SkyvaultContext db)
        {
            this.db = db;
        }

        public CustomerProfile? Get(string profileId, string systemUserId) 
        {

            var profile = db.CustomerProfiles.Include(p => p.Passports)
                .ThenInclude(o => o.Visas)
                .FirstOrDefault(p => p.Id == Convert.ToInt32(profileId) && p.SystemUserId == Convert.ToInt32(systemUserId));
            return profile;
   
        }

        public CustomerProfile Create(CustomerProfile newProfile) 
        {
            var savedprofile = db.CustomerProfiles.Add(newProfile);
            db.SaveChanges();

            return savedprofile.Entity;
        }
    }
}
