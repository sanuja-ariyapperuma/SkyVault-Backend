using AutoMapper;
using SkyVault.Payloads.ResponsePayloads;
using SkyVault.WebApi.Backend.Models;

namespace SkyVault.WebApi.MappingProfiles
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<Backend.Models.Salutation, Payloads.ResponsePayloads.Salutation>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.SalutationName));

            CreateMap<Backend.Models.Nationality,  Payloads.ResponsePayloads.Nationality>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.NationalityName));

            CreateMap<Backend.Models.Country, Payloads.ResponsePayloads.Country>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.CountryName));

            CreateMap<Backend.Gender, Payloads.ResponsePayloads.Gender>();
        }
    }
}
