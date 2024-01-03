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
    public class OwnerProfile : Profile
    {
        public OwnerProfile()
        {
            CreateMap<Owner, PublicInfoDto>()
                .ForMember(des => des.Name, a => a.MapFrom(src => src.AppUser.Name))
                .ForMember(des => des.Email, a => a.MapFrom(src => src.AppUser.Email))
                .ForMember(des => des.PhoneNumber, a => a.MapFrom(src => src.AppUser.PhoneNumber))
                .ReverseMap();

            CreateMap<OwnerProduct, ProductInfoDto>()
                .ForMember(des => des.DiscountPrice, a => a.MapFrom(src =>  (src.Price - ((src.Price * src.Discount)/100))))
                .ReverseMap();

            CreateMap<Owner, OwnerDto>()
                .ForMember(des => des.Name, a => a.MapFrom(src => src.AppUser.Name))
                .ForMember(des => des.Addresses, a => a.MapFrom(src => src.AppUser.Addresses))
                .ReverseMap();

            CreateMap<Owner, TraderDetailsDto>()
               .ForMember(des => des.Name, a => a.MapFrom(src => src.AppUser.Name))
               .ForMember(des => des.Email, a => a.MapFrom(src => src.AppUser.Email))
               .ForMember(des => des.PhoneNumber, a => a.MapFrom(src => src.AppUser.PhoneNumber))
               .ReverseMap();

            CreateMap<ProductDto, OwnerProduct>()
                .ForMember(des => des.CreatedTime, a => a.MapFrom(src => DateTime.Now))
                .ReverseMap();

            CreateMap<OwnerReview, ReviewDto>()
                .ForMember(des => des.PersonName, a => a.MapFrom(src => src.Owner.AppUser.Name))
                .ReverseMap();

            CreateMap<OfferDto, OwnerOffer>()
                .ForMember(des => des.CreatedTime, a => a.MapFrom(src => DateTime.Now))
                .ReverseMap();
            
            CreateMap<OwnerOffer, OfferDetailsDto>()
                .ForMember(des => des.TraderImage, a => a.MapFrom(src => src.Owner.Image))
                .ReverseMap();

            CreateMap<OwnerOfferProduct, OfferProductDto>();
            CreateMap<OwnerMenuCategory, MenuCategoryDto>().ReverseMap();
            CreateMap<OwnerMenuCategory, MenuCategoryDetailsDto>().ReverseMap();
            CreateMap<OwnerReview, ReviewInfoDto>()
                .ForMember(des => des.CreatedTime, a => a.MapFrom(src => DateTime.Now))
                .ReverseMap();
            CreateMap<OfferInfoDto, OwnerOffer>()
                .ForMember(o => o.Products, opt => opt.Ignore());
            CreateMap<Owner, OwnerRegestrationDto>().ReverseMap();
            CreateMap<OwnerCategory, CategoryDto>().ReverseMap();
            CreateMap<OwnerCategory, CategoryInfoDto>().ReverseMap();





            CreateMap<OwnerCart, OwnerCartDto>()
                .ForMember(des => des.SupplierName, a => a.MapFrom(src => src.Supplier.AppUser.Name))
                .ForMember(des => des.SupplierId, a => a.MapFrom(src => src.Supplier.Id))
                //.ForMember(des => des.Total, a => a.MapFrom(src => src.OwnerOffer.Price * src.Quantity))
                .ReverseMap();

            CreateMap<OwnerCartProduct, OwnerCartProductDto>()
                .ForMember(des => des.ProductName, a => a.MapFrom(src => src.SupplierProduct.Name))
                //.ForMember(des => des.Total, a => a.MapFrom(src => src.OwnerProduct.Price * src.Quantity))
                .ReverseMap();

            CreateMap<OwnerCartOffer, OwnerCartOfferDto>()
                .ForMember(des => des.OfferName, a => a.MapFrom(src => src.SupplierOffer.Name))
                //.ForMember(des => des.Total, a => a.MapFrom(src => src.OwnerOffer.Price * src.Quantity))
                .ReverseMap();
        }
    }
}
