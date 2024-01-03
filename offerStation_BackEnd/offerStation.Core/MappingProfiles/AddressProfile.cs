using AutoMapper;
using offerStation.Core.Dtos;
using offerStation.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace offerStation.Core.MappingProfiles
{
    public class AddressProfile : Profile
    {
        public AddressProfile()
        {
            CreateMap<Address, AddressDTO>().ReverseMap();
            CreateMap<Address, AddressCityNameDto>()
                .ForMember(des => des.CityName, a => a.MapFrom(src => src.City.Name))
                .ReverseMap();
            CreateMap<City, CityDto>().ReverseMap();
        }
    }
}
