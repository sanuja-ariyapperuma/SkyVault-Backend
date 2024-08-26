using System.Reflection.Metadata.Ecma335;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SkyVault.Exceptions;
using SkyVault.Payloads.CommonPayloads;
using SkyVault.Payloads.RequestPayloads;
using SkyVault.Payloads.ResponsePayloads;
using SkyVault.WebApi.Backend;
using SkyVault.WebApi.Backend.Models;
using SkyVault.WebApi.Helper;

namespace SkyVault.WebApi.Workloads
{
    internal static class ProfileWorkload
    {
        private static string _correlationId = string.Empty;

        #region Workloads

        public static IResult SearchProfiles(
            [FromBody] SearchProfileRequest searchProfileRequest,
            SkyvaultContext dbContext,
            HttpContext context)
        {
            try
            {

                _correlationId = CorrelationHandler.Get(context);

                var customerProfileData = new CustomerProfileData(dbContext);

                var customerProfiles = customerProfileData.Search(
                    searchProfileRequest.SearchQuery!, 
                    Convert.ToInt32(searchProfileRequest.SysUserId));

                return Results.Ok(new SearchProfileResponse(searchProfileRequest.SearchQuery!, 
                    customerProfiles!));
            }
            catch(FormatException e){
                e.LogException(_correlationId);

                return Results.Problem(new ValidationProblemDetails().ToValidationProblemDetails(
                    "Incorrect format of data found", "30550615-0001", _correlationId
                ));
            }
            catch (Exception e)
            {
                e.LogException(_correlationId);

                return Results.Problem(new ProblemDetails().ToProblemDetails(
                    "An unexpected error occurred. Please try again later.",
                    "30550615-0002", _correlationId));
            }
        }

       

        public static IResult AddPassport([FromBody] PassportRequest passportRequest,
            SkyvaultContext dbContext,
            HttpContext context) 
        {
            try
            {
                var customerProfileData = new CustomerProfileData(dbContext);
                SaveUpdateCustomerProfileResponse savedCustomerProfile;

                if (String.IsNullOrEmpty(passportRequest.CustomerProfileId?.Trim())) 
                {
                    if (!IsPassportDataValid(passportRequest))
                        return Results.BadRequest("Invalid data found");  

                    var checkPassportExists = customerProfileData.CheckPassportExists(passportRequest.PassportNumber ?? "", _correlationId);

                    if (checkPassportExists)
                        return Results.BadRequest("A passport from the provided passport number is already existing");

                    var savedprofile = customerProfileData.SaveProfile(passportRequest, _correlationId);
                    
                    savedCustomerProfile = new SaveUpdateCustomerProfileResponse(
                        savedprofile.Value!.Id.ToString(),
                        savedprofile.Value.Passports.First().Id.ToString());

                    return Results.Created<SaveUpdateCustomerProfileResponse>("AddPassport", savedCustomerProfile);
                }
                else 
                {

                    var authorized = customerProfileData.CheckAccessToTheProfile(
                        Convert.ToInt32(passportRequest.CustomerProfileId),
                        Convert.ToInt32(passportRequest.SystemUserId),
                        _correlationId
                    );

                    if (!authorized.Succeeded)
                        return Results.Problem(
                                            new ValidationProblemDetails().ToValidationProblemDetails(
                                                authorized.Message, authorized.ErrorCode, _correlationId));

                    var passportData = new PassportData(dbContext);
                    var savepassport = passportData.AddNewPassport(passportRequest, _correlationId);

                    savedCustomerProfile = new SaveUpdateCustomerProfileResponse(
                        passportRequest.CustomerProfileId,
                        savepassport.Value?.Id.ToString()
                        );

                    return Results.Ok(savedCustomerProfile);
                }

                
            }
            catch (System.Exception e)
            {
                e.LogException(_correlationId);

                return Results.Problem(new ValidationProblemDetails().ToValidationProblemDetails(
                    "Sorry! Couldn't able to update or save passport", "30550615-0003", _correlationId
                ));
            }
        }

        public static IResult GetPassports(
            [FromBody] GetPassportRequest passportRequest,
            SkyvaultContext dbContext,
            HttpContext context
            )
        {
            try
            {
            _correlationId = CorrelationHandler.Get(context);

            var passportData = new PassportData(dbContext);

            var customer_profile_data = new CustomerProfileData(dbContext);

            var authorization = customer_profile_data.CheckAccessToTheProfile(
                Convert.ToInt32(passportRequest.CustomerProfileId),
                Convert.ToInt32(passportRequest.SystemUserId),
                _correlationId
                );

            if (!authorization.Succeeded)
                return Results.Problem(
                                   new ValidationProblemDetails().ToValidationProblemDetails(
                                        authorization.Message, "30550615-0004", _correlationId));

            var passport = passportData.GetPassportByCustomerProfileId(Convert.ToInt32(passportRequest.CustomerProfileId), _correlationId);

            return Results.Ok(passport.Value?.Select(p => new PassportResponse(
                    p.Id.ToString(),
                    p.LastName,
                    p.OtherNames,
                    p.PassportNumber,
                    p.Gender,
                    p.DateOfBirth.ToString(),
                    p.PlaceOfBirth,
                    p.ExpiryDate.ToString(),
                    p.NationalityId.ToString(),
                    p.CountryId.ToString(),
                    p.IsPrimary.ToString(),
                    p.CustomerProfile.SalutationId.ToString()
            )));

            }
            catch (Exception e)
            {
                
                e.LogException(_correlationId);
                return Results.Problem(
                                        new ProblemDetails().ToProblemDetails(
                                            "Sorry! Couldn't able to get passports", "30550615-0005", _correlationId));
            }
            

        }

        public static IResult UpdatePassport(
                [FromBody] PassportRequest passportRequest,
                SkyvaultContext dbContext,
                HttpContext context)
        {
            try
            {
                _correlationId = CorrelationHandler.Get(context);

            var passportData = new PassportData(dbContext);

            var customerProfileData = new CustomerProfileData(dbContext);

            var authorized = customerProfileData.CheckAccessToTheProfile(
                Convert.ToInt32(passportRequest.CustomerProfileId),
                Convert.ToInt32(passportRequest.SystemUserId),
                _correlationId
            );

            if (!authorized.Succeeded)
                return Results.Problem(
                                    new ValidationProblemDetails().ToValidationProblemDetails(
                                        authorized.Message, authorized.ErrorCode, _correlationId));

            var result = passportData.UpdatePassport(passportRequest, _correlationId);

            if (!result.Succeeded)
                return Results.Problem(
                    new ValidationProblemDetails().ToValidationProblemDetails(
                    result.Message, result.ErrorCode, _correlationId));

            return Results.Ok(new SaveUpdateCustomerProfileResponse(passportRequest.CustomerProfileId));

            }
            catch (System.Exception e)
            {
                e.LogException(_correlationId);
                return Results.Problem(
                                        new ProblemDetails().ToProblemDetails(
                                            "Sorry! Couldn't able to update passport", "30550615-0006", _correlationId));
            }
            
        }

        public static IResult GetVisaByCustomerProfileId(
                [FromBody] GetVISAsByCustomerIDRequest visaRequest,
                SkyvaultContext dbContext,
                HttpContext context) 
        {
            try
            {
                _correlationId = CorrelationHandler.Get(context);

                var visaData = new VisaData(dbContext);
                var customer_profile_data = new CustomerProfileData(dbContext);

                var authorization = customer_profile_data.CheckAccessToTheProfile(
                        Convert.ToInt32(visaRequest.CustomerProfileId),
                        Convert.ToInt32(visaRequest.SystemUserId),
                        _correlationId
                    );

                if (!authorization.Succeeded)
                    return Results.Problem(
                                       new ValidationProblemDetails().ToValidationProblemDetails(
                                            "Unauthorized", "30550615-0007", _correlationId));

                var result = visaData.GetVisaByCustomerProfileId(
                    Convert.ToInt32(visaRequest.CustomerProfileId),
                    _correlationId);

                return Results.Ok(result.Value?.Select(visa => new Payloads.CommonPayloads.Visa(
                    visa.Id.ToString(),
                    visa.VisaNumber,
                    visa.CountryId.ToString(),
                    visa.IssuedPlace,
                    visa.IssuedDate.ToString(),
                    visa.ExpireDate.ToString(),
                    visa.Passport.IsPrimary.ToString(),
                    visa.Passport.PassportNumber,
                    visa.Country.CountryName
                )).ToArray()
                );
             
            }
            catch (Exception e)
            {
                e.LogException(_correlationId);
                return Results.Problem(
                                    new ValidationProblemDetails().ToValidationProblemDetails(
                                        "Sorry! Couldn't able to retrive VISA", "30550615-0008", _correlationId));
                
            }
        }

        public static IResult AddVisa([FromBody] VisaReqeust visaReqeust,
            SkyvaultContext dbContext,
            HttpContext context)
        {
            try
            {
                _correlationId = CorrelationHandler.Get(context);

                var visaData = new VisaData(dbContext);
                var customerProfileData = new CustomerProfileData(dbContext);

                var authorized = customerProfileData.CheckAccessToTheProfile(
                    Convert.ToInt32(visaReqeust.CustomerProfileId),
                    Convert.ToInt32(visaReqeust.SystemUserId),
                    _correlationId
                );

                if(!authorized.Succeeded)
                    return Results.Problem(
                                        new ValidationProblemDetails().ToValidationProblemDetails(
                                            authorized.Message, authorized.ErrorCode, _correlationId));


                var result = visaData.AddVisa(visaReqeust, _correlationId);

                if (!result.Succeeded)
                    return Results.Problem(
                                        new ValidationProblemDetails().ToValidationProblemDetails(
                                            result.Message, result.ErrorCode, _correlationId));

                return Results.Ok(new AddVISAResponse(result.Value));    
            }
            catch (Exception e)
            {
                e.LogException(_correlationId);
                return Results.Problem(
                                        new ProblemDetails().ToProblemDetails(
                                            "Sorry! Coudn't add VISA", "30550615-0009", _correlationId));
            }
            
        }

        public static IResult UpdateVisa([FromRoute] string visaId,[FromBody] VisaReqeust visaReqeust,
            SkyvaultContext dbContext,
            HttpContext context)
        {
            try
            {
                _correlationId = CorrelationHandler.Get(context);

                var visaData = new VisaData(dbContext);
                var customerProfileData = new CustomerProfileData(dbContext);

                var authorized = customerProfileData.CheckAccessToTheProfile(
                    Convert.ToInt32(visaReqeust.CustomerProfileId),
                    Convert.ToInt32(visaReqeust.SystemUserId),
                    _correlationId
                );

                if(!authorized.Succeeded)
                    return Results.Problem(
                                        new ValidationProblemDetails().ToValidationProblemDetails(
                                            authorized.Message, authorized.ErrorCode, _correlationId));
                    
                var result = visaData.UpdateVisa(visaId, visaReqeust, _correlationId);

                if (!result.Succeeded)
                    return Results.Problem(
                        new ValidationProblemDetails().ToValidationProblemDetails(
                        result.Message, result.ErrorCode, _correlationId));

                return Results.Ok(new SaveUpdateCustomerProfileResponse(visaReqeust.CustomerProfileId));
            }
            catch (Exception e)
            {
                e.LogException(_correlationId);
                return Results.Problem(
                                            new ProblemDetails().ToProblemDetails(
                                            "Sorry! Couldn't update VISA", "30550615-0010", _correlationId));
            }
            
        }

        public static IResult UpdateComMethod(
            [FromBody] ComMethodUpdateRequest comMethodRequest,
                       SkyvaultContext dbContext,
                                  HttpContext context)
        {
            try
            {
                _correlationId = CorrelationHandler.Get(context);

                var customerProfileData = new CustomerProfileData(dbContext);

                    var authorized = customerProfileData.CheckAccessToTheProfile(
                        Convert.ToInt32(comMethodRequest.CustomerProfileId),
                        Convert.ToInt32(comMethodRequest.SystemUserId),
                        _correlationId
                    );

                    if(!authorized.Succeeded)
                        return Results.Problem(
                                            new ValidationProblemDetails().ToValidationProblemDetails(
                                                authorized.Message, authorized.ErrorCode, _correlationId));

                var result = customerProfileData.UpdateComMethod(comMethodRequest, _correlationId);

                if (!result.Succeeded)
                    return Results.Problem(
                                        new ValidationProblemDetails().ToValidationProblemDetails(
                                                                result.Message, result.ErrorCode, _correlationId));

                return Results.Ok(new SaveUpdateCustomerProfileResponse(comMethodRequest.CustomerProfileId));
            }
            catch (Exception e)
            {
                
                e.LogException(_correlationId);
                return Results.Problem(
                                            new ProblemDetails().ToProblemDetails(
                                            "Sorry! Preffered commiunication method did not updated", "30550615-0011", _correlationId));
            }
            
        }

        public static IResult GetComMethod([FromBody] GetComMethodRequest comMethodRequest,
                       SkyvaultContext dbContext,
                                  HttpContext context) 
        {
            try
            {
                _correlationId = CorrelationHandler.Get(context);

                var customerProfileData = new CustomerProfileData(dbContext);

                var authorized = customerProfileData.CheckAccessToTheProfile(
                    Convert.ToInt32(comMethodRequest.CustomerProfileId),
                    Convert.ToInt32(comMethodRequest.SystemUserId),
                    _correlationId
                );

                if (!authorized.Succeeded)
                    return Results.Problem(
                                        new ValidationProblemDetails().ToValidationProblemDetails(
                                            authorized.Message, authorized.ErrorCode, _correlationId));

                var result = customerProfileData.GetComMethod(Convert.ToInt32(comMethodRequest.CustomerProfileId), _correlationId);

                if (!result.Succeeded)
                    return Results.Problem(
                                        new ValidationProblemDetails().ToValidationProblemDetails(
                                            result.Message, result.ErrorCode, _correlationId));

                return Results.Ok(result.Value);
            }
            catch (Exception e) 
            {
                e.LogException(_correlationId);
                return Results.Problem(
                                            new ProblemDetails().ToProblemDetails(
                                            "Sorry! Couldn't get preffered commiunication method", "30550615-0012", _correlationId));
            }

            
        }

        public static IResult DeleteVisa(
            [FromRoute] string visaId,
            [FromBody] DeleteVisaRequest deleteVisaRequest,
            SkyvaultContext dbContext,
            HttpContext context
            ) 
        {
            try
            {
                _correlationId = CorrelationHandler.Get(context);

                var visaData = new VisaData(dbContext);
                var customerProfileData = new CustomerProfileData(dbContext);

                if (int.TryParse(visaId, out int deletingVisa) &&
                    int.TryParse(deleteVisaRequest.CustomerProfileId, out int customerProfileId) &&
                    int.TryParse(deleteVisaRequest.SystemUserId, out int systemUser)
                    )
                {
                    var authorized = customerProfileData.CheckAccessToTheProfile(
                        customerProfileId,
                        systemUser,
                        _correlationId
                    );

                    if (!authorized.Succeeded)
                        return Results.Problem(
                                            new ValidationProblemDetails().ToValidationProblemDetails(
                                                authorized.Message, authorized.ErrorCode, _correlationId));

                    var result = visaData.DeleteVisa(deletingVisa, _correlationId);

                    if (!result.Succeeded)
                        return Results.Problem(
                                            new ValidationProblemDetails().ToValidationProblemDetails(
                                                result.Message, result.ErrorCode, _correlationId));

                    return Results.Ok("Visa deleted successfully");
                }

                return Results.Problem(
                                        new ValidationProblemDetails().ToValidationProblemDetails(
                                            "Invalid data found", "30550615-0013", _correlationId));
            }
            catch (Exception e)
            {
                e.LogException(_correlationId);
                return Results.Problem(
                                            new ProblemDetails().ToProblemDetails(
                                            "Sorry! Couldn't delete VISA", "30550615-0014", _correlationId));

            }          

        }

        public static IResult AddFFN([FromBody] FFNRequest ffnRequest, SkyvaultContext dbContext, HttpContext context) 
        {
            try
            {
                _correlationId = CorrelationHandler.Get(context);

                var ffnData = new FFNData(dbContext);

                var customerProfileData = new CustomerProfileData(dbContext);

                var authorized = customerProfileData.CheckAccessToTheProfile(
                       Convert.ToInt32(ffnRequest.CustomerProfileId),
                       Convert.ToInt32(ffnRequest.SystemUser),
                       _correlationId
                   );

                if (!authorized.Succeeded)
                    return Results.Problem(
                                        new ValidationProblemDetails().ToValidationProblemDetails(
                                            authorized.Message, authorized.ErrorCode, _correlationId));



                if (int.TryParse(ffnRequest.CustomerProfileId, out int cutomerId))
                {
                    var result = ffnData.AddFFN(cutomerId, ffnRequest.FFN, _correlationId);

                    if (!result.Succeeded)
                        return Results.Problem(
                                            new ValidationProblemDetails().ToValidationProblemDetails(
                                                result.Message, result.ErrorCode, _correlationId));

                    return Results.Ok(result.Value);
                }

                return Results.Problem(
                                        new ValidationProblemDetails().ToValidationProblemDetails(
                                            "Invalid data found", "30550615-0015", _correlationId));


            }
            catch (Exception e)
            {
                e.LogException(_correlationId);
                return Results.Problem(
                                            new ProblemDetails().ToProblemDetails(
                                            "Sorry! Couldn't add FFN", "30550615-0016", _correlationId));
            }

        }

        public static IResult UpdateFFN([FromRoute] string ffId, [FromBody] FFNRequest ffnRequest, SkyvaultContext dbContext, HttpContext context) 
        {
            try
            {
                _correlationId = CorrelationHandler.Get(context);

                var customerProfileData = new CustomerProfileData(dbContext);

                var authorized = customerProfileData.CheckAccessToTheProfile(
                       Convert.ToInt32(ffnRequest.CustomerProfileId),
                       Convert.ToInt32(ffnRequest.SystemUser),
                       _correlationId
                   );

                if (!authorized.Succeeded)
                    return Results.Problem(
                                        new ValidationProblemDetails().ToValidationProblemDetails(
                                            authorized.Message, authorized.ErrorCode, _correlationId));

                var ffnData = new FFNData(dbContext);

                if (int.TryParse(ffId, out int ffnId))
                {
                    var result = ffnData.UpdateFFN(ffnId, ffnRequest.FFN, _correlationId);

                    if (!result.Succeeded)
                        return Results.Problem(
                                            new ValidationProblemDetails().ToValidationProblemDetails(
                                                result.Message, result.ErrorCode, _correlationId));

                    return Results.Ok(result.Value);
                }

                return Results.Problem(
                                        new ValidationProblemDetails().ToValidationProblemDetails(
                                            "Invalid data found", "30550615-0017", _correlationId));
            }
            catch (Exception e)
            {
                e.LogException(_correlationId);
                return Results.Problem(
                                            new ProblemDetails().ToProblemDetails(
                                            "Sorry! Couldn't update FFN", "30550615-0018", _correlationId));

            }

        }

        public static IResult DeleteFFN([FromRoute] string ffId, [FromBody] FFNRequest ffnRequest, SkyvaultContext dbContext, HttpContext context) 
        {
            try
            {
                _correlationId = CorrelationHandler.Get(context);

                var customerProfileData = new CustomerProfileData(dbContext);

                var authorized = customerProfileData.CheckAccessToTheProfile(
                       Convert.ToInt32(ffnRequest.CustomerProfileId),
                       Convert.ToInt32(ffnRequest.SystemUser),
                       _correlationId
                   );

                if (!authorized.Succeeded)
                    return Results.Problem(
                                        new ValidationProblemDetails().ToValidationProblemDetails(
                                            authorized.Message, authorized.ErrorCode, _correlationId));

                var ffnData = new FFNData(dbContext);

                if (int.TryParse(ffId, out int ffnId))
                {
                    var result = ffnData.DeleteFFN(ffnId, _correlationId);

                    if (!result.Succeeded)
                        return Results.Problem(
                                            new ValidationProblemDetails().ToValidationProblemDetails(
                                                result.Message, result.ErrorCode, _correlationId));

                    return Results.Ok(result.Value);
                }

                return Results.Problem(
                                        new ValidationProblemDetails().ToValidationProblemDetails(
                                            "Invalid data found", "30550615-0019", _correlationId));
            }
            catch (Exception e)
            {
                e.LogException(_correlationId);
                return Results.Problem(
                                            new ProblemDetails().ToProblemDetails(
                                            "Sorry! Couldn't delete FFN", "30550615-0020", _correlationId));
            }

        }

        public static IResult GetFFNByCustomerId(
            [FromBody] FFNRequest ffnRequest, 
            SkyvaultContext dbContext, HttpContext context) 
        {
            try
            {
                _correlationId = CorrelationHandler.Get(context);

                var ffnData = new FFNData(dbContext);

                var customerProfileData = new CustomerProfileData(dbContext);

                var authorized = customerProfileData.CheckAccessToTheProfile(
                       Convert.ToInt32(ffnRequest.CustomerProfileId),
                       Convert.ToInt32(ffnRequest.SystemUser),
                       _correlationId
                   );

                if (!authorized.Succeeded)
                    return Results.Problem(
                                        new ValidationProblemDetails().ToValidationProblemDetails(
                                            authorized.Message, authorized.ErrorCode, _correlationId));

                if (int.TryParse(ffnRequest.CustomerProfileId, out int custId))
                {
                    var result = ffnData.GetFFNByCustomerId(custId, _correlationId);

                    if (!result.Succeeded)
                        return Results.Problem(
                                            new ValidationProblemDetails().ToValidationProblemDetails(
                                                result.Message, result.ErrorCode, _correlationId));

                    return Results.Ok(result.Value);
                }

                return Results.Problem(
                                        new ValidationProblemDetails().ToValidationProblemDetails(
                                            "Invalid data found", "30550615-0021", _correlationId));
            }
            catch (Exception e)
            {
                e.LogException(_correlationId);
                return Results.Problem(
                                            new ProblemDetails().ToProblemDetails(
                                            "Sorry! Couldn't get FFN", "30550615-0022", _correlationId));
            }

        }

        public static IResult GetFamilyMembers([FromBody] GetFamilyMembersRequest request, 
            SkyvaultContext dbContext, 
            HttpContext context) 
        {
            _correlationId = CorrelationHandler.Get(context);
            var customerProfileData = new CustomerProfileData(dbContext);

            var authorized = customerProfileData.CheckAccessToTheProfile(
                   Convert.ToInt32(request.CustomerProfileId),
                   Convert.ToInt32(request.SystemUserId),
                   _correlationId
            );

            if (!authorized.Succeeded)
                return Results.Problem(
                                    new ValidationProblemDetails().ToValidationProblemDetails(
                                        authorized.Message, authorized.ErrorCode, _correlationId));


            if (int.TryParse(request.CustomerProfileId, out int custId)) 
            {

                var result = customerProfileData.GetFamilyMembers(custId, _correlationId);

                return Results.Ok(result.Value);

            }

            return Results.Problem(
                                        new ValidationProblemDetails().ToValidationProblemDetails(
                                            "Invalid data found", "30550615-0021", _correlationId));


        }
        #endregion

        #region Private Methods

        private static bool IsPassportDataValid(PassportRequest passportRequest)
        {
            if (
                String.IsNullOrEmpty(passportRequest.CountryId) &&
                String.IsNullOrEmpty(passportRequest.DateOfBirth) &&
                String.IsNullOrEmpty(passportRequest.ExpiryDate) &&
                String.IsNullOrEmpty(passportRequest.PassportNumber) &&
                passportRequest.PassportNumber?.Length < 6 &&
                String.IsNullOrEmpty(passportRequest.OtherNames) &&
                passportRequest.OtherNames?.Length < 3 &&
                String.IsNullOrEmpty(passportRequest.LastName) &&
                passportRequest.LastName?.Length < 3 &&
                String.IsNullOrEmpty(passportRequest.Gender) &&
                String.IsNullOrEmpty(passportRequest.SalutationId) &&
                String.IsNullOrEmpty(passportRequest.SystemUserId)
                ) return false;

            return true;
        }
        #endregion

    }
}