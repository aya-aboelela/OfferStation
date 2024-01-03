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
    public class ApplicationUserProfile : Profile
    {
        public ApplicationUserProfile()
        {

            CreateMap<CustomerRegestrationDto,ApplicationUser>()
                .ForMember(ob=>ob.UserName,dest=> dest.MapFrom(src=>src.Email))
                .ReverseMap();
            
            CreateMap<SupplierRegestrationDto, ApplicationUser>()
                .ForMember(ob=>ob.UserName,dest=> dest.MapFrom(src=>src.Email))
                .ReverseMap();

            CreateMap<OwnerRegestrationDto, ApplicationUser>()
                .ForMember(ob=>ob.UserName,dest=> dest.MapFrom(src=>src.Email))
                .ReverseMap();
        
        }
    }
}
