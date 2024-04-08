using AutoMapper;
using SkyVault.Payloads.ResponsePayloads;
using SkyVault.WebApi.Backend.Models;

namespace SkyVault.WebApi.MappingProfiles
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<Salutation, Payloads.CommonPayloads.Salutation>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.SalutationName));

            CreateMap<Nationality,  Payloads.CommonPayloads.Nationality>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.NationalityName));

            CreateMap<Country, Payloads.CommonPayloads.Country>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.CountryName));

            CreateMap<Gender, Payloads.CommonPayloads.Gender>();
        }
    }
}
