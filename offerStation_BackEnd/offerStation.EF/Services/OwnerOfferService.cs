    using AutoMapper;
using offerStation.Core.Dtos;
using offerStation.Core.Interfaces;
using offerStation.Core.Interfaces.Services;
using offerStation.Core.Models;
using OrderStation.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace offerStation.EF.Services
{
    public class OwnerOfferService : IOwnerOfferService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private OwnerOfferProduct ownerOfferProduct;
        public OwnerOfferService(IMapper mapper, IUnitOfWork unitOfWork) 
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<OfferDetailsDto?> GetOfferDetails(int id)
        {
            OfferDetailsDto offerDto = null;

            OwnerOffer offer = await _unitOfWork.OwnerOffers.FindAsync(o => o.Id == id && !o.IsDeleted, 
                new List<Expression<Func<OwnerOffer, object>>>()
                {
                    o => o.Owner,
                    o => o.Products,
                });

            if(offer is not null)
            {
                offerDto = _mapper.Map<OfferDetailsDto>(offer);
            }
            return offerDto;
        }
        public async Task<List<OfferDetailsDto>?> GetAllOffersByOwnerId(int ownerId)
        {
            List<OfferDetailsDto> offersDtoList = null;

            IEnumerable<OwnerOffer> offersList = await _unitOfWork.OwnerOffers.FindAllAsync(o => o.OwnerId == ownerId && !o.IsDeleted,
                new List<Expression<Func<OwnerOffer, object>>>()
                {
                    o => o.Owner,
                    o => o.Products,
                });


            if (offersList is not null)
            {
                offersDtoList = _mapper.Map<List<OfferDetailsDto>>(offersList);
            }
            return offersDtoList;
        }
        public async Task<bool> AddOffer(int ownerId, OfferInfoDto offerDto)
        {
            if (offerDto is not null)
            {
                OwnerOffer Offer = new OwnerOffer();
                Offer = _mapper.Map<OwnerOffer>(offerDto);

                Offer.OwnerId = ownerId;
                Offer.CreatedTime = DateTime.Now;

                _unitOfWork.OwnerOffers.Add(Offer);
                _unitOfWork.Complete();

                Offer.Products = await AddOfferProducts(Offer.Id, offerDto.Products);
                
                _unitOfWork.OwnerOffers.Update(Offer);
                _unitOfWork.Complete();
                
                return true;
            }
            return false;
        }
        public async Task<bool> EditOffer(int id, OfferInfoDto offerDto)
        {
            OwnerOffer offer = await _unitOfWork.OwnerOffers.GetByIdAsync(id);

            if (offer is not null && offerDto is not null)
            {
                offer.Name = offerDto.Name;
                offer.Price = offerDto.Price;
                offer.Image = offerDto.Image;
                offer.Description = offerDto.Description;

                _unitOfWork.OwnerOffers.Update(offer);
                _unitOfWork.Complete();

                return true;
            }
            return false;
        }
        public async Task<bool> DeleteOffer(int id)
        {
            OwnerOffer offer = await _unitOfWork.OwnerOffers.GetByIdAsync(id);

            if (offer is not null)
            {
                _unitOfWork.OwnerOffers.Delete(offer);
                _unitOfWork.Complete();

                return true;
            }
            return false;
        }
        private async Task<List<OwnerOfferProduct>?> AddOfferProducts(int offerId, List<OfferProductDto> offerProductList)
        {
            List<OwnerOfferProduct> ownerOfferProducts = null;

            if (offerProductList is not null)
            {
                ownerOfferProducts = new();

                foreach(var offerProduct in offerProductList)
                {
                    ownerOfferProduct = new ()
                    { 
                        OfferId = offerId,
                        Quantity = offerProduct.Quantity,
                        ProductId = offerProduct.ProductId
                    };

                    _unitOfWork.OwnerOfferProducts.Add(ownerOfferProduct);
                    _unitOfWork.Complete();

                    ownerOfferProducts.Add(ownerOfferProduct);
                }          
            }
            return ownerOfferProducts;
        }
    }
}
