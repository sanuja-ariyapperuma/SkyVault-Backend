using Microsoft.AspNetCore.Mvc;
using SkyVault.Payloads.RequestPayloads;
using SkyVault.WebApi.Backend.Models;
using SkyVault.WebApi.Backend;
using SkyVault.Payloads.ResponsePayloads;
using SkyVault.Exceptions;

namespace SkyVault.WebApi.Workloads
{
    public static class TransferProfileWorkload
    {
        public async static Task<SkyResult<List<StaffMemberResponse>>> GetAllStaff(
            SkyvaultContext dbContext
            )
        {
            var correlationId = "00000000-0000"; // Default correlation ID for non-HTTP context

            try
            {
                var transferProfileData = new TransferProfileData(dbContext);
                var allStaff = await transferProfileData.GetAllStaff();

                if (!allStaff.Succeeded || allStaff.Value == null) 
                {
                    return new SkyResult<List<StaffMemberResponse>>().Fail(
                        allStaff?.Message ?? "Error loading staff", 
                        allStaff?.ErrorCode ?? "transfer-0001", 
                        allStaff?.CorrelationId ?? correlationId);
                }

                List<StaffMemberResponse> staffListResponse = [];

                foreach (var staff in allStaff.Value)
                {
                    staffListResponse.Add(new StaffMemberResponse(
                        staff.Id,
                        StaffDescription(staff.FirstName ?? "", staff.LastName ?? "", staff.UserRole ?? "")
                    ));
                }

                return new SkyResult<List<StaffMemberResponse>>().SucceededWithValue(staffListResponse);
            }
            catch (Exception e)
            {
                e.LogException(correlationId);
                return new SkyResult<List<StaffMemberResponse>>().Fail(
                    "An unexpected error occurred while loading staff", 
                    "transfer-0002", 
                    correlationId);
            }
        }

        public static async Task<SkyResult<List<CustomersForStaffIdResponse>>> GetCustomersForStaffId(
            [FromBody] GetClientsForStaffIdRequest request,
            SkyvaultContext dbContext)
        {
            var correlationId = "00000000-0000"; // Default correlation ID for non-HTTP context

            try
            {
                if (request?.StaffId is null)
                    return new SkyResult<List<CustomersForStaffIdResponse>>().Fail(
                        "StaffId cannot be null or empty.", 
                        "transfer-0003", 
                        correlationId);

                int staffId = request.StaffId;

                var transferProfileData = new TransferProfileData(dbContext);
                var customersResult = await transferProfileData.GetCustomersForStaffId(staffId);

                if (!customersResult.Succeeded || customersResult.Value == null)
                    return new SkyResult<List<CustomersForStaffIdResponse>>().Fail(
                        customersResult?.Message ?? "Error retrieving clients", 
                        customersResult?.ErrorCode ?? "transfer-0004", 
                        customersResult?.CorrelationId ?? correlationId);

                var customerResponse = customersResult.Value
                    .Select(c => new CustomersForStaffIdResponse(c.Id, c.NameWithInitials))
                    .ToList();

                return new SkyResult<List<CustomersForStaffIdResponse>>().SucceededWithValue(customerResponse);
            }
            catch (Exception e)
            {
                e.LogException(correlationId);
                return new SkyResult<List<CustomersForStaffIdResponse>>().Fail(
                    "An unexpected error occurred while retrieving customers", 
                    "transfer-0005", 
                    correlationId);
            }
        }

        public static async Task<SkyResult<bool>> TransferProfiles(
            [FromBody] TransferProfileRequest request,
            SkyvaultContext dbContext)
        {
            var correlationId = "00000000-0000"; // Default correlation ID for non-HTTP context

            try
            {
                if (request?.TransferTo is null)
                    return new SkyResult<bool>().Fail(
                        "TransferToId cannot be null or empty.", 
                        "transfer-0006", 
                        correlationId);

                if (request?.Customers is null || request.Customers.Length == 0)
                    return new SkyResult<bool>().Fail(
                        "Customers cannot be null or empty.", 
                        "transfer-0007", 
                        correlationId);

                var transferProfileData = new TransferProfileData(dbContext);

                int[] customerIdArray = [.. request.Customers.Select(c => c.CustomerId)];

                var customersResult = await transferProfileData.UpdateProfileTransfer(request.TransferTo, customerIdArray);

                if (!customersResult.Succeeded)
                    return new SkyResult<bool>().Fail(
                        customersResult.Message ?? "Error updating transfers", 
                        customersResult.ErrorCode ?? "transfer-0008", 
                        customersResult.CorrelationId ?? correlationId);

                return new SkyResult<bool>().SucceededWithValue(true);
            }
            catch (Exception e)
            {
                e.LogException(correlationId);
                return new SkyResult<bool>().Fail(
                    "An unexpected error occurred while transferring profiles", 
                    "transfer-0009", 
                    correlationId);
            }
        }

        private static string StaffDescription(string firstName, string lastName, string role ) 
        {
            return $"{firstName} {lastName} ({role})";
        }

        
    }
}
