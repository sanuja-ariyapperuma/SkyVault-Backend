using Microsoft.AspNetCore.Mvc;
using SkyVault.Payloads.RequestPayloads;
using SkyVault.WebApi.Backend.Models;
using SkyVault.WebApi.Backend;
using SkyVault.Payloads.ResponsePayloads;

namespace SkyVault.WebApi.Workloads
{
    public static class TransferProfileWorkload
    {
        public async static Task<IResult> GetAllStaff(
            SkyvaultContext dbContext
            )
        {

            var transferProfileData = new TransferProfileData(dbContext);
            
            var allStaff = await transferProfileData.GetAllStaff();

            if (!allStaff.Succeeded || allStaff.Value == null) 
            {
                return Results.Problem("Error loading staff", statusCode: StatusCodes.Status500InternalServerError);
            }

            List<StaffMemberResponse> staffListResponse = [];

            foreach (var staff in allStaff.Value)
            {
                staffListResponse.Add(new StaffMemberResponse(
                    staff.Id,
                    StaffDescription(staff.FirstName, staff.LastName, staff.UserRole)
                ));
            }

            return Results.Ok(staffListResponse);
        }

        public static async Task<IResult> GetCustomersForStaffId(
            [FromBody] GetClientsForStaffIdRequest request,
            SkyvaultContext dbContext)
        {
            if (request?.StaffId is null)
                return Results.Problem("StaffId cannot be null or empty.", statusCode: StatusCodes.Status400BadRequest);

            int staffId = request.StaffId;

            var transferProfileData = new TransferProfileData(dbContext);
            var customersResult = await transferProfileData.GetCustomersForStaffId(staffId);

            if (!customersResult.Succeeded || customersResult.Value == null)
                return Results.Problem("Error retrieving clients", statusCode: StatusCodes.Status400BadRequest);

            var customerResponse = customersResult.Value
                .Select(c => new CustomersForStaffIdResponse(c.Id, c.NameWithInitials))
                .ToList();

            return Results.Ok(customerResponse);
        }

        public static async Task<IResult> TransferProfiles(
            [FromBody] TransferProfileRequest request,
            SkyvaultContext dbContext)
        {
            if (request?.TransferTo is null)
                return Results.Problem("TransferToId cannot be null or empty.", statusCode: StatusCodes.Status400BadRequest);

            if (request?.Customers is null || request.Customers.Length == 0)
                return Results.Problem("Customers cannot be null or empty.", statusCode: StatusCodes.Status400BadRequest);

            var transferProfileData = new TransferProfileData(dbContext);

            int[] customerIdArray = [.. request.Customers.Select(c => c.CustomerId)];

            var customersResult = await transferProfileData.UpdateProfileTransfer(request.TransferTo, customerIdArray);

            if (!customersResult.Succeeded)
                return Results.Problem("Error updating trasfers", statusCode: StatusCodes.Status400BadRequest);

            return Results.Ok();
        }

        private static string StaffDescription(string firstName, string lastName, string role ) 
        {
            return $"{firstName} {lastName} ({role})";
        }

        
    }
}
