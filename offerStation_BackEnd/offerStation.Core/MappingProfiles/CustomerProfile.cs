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
    public class CustomerProfile:Profile
    {
        public CustomerProfile()
        {
            CreateMap<Customer, CustomerRegestrationDto>().ReverseMap();
            CreateMap<Customer, CustomerInfoDto>()
                .ForMember(des => des.Name, a => a.MapFrom(src => src.AppUser.Name))
                .ForMember(des => des.PhoneNumber, a => a.MapFrom(src => src.AppUser.PhoneNumber))
                .ForMember(des => des.Email, a => a.MapFrom(src => src.AppUser.Email))
                .ReverseMap();

            CreateMap<CustomerReview, ReviewInfoDto>()
                .ForMember(des => des.CreatedTime, a => a.MapFrom(src => DateTime.Now))
                .ReverseMap();
            
            CreateMap<CustomerReview, ReviewDto>()
                .ForMember(des => des.PersonName, a => a.MapFrom(src => src.Customer.AppUser.Name))
                .ReverseMap();


            CreateMap<CustomerCart, CustomerCartDto>()
                .ForMember(des => des.OwnerName, a => a.MapFrom(src => src.Owner.AppUser.Name))
                .ForMember(des => des.OwnerId, a => a.MapFrom(src => src.Owner.Id))
                //.ForMember(des => des.Total, a => a.MapFrom(src => src.OwnerOffer.Price * src.Quantity))
                .ReverseMap();

            CreateMap<CustomerCartProduct, CustomerCartProductDto>()
                .ForMember(des => des.ProductName, a => a.MapFrom(src => src.OwnerProduct.Name))
                //.ForMember(des => des.Total, a => a.MapFrom(src => src.OwnerProduct.Price * src.Quantity))
                .ReverseMap();

            CreateMap<CustomerCartOffer, CustomerCartOfferDto>()
                .ForMember(des => des.OfferName, a => a.MapFrom(src => src.OwnerOffer.Name))
                //.ForMember(des => des.Total, a => a.MapFrom(src => src.OwnerOffer.Price * src.Quantity))
                .ReverseMap();
        }
    }
}
