using AutoMapper;
using offerStation.Core.Dtos;
using offerStation.Core.Models;
using OrderStation.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace offerStation.Core.MappingProfiles
{
    public class SupplierProfile : Profile
    {
        public SupplierProfile()
        {
            CreateMap<Supplier, PublicInfoDto>()
                .ForMember(des => des.Name, a => a.MapFrom(src => src.AppUser.Name))
                .ForMember(des => des.Email, a => a.MapFrom(src => src.AppUser.Email))
                .ForMember(des => des.PhoneNumber, a => a.MapFrom(src => src.AppUser.PhoneNumber))
                .ReverseMap();

            CreateMap<Supplier, TraderDetailsDto>()
              .ForMember(des => des.Name, a => a.MapFrom(src => src.AppUser.Name))
              .ForMember(des => des.Email, a => a.MapFrom(src => src.AppUser.Email))
              .ForMember(des => des.PhoneNumber, a => a.MapFrom(src => src.AppUser.PhoneNumber))
              .ReverseMap();

            CreateMap<ProductDto, SupplierProduct>()
                .ForMember(des => des.CreatedTime, a => a.MapFrom(src => DateTime.Now))
                .ReverseMap();

            CreateMap<SupplierProduct, ProductInfoDto>()
               .ForMember(des => des.DiscountPrice, a => a.MapFrom(src => (src.Price - ((src.Price * src.Discount) / 100))))
               .ReverseMap();

            CreateMap<Supplier, SupplierDto>()
               .ForMember(des => des.Name, a => a.MapFrom(src => src.AppUser.Name))
               .ForMember(des => des.Addresses, a => a.MapFrom(src => src.AppUser.Addresses))
               .ReverseMap();

            CreateMap<OfferDto, SupplierOffer>()
                .ForMember(des => des.CreatedTime, a => a.MapFrom(src => DateTime.Now))
                .ReverseMap();

            CreateMap<SupplierOffer, OfferDetailsDto>()
                .ForMember(des => des.TraderImage, a => a.MapFrom(src => src.Supplier.Image))
                .ReverseMap();

            CreateMap<SupplierOfferProduct, OfferProductDto>();
            CreateMap<SupplierMenuCategory, MenuCategoryDto>().ReverseMap();
            CreateMap<SupplierMenuCategory, MenuCategoryDetailsDto>().ReverseMap();
            CreateMap<OfferInfoDto, SupplierOffer>()
                .ForMember(o => o.Products, opt => opt.Ignore());
            CreateMap<Supplier, SupplierRegestrationDto>().ReverseMap();
            CreateMap<SupplierCategory, CategoryDto>().ReverseMap();
            CreateMap<SupplierCategory, CategoryInfoDto>().ReverseMap();
        }
    }
}
