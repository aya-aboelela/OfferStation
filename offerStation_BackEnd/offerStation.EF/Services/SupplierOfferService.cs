using AutoMapper;
using offerStation.Core.Dtos;
using offerStation.Core.Interfaces;
using offerStation.Core.Interfaces.Services;
using offerStation.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace offerStation.EF.Services
{
    public class SupplierOfferService : ISupplierOfferService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private SupplierOfferProduct supplierOfferProduct;

        public SupplierOfferService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<OfferDetailsDto?> GetOfferDetails(int id)
        {
            OfferDetailsDto offerDto = null;

            SupplierOffer offer = await _unitOfWork.SupplierOffers.FindAsync(o => o.Id == id && !o.IsDeleted,
                new List<Expression<Func<SupplierOffer, object>>>()
                {
                    o => o.Supplier,
                    o => o.Products,    
                });

            if (offer is not null)
            {
                offerDto = _mapper.Map<OfferDetailsDto>(offer);
            }
            return offerDto;
        }
        public async Task<List<OfferDetailsDto>?> GetAllOffersBySupplierId(int supplierId)
        {
            List<OfferDetailsDto> offersDtoList = null;

            IEnumerable<SupplierOffer> offersList = await _unitOfWork.SupplierOffers.FindAllAsync(s => s.SupplierID == supplierId && !s.IsDeleted,
                new List<Expression<Func<SupplierOffer, object>>>()
                {
                    s => s.Supplier,
                    s => s.Products,
                });


            if (offersList is not null)
            {
                offersDtoList = _mapper.Map<List<OfferDetailsDto>>(offersList);
            }
            return offersDtoList;
        }
        public async Task<bool> AddOffer(int supplierId, OfferInfoDto offerDto)
        {
            if (offerDto is not null)
            {
                SupplierOffer Offer = new SupplierOffer();
                Offer = _mapper.Map<SupplierOffer>(offerDto);

                Offer.SupplierID = supplierId;
                Offer.CreatedTime = DateTime.Now;

                _unitOfWork.SupplierOffers.Add(Offer);
                _unitOfWork.Complete();

                Offer.Products = await AddOfferProducts(Offer.Id, offerDto.Products);

                _unitOfWork.SupplierOffers.Update(Offer);
                _unitOfWork.Complete();

                return true;
            }
            return false;
        }
        public async Task<bool> EditOffer(int id, OfferInfoDto offerDto)
        {
            SupplierOffer offer = await _unitOfWork.SupplierOffers.GetByIdAsync(id);

            if (offer is not null && offerDto is not null)
            {
                offer.Name = offerDto.Name;
                offer.Price = offerDto.Price;
                offer.Image = offerDto.Image;
                offer.Description = offerDto.Description;

                _unitOfWork.SupplierOffers.Update(offer);
                _unitOfWork.Complete();

                return true;
            }
            return false;
        }
        public async Task<bool> DeleteOffer(int id)
        {
            SupplierOffer offer = await _unitOfWork.SupplierOffers.GetByIdAsync(id);

            if (offer is not null)
            {
                _unitOfWork.SupplierOffers.Delete(offer);
                _unitOfWork.Complete();

                return true;
            }
            return false;
        }
        private async Task<List<SupplierOfferProduct>?> AddOfferProducts(int offerId, List<OfferProductDto> offerProductList)
        {
            List<SupplierOfferProduct> supplierOfferProducts = null;

            if (offerProductList is not null)
            {
                supplierOfferProducts = new();

                foreach (var offerProduct in offerProductList)
                {
                    supplierOfferProduct = new()
                    {
                        OfferId = offerId,
                        Quantity = offerProduct.Quantity,
                        ProductId = offerProduct.ProductId
                    };

                    _unitOfWork.SupplierOfferProducts.Add(supplierOfferProduct);
                    _unitOfWork.Complete();

                    supplierOfferProducts.Add(supplierOfferProduct);
                }
            }
            return supplierOfferProducts;
        }
    }
}
