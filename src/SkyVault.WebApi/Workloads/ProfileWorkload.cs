using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkyVault.Payloads;
using SkyVault.Payloads.RequestPayloads;
using SkyVault.Payloads.ResponsePayloads;
using SkyVault.WebApi.Backend;
using SkyVault.WebApi.Backend.Models;

namespace SkyVault.WebApi.Workloads
{
    internal static class ProfileWorkload
    {
        public static async Task<SkyResult<ProfilePayload>> SaveProfile(
            [FromBody] ProfilePayload profile,
            SkyvaultContext dbContext
        )
        {
 
            var newprofile = await dbContext.CustomerProfiles.CreateProfile(profile, dbContext);

            var result = new SkyResult<ProfilePayload>();
            return newprofile == null ? result.Fail("Invalid Information Found", "400", "0") : result.SucceededWithValue(newprofile);
        }

        public static async Task<SkyResult<ProfilePayload>> GetProfile(
                       [FromRoute] string id, 
                       [FromRoute] string sysUserId,
                       SkyvaultContext dbContext
                   )
        {
            var customerProfile = await dbContext.CustomerProfiles.GetProfile(id, sysUserId, dbContext);

            var result = new SkyResult<ProfilePayload>();

            return customerProfile == null ? 
                result.Fail("Profile not found", "404", "0") : 
                result.SucceededWithValue(customerProfile);
        }

        public static async Task<SkyResult<List<ProfileSearchResponse>>> GetAllProfiles(
                [FromRoute] string SysUserId,
                [FromRoute] string RoleId,
                [FromRoute] string SearchQuery,
                SkyvaultContext dbContext
            ) 
        {
            var searchResult = await dbContext.CustomerProfiles.GetAllProfiles(SysUserId, RoleId, SearchQuery, dbContext);
            
            var result = new SkyResult<List<ProfileSearchResponse>>();

            return result.SucceededWithValue(searchResult);
        }

    }
}
