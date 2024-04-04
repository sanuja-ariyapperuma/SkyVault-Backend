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

        public List<CustomerProfile>? Search(string searchQuery, int systemUserId, int roleId) => 
            db.CustomerProfiles.Where(c => c.SystemUserId == systemUserId && 
            (
                c.Passports.Any(p => p.PassportNumber.Contains(searchQuery)) ||
                c.Passports.Any(p => p.LastName.Contains(searchQuery)) ||
                c.Passports.Any(p => p.OtherNames.Contains(searchQuery))
            )).Include(p => p.Passports)
            .Include(p => p.Salutation)
            .Select(p => new CustomerProfile
            {
                Id = p.Id,
                Salutation = p.Salutation,
                Passports = p.Passports
            }).ToList();
   
 
        public CustomerProfile Create(CustomerProfile newProfile) 
        {
            var savedprofile = db.CustomerProfiles.Add(newProfile);
            db.SaveChanges();

            return savedprofile.Entity;
        }
    }
}
