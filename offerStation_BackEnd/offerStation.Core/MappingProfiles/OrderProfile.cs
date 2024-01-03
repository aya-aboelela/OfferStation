using AutoMapper;
using offerStation.Core.Constants;
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
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<OwnerOrder, OrderDto>()
                .ForMember(des => des.TraderName, a => a.MapFrom(src => src.Supplier.AppUser.Name))
                .ForMember(des => des.RequesterName, a => a.MapFrom(src => src.Owner.AppUser.Name))
                .ForMember(des => des.AdminTotal, a => a.MapFrom(src => src.Total * (Const.Fee / 100)))
                .ReverseMap();
            CreateMap<OwnerOrder, OrderDetailsDto>()
                .ForMember(des => des.TraderId, a => a.MapFrom(src => src.SupplierId))
                .ForMember(des => des.TraderName, a => a.MapFrom(src => src.Supplier.AppUser.Name))
                .ReverseMap();
            CreateMap<OwnerOrder, RequestedOrderDto>()
                .ForMember(des => des.RequesterId, a => a.MapFrom(src => src.OwnerId))
                .ForMember(des => des.RequesterName, a => a.MapFrom(src => src.Owner.AppUser.Name))
                .ForMember(des => des.NetTotal, a => a.MapFrom(src => src.Total - (src.Total * (Const.Fee / 100))));

            CreateMap<OwnerOrderProduct, OrderProductDto>()
                .ForMember(des => des.TraderProductId, a => a.MapFrom(src => src.SupplierProductId))
                .ReverseMap();
            CreateMap<OwnerOrderOffer, OrderOfferDto>()
                .ForMember(des => des.TraderOfferId, a => a.MapFrom(src => src.SupplierOffertId))
                .ReverseMap();

            CreateMap<CustomerOrder, OrderDto>()
                .ForMember(des => des.TraderName, a => a.MapFrom(src => src.Owner.AppUser.Name))
                .ForMember(des => des.RequesterName, a => a.MapFrom(src => src.Customer.AppUser.Name))
                .ForMember(des => des.AdminTotal, a => a.MapFrom(src => src.Total * (Const.Fee / 100)))
                .ReverseMap();
            CreateMap<CustomerOrder, OrderDetailsDto>()
                .ForMember(des => des.TraderId, a => a.MapFrom(src => src.OwnerId))
                .ForMember(des => des.TraderName, a => a.MapFrom(src => src.Owner.AppUser.Name))
                .ForMember(des => des.Total, a => a.MapFrom(src => src.Total))
                .ReverseMap();
            CreateMap<CustomerOrder, RequestedOrderDto>()
                .ForMember(des => des.RequesterId, a => a.MapFrom(src => src.CustomerId))
                .ForMember(des => des.RequesterName, a => a.MapFrom(src => src.Customer.AppUser.Name))
                .ForMember(des => des.NetTotal, a => a.MapFrom(src => src.Total - (src.Total * (Const.Fee / 100))))
                .ReverseMap();

            CreateMap<CustomerOrderProduct, OrderProductDto>()
                .ForMember(des => des.TraderProductId, a => a.MapFrom(src => src.OwnerProductId))
                .ReverseMap();
            CreateMap<CustomerOrderOffer, OrderOfferDto>()
                .ForMember(des => des.TraderOfferId, a => a.MapFrom(src => src.OwnerOffertId))
                .ReverseMap();
        }
    }
}
