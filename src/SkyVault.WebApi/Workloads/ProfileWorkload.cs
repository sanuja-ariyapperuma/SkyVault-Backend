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

                var userIdentifyer = context.User.Identity!.Name;

                _correlationId = CorrelationHandler.Get(context);

                var customerProfileData = new CustomerProfileData(dbContext);

                var customerProfiles = customerProfileData.Search(
                    searchProfileRequest.SearchQuery!,
                    userIdentifyer!);

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
                SaveUpdateCustomerProfileResponse savedCustomerProfile;
                var customerProfileData = new CustomerProfileData(dbContext);
                var systemUserData = new SystemUserData(dbContext);

                if (String.IsNullOrEmpty(passportRequest.CustomerProfileId?.Trim())) 
                {


                    var userId = (String.IsNullOrEmpty(passportRequest.ParentId?.Trim())) ? 
                        GetUserId(context, systemUserData) : 
                        GetUserIdForCustomerProfile(context, systemUserData, customerProfileData, passportRequest.ParentId);

                    var validatePassportRequest = IsPassportDataValid(passportRequest);

                    if (!validatePassportRequest.Succeeded)
                        return Results.BadRequest(validatePassportRequest.Message);  

                    var checkPassportExists = customerProfileData.CheckPassportExists(passportRequest.PassportNumber ?? "");

                    if (checkPassportExists)
                        return Results.BadRequest("Passport number is already existing");

                    var savedprofile = customerProfileData.SaveProfile(passportRequest, userId, _correlationId);
                    
                    savedCustomerProfile = new SaveUpdateCustomerProfileResponse(
                        savedprofile.Value!.Id.ToString(),
                        savedprofile.Value.Passports.First().Id.ToString());

                    return Results.Created<SaveUpdateCustomerProfileResponse>("AddPassport", savedCustomerProfile);
                }
                else 
                {
                    var customer_profile_data = new CustomerProfileData(dbContext);

                    GetUserIdForCustomerProfile(
                        context,
                        systemUserData,
                        customer_profile_data,
                        passportRequest.CustomerProfileId
                    );

                    var passportData = new PassportData(dbContext);
                    var savepassport = passportData.AddNewPassport(passportRequest, _correlationId);

                    savedCustomerProfile = new SaveUpdateCustomerProfileResponse(
                        passportRequest.CustomerProfileId,
                        savepassport.Value?.Id.ToString()
                        );

                    return Results.Ok(savedCustomerProfile);
                }

                
            }
            catch (Exception e)
            {
                e.LogException(_correlationId);

                return Results.Problem(new ValidationProblemDetails().ToValidationProblemDetails(
                    "Sorry! Couldn't able to update or save passport", "30550615-0003", _correlationId
                ));
            }
        }

        public static IResult GetPassportsByCustomerProfileId(
            [FromRoute] string profileId,
            SkyvaultContext dbContext,
            HttpContext context
        )
        {
            try
            {
            
                _correlationId = CorrelationHandler.Get(context);

                var passportData = new PassportData(dbContext);
                var customer_profile_data = new CustomerProfileData(dbContext);
                var systemUserData = new SystemUserData(dbContext);

                GetUserIdForCustomerProfile(
                    context,
                    systemUserData,
                    customer_profile_data,
                    profileId);

                var passport = passportData.GetPassportByCustomerProfileId(Convert.ToInt32(profileId), _correlationId);

                var passportResult = passport.Value?.Select(p => new PassportResponse(
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
                        p.CustomerProfile.SalutationId.ToString(),
                        getPassportCode(p)
                ));

                return Results.Ok(passportResult);

            }
            catch (Exception e)
            {
                
                e.LogException(_correlationId);
                return Results.Problem(
                                        new ProblemDetails().ToProblemDetails(
                                            "Sorry! Couldn't able to get passports", "30550615-0005", _correlationId));
            }
            

        }

        private static string getPassportCode(Passport p) 
        {
            return String.Concat(
                            "SR DOCS YY HK1-P-",
                            p.Country.CountryCode.ToUpper(), "-",
                            p.PassportNumber, "-",
                            p.PlaceOfBirth, "-",
                            convertDateToCustomFormat(p.DateOfBirth), "-",
                            p.Gender, "-",
                            convertDateToCustomFormat(p.ExpiryDate), "-",
                            p.LastName.ToUpper(), "-",
                            p.OtherNames.ToUpper().Replace(" ", "-"), "-", "H/P1"
                        );
        }

        private static string getVisaCode(Backend.Models.Visa visa) 
        {
            return String.Concat(
                        "SR DOCO YY HK1", "-",
                        visa.Passport.Country.CountryCode, "-",
                        "V", "-",
                        visa.VisaNumber, "-",
                        visa.IssuedPlace, "-",
                        convertDateToCustomFormat(visa.IssuedDate), "-",
                        visa.Country.CountryCode, "-",
                        convertDateToCustomFormat(visa.ExpireDate)
                    );
        }

        private static string convertDateToCustomFormat(DateOnly date) => date.ToString("ddMMMyy").ToUpper();
        

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

                var systemUserData = new SystemUserData(dbContext);

                GetUserIdForCustomerProfile(
                    context,
                    systemUserData,
                    customerProfileData,
                    passportRequest.CustomerProfileId!);

                var result = passportData.UpdatePassport(passportRequest, _correlationId);

                if (!result.Succeeded)
                    return Results.Problem(
                        new ValidationProblemDetails().ToValidationProblemDetails(
                        result.Message, result.ErrorCode, _correlationId)
                    );

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
                [FromRoute] string profileId,
                SkyvaultContext dbContext,
                HttpContext context) 
        {
            try
            {
                _correlationId = CorrelationHandler.Get(context);

                var visaData = new VisaData(dbContext);
                var customer_profile_data = new CustomerProfileData(dbContext);
                var systemUserData = new SystemUserData(dbContext);

                GetUserIdForCustomerProfile(
                    context,
                    systemUserData,
                    customer_profile_data,
                    profileId);

                var result = visaData.GetVisaByCustomerProfileId(
                    Convert.ToInt32(profileId),
                    _correlationId);

                var response = result.Value?.Select(visa => new Payloads.CommonPayloads.Visa(
                    visa.Id.ToString(),
                    visa.VisaNumber,
                    visa.CountryId.ToString(),
                    visa.IssuedPlace,
                    visa.IssuedDate.ToString(),
                    visa.ExpireDate.ToString(),
                    visa.Passport.IsPrimary.ToString(),
                    visa.Passport.PassportNumber,
                    visa.Country.CountryName,
                    getVisaCode(visa)
                )).ToArray();

                return Results.Ok(response);
             
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

                var systemUserData = new SystemUserData(dbContext);

                GetUserIdForCustomerProfile(
                    context,
                    systemUserData,
                    customerProfileData,
                    visaReqeust.CustomerProfileId!);

                var result = visaData.AddVisa(visaReqeust, _correlationId);

                if (!result.Succeeded)
                    return Results.Problem(
                                        new ValidationProblemDetails().ToValidationProblemDetails(
                                            result.Message, result.ErrorCode, _correlationId));

                return Results.Ok(new AddVISAResponse(result.Value!));    
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

                var systemUserData = new SystemUserData(dbContext);

                int userId = GetUserIdForCustomerProfile(
                                context,
                                systemUserData,
                                customerProfileData,
                                visaReqeust.CustomerProfileId!
                             );
                    
                var result = visaData.UpdateVisa(visaId, visaReqeust, userId, _correlationId);

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

                var systemUserData = new SystemUserData(dbContext);

                GetUserIdForCustomerProfile(
                    context,
                    systemUserData,
                    customerProfileData,
                    comMethodRequest.CustomerProfileId!
                );

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

        public static IResult GetCommMethod(
                        [FromBody] GetComMethodRequest comReqeust,
                        SkyvaultContext dbContext,
                        HttpContext context) 
        {
            try
            {
                _correlationId = CorrelationHandler.Get(context);

                var customerProfileData = new CustomerProfileData(dbContext);
                var systemUserData = new SystemUserData(dbContext);

                GetUserIdForCustomerProfile(
                    context,
                    systemUserData,
                    customerProfileData,
                    comReqeust.CustomerProfileId
                );

                var result = customerProfileData.GetComMethod(Convert.ToInt32(comReqeust.CustomerProfileId), _correlationId);

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
            SkyvaultContext dbContext,
            HttpContext context
            ) 
        {
            try
            {
                _correlationId = CorrelationHandler.Get(context);

                var visaData = new VisaData(dbContext);


                if (int.TryParse(visaId, out int deletingVisa))
                {

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

                var systemUserData = new SystemUserData(dbContext);

                GetUserIdForCustomerProfile(
                    context,
                    systemUserData,
                    customerProfileData,
                    ffnRequest.CustomerProfileId!
                );

                if (int.TryParse(ffnRequest.CustomerProfileId, out int cutomerId))
                {
                    var result = ffnData.AddFFN(cutomerId, ffnRequest.FFN!, _correlationId);

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

                var systemUserData = new SystemUserData(dbContext);

                GetUserIdForCustomerProfile(
                    context,
                    systemUserData,
                    customerProfileData,
                    ffnRequest.CustomerProfileId!
                );

                var ffnData = new FFNData(dbContext);

                if (int.TryParse(ffId, out int ffnId))
                {
                    var result = ffnData.UpdateFFN(ffnId, ffnRequest.FFN!, _correlationId);

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

                var systemUserData = new SystemUserData(dbContext);

                GetUserIdForCustomerProfile(
                    context,
                    systemUserData,
                    customerProfileData,
                    ffnRequest.CustomerProfileId!
                );

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
            [FromRoute] string profileId, 
            SkyvaultContext dbContext, HttpContext context) 
        {
            try
            {
                _correlationId = CorrelationHandler.Get(context);

                var ffnData = new FFNData(dbContext);

                var customerProfileData = new CustomerProfileData(dbContext);
                var systemUserData = new SystemUserData(dbContext);

                GetUserIdForCustomerProfile(
                    context,
                    systemUserData,
                    customerProfileData,
                    profileId
                );

                if (int.TryParse(profileId, out int custId))
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

        public static IResult GetFamilyMembers([FromRoute] string profileId, 
            SkyvaultContext dbContext, 
            HttpContext context) 
        {
            _correlationId = CorrelationHandler.Get(context);
            var customerProfileData = new CustomerProfileData(dbContext);

            if (int.TryParse(profileId, out int custId)) 
            {
                var systemUserData = new SystemUserData(dbContext);

                GetUserIdForCustomerProfile(
                    context,
                    systemUserData,
                    customerProfileData,
                    profileId
                );

                var result = customerProfileData.GetFamilyMembers(custId, _correlationId);

                return Results.Ok(result.Value);

            }

            return Results.Problem(
                                        new ValidationProblemDetails().ToValidationProblemDetails(
                                            "Invalid data found", "30550615-0021", _correlationId));


        }
        #endregion

        #region Private Methods

        private static SkyResult<bool> IsPassportDataValid(PassportRequest passportRequest)
        {
            if (String.IsNullOrEmpty(passportRequest.CountryId)) 
            {
                return new SkyResult<bool>().Fail("Country Id is required", "", "");
            } else if(String.IsNullOrEmpty(passportRequest.DateOfBirth))
            {
                return new SkyResult<bool>().Fail("Date of Birth is required", "", "");
            }
            else if (String.IsNullOrEmpty(passportRequest.ExpiryDate))
            {
                return new SkyResult<bool>().Fail("Expiry Date is required", "", "");
            }
            else if (String.IsNullOrEmpty(passportRequest.PassportNumber))
            {
                return new SkyResult<bool>().Fail("Passport Number is required", "", "");
            }
            else if (passportRequest.PassportNumber?.Length < 8)
            {
                return new SkyResult<bool>().Fail("Passport Number should be atleast 8 characters", "", "");
            }
            else if (String.IsNullOrEmpty(passportRequest.OtherNames))
            {
                return new SkyResult<bool>().Fail("Other Names is required", "", "");
            }
            else if (passportRequest.OtherNames?.Length < 3)
            {
                return new SkyResult<bool>().Fail("Other Names should be atleast 3 characters", "", "");
            }
            else if (String.IsNullOrEmpty(passportRequest.LastName))
            {
                return new SkyResult<bool>().Fail("Last Name is required", "", "");
            }
            else if (passportRequest.LastName?.Length < 3)
            {
                return new SkyResult<bool>().Fail("Last Name should be atleast 3 characters", "", "");
            }

            return new SkyResult<bool>().SucceededWithValue(true);
        }

        private static int GetUserIdForCustomerProfile(
            HttpContext context, 
            SystemUserData systemUserData, 
            CustomerProfileData customerProfileData,
            string customerProfileId
            )
        {
            var userUpn = context.User.Identity!.Name;

            if (String.IsNullOrEmpty(userUpn))
                throw new UnauthorizedAccessException("Unauthorized");

            var userId = systemUserData.GetUserIdByUpn(userUpn, _correlationId);

            if (!userId.Succeeded)
                throw new UnauthorizedAccessException("Unauthorized");

            var authorized = customerProfileData.CheckAccessToTheProfile(
                        Convert.ToInt32(customerProfileId),
                        Convert.ToInt32(userId.Value),
                        _correlationId
                    );

            if (!authorized.Succeeded)
                throw new UnauthorizedAccessException("Unauthorized");

            return systemUserData.GetUserIdByUpn(userUpn, _correlationId).Value;
        }

        private static int GetUserId(
            HttpContext context,
            SystemUserData systemUserData
            ) 
        {
            var userUpn = context.User.Identity!.Name;

            if (String.IsNullOrEmpty(userUpn))
                throw new UnauthorizedAccessException("Unauthorized");

            var userId = systemUserData.GetUserIdByUpn(userUpn, _correlationId);

            return userId.Value;
        }

        #endregion

    }
}