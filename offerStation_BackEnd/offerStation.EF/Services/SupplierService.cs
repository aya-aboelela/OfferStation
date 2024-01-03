using AutoMapper;
using offerStation.Core.Dtos;
using offerStation.Core.Interfaces;
using offerStation.Core.Interfaces.Services;
using offerStation.Core.Models;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace offerStation.EF.Services
{
    public class SupplierService : ISupplierService
    {
        private IMapper _mapper;
        private IUnitOfWork _unitOfWork;
        private readonly IHelperService _helperService;
        public SupplierService(IMapper mapper, IUnitOfWork unitOfWork, IHelperService helperService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _helperService = helperService;
        }
        public async Task<PublicInfoDto?> GetSupplier(int id)
        {
            PublicInfoDto? supplierInfo = null;

            Supplier supplier = await _unitOfWork.Suppliers.FindAsync(s => s.Id == id,
                new List<Expression<Func<Supplier, object>>>()
                {
                    s => s.AppUser,
                });


            if (supplier is not null)
            {
                supplierInfo = new PublicInfoDto();
                supplierInfo = _mapper.Map<PublicInfoDto>(supplier);
            }

            return supplierInfo;
        }
        public async Task<List<SupplierDto>?> GetAllSuppliers()
        {
            List<SupplierDto> supplierDtoList = null;

            IEnumerable<Supplier> supplierList = await _unitOfWork.Suppliers
                .FindAllAsync(s => !s.IsDeleted && s.Approved);

            if (supplierList is not null)
            {
                supplierDtoList = new List<SupplierDto>();
                supplierDtoList = _mapper.Map<List<SupplierDto>>(supplierList);
            }
            return supplierDtoList;
        }
        public async Task<List<TraderDetailsDto>?> GetSuspendedSuppliers()
        {
            List<TraderDetailsDto> supplierDtoList = null;

            IEnumerable<Supplier> supplierList = await _unitOfWork.Suppliers
                .FindAllAsync(s => s.IsDeleted && s.Approved, 
                new List<Expression<Func<Supplier, object>>>()
                {
                    s => s.AppUser,
                });

            if (supplierList is not null)
            {
                supplierDtoList = new ();
                supplierDtoList = _mapper.Map<List<TraderDetailsDto>>(supplierList);
            }
            return supplierDtoList;
        }
        public async Task<List<TraderDetailsDto>?> GetWaitingSuppliers()
        {
            List<TraderDetailsDto> supplierDtoList = null;

            IEnumerable<Supplier> supplierList = await _unitOfWork.Suppliers
                .FindAllAsync(s => !s.IsDeleted && !s.Approved,
                new List<Expression<Func<Supplier, object>>>()
                {
                    s => s.AppUser,
                });

            if (supplierList is not null)
            {
                supplierDtoList = new ();
                supplierDtoList = _mapper.Map<List<TraderDetailsDto>>(supplierList);
            }
            return supplierDtoList;
        }
        public async Task<bool> EditSupplier(int id, PublicInfoDto supplierInfo)
        {
            Supplier supplier = await _unitOfWork.Suppliers.FindAsync(s => s.Id == id,
                new List<Expression<Func<Supplier, object>>>()
                {
                    s => s.AppUser,
                });

            if (supplier.IsDeleted is false)
            {
                supplier.Image = supplierInfo.Image;
                supplier.AppUser.Name = supplierInfo.Name;
                supplier.AppUser.Email = supplierInfo.Email;
                supplier.AppUser.PhoneNumber = supplierInfo.PhoneNumber;

                _unitOfWork.Suppliers.Update(supplier);
                _unitOfWork.Complete();

                return true;
            }
            return false;
        }
        public async Task<bool> PermanentDeleteSupplier(int id)
        {
            Supplier supplier = await _unitOfWork.Suppliers.GetByIdAsync(id);
            if (supplier is not null)
            {
                supplier.Approved = false;
                supplier.IsDeleted = true;

                _unitOfWork.Suppliers.Update(supplier);
                _unitOfWork.Complete();

                return true;
            }
            return false;
        }
        public async Task<bool> SuspendSupplier(int id)
        {
            Supplier supplier = await _unitOfWork.Suppliers.GetByIdAsync(id);
            if (supplier is not null)
            {
                _unitOfWork.Suppliers.Delete(supplier);
                _unitOfWork.Complete();

                return true;
            }
            return false;
        }
        public async Task<bool> RemoveSupplierSuspension(int id)
        {
            Supplier supplier = await _unitOfWork.Suppliers.GetByIdAsync(id);
            if (supplier is not null)
            {
                supplier.IsDeleted = false;

                _unitOfWork.Suppliers.Update(supplier);
                _unitOfWork.Complete();

                return true;
            }
            return false;
        }
        public async Task<bool> ApproveSupplier(int id)
        {
            Supplier supplier = await _unitOfWork.Suppliers.GetByIdAsync(id);
            if (supplier is not null)
            {
                supplier.Approved = true;

                _unitOfWork.Suppliers.Update(supplier);
                _unitOfWork.Complete();

                return true;
            }
            return false;
        }
        public async Task<ProductInfoDto?> GetProductDetails(int id)
        {
            ProductInfoDto productDto = null;

            SupplierProduct product = await _unitOfWork.SupplierProducts.GetByIdAsync(id);
            if (product is not null)
            {
                productDto = _mapper.Map<ProductInfoDto>(product);
            }
            return productDto;
        }
        public async Task<bool> AddProduct(int supplierId, ProductDto productDto)
        {
            if (productDto is not null)
            {
                SupplierProduct product = new SupplierProduct();
                product = _mapper.Map<SupplierProduct>(productDto);
                product.SupplierId = supplierId;

                _unitOfWork.SupplierProducts.Add(product);
                _unitOfWork.Complete();

                return true;
            }
            return false;
        }
        public async Task<bool> EditProduct(int id, ProductDto productDto)
        {
            SupplierProduct product = await _unitOfWork.SupplierProducts.FindAsync(p => p.Id == id);

            if (product is not null && productDto is not null)
            {
                product.Name = productDto.Name;
                product.Price = productDto.Price;
                product.Image = productDto.Image;
                product.Discount = productDto.Discount;
                product.CategoryId = productDto.CategoryId;
                product.Description = productDto.Description;

                _unitOfWork.SupplierProducts.Update(product);
                _unitOfWork.Complete();

                return true;
            }
            return false;
        }
        public async Task<bool> DeleteProduct(int id)
        {
            SupplierProduct product = await _unitOfWork.SupplierProducts.GetByIdAsync(id);

            if (product is not null)
            {
                _unitOfWork.SupplierProducts.Delete(product);
                _unitOfWork.Complete();

                return true;
            }
            return false;
        }
        public async Task<bool> AddCategory(CategoryInfoDto categoryDto)
        {
            if (categoryDto is not null)
            {
                SupplierCategory category = new SupplierCategory();
                category = _mapper.Map<SupplierCategory>(categoryDto);

                _unitOfWork.SupplierCategories.Add(category);
                _unitOfWork.Complete();

                return true;
            }
            return false;
        }
        public async Task<bool> EditCategory(int id, CategoryInfoDto categoryDto)
        {
            SupplierCategory category = await _unitOfWork.SupplierCategories.GetByIdAsync(id);

            if (category is not null && categoryDto is not null)
            {
                category.Name = categoryDto.Name;
                category.Image = categoryDto.Image;

                _unitOfWork.SupplierCategories.Update(category);
                _unitOfWork.Complete();

                return true;
            }
            return false;
        }
        public async Task<bool> DeleteCategory(int id)
        {
            SupplierCategory category = await _unitOfWork.SupplierCategories.GetByIdAsync(id);

            if (category is not null)
            {
                _unitOfWork.SupplierCategories.Delete(category);
                _unitOfWork.Complete();

                return true;
            }
            return false;
        }
        public async Task<List<SupplierOfferProductsDto>?> GetOfferDetailsByOfferId(int id)
        {
            List<SupplierOfferProductsDto> OfferListDto = new List<SupplierOfferProductsDto> { };
            IEnumerable<SupplierOfferProduct> offerList = await _unitOfWork.SupplierOfferProducts.FindAllAsync(o => o.OfferId == id,
            new List<Expression<Func<SupplierOfferProduct, object>>>()
            {
                O=>O.Product,

            });
            foreach (SupplierOfferProduct offer in offerList)
            {
                SupplierOfferProductsDto OfferList = new SupplierOfferProductsDto();

                OfferList.ProductName = offer.Product.Name;
                OfferList.price = offer.Product.Price;
                OfferList.ProductDescription = offer.Product.Description;
                OfferList.Quantity = offer.Quantity;
                OfferListDto.Add(OfferList);

            }
            return OfferListDto;

        }
        public async Task<List<ProductInfoDto>> GetProductsByMenuCategoryID(int id)
        {
            List<ProductInfoDto> SupplierProductsDTOs;

            SupplierProductsDTOs = new List<ProductInfoDto>();
            IEnumerable<SupplierProduct> result = await _unitOfWork.SupplierProducts.FindAllAsync(d => d.CategoryId == id && !d.IsDeleted);

            foreach (SupplierProduct product in result)
            {
                ProductInfoDto ProductDTO = new ProductInfoDto();
                ProductDTO.Price = product.Price;
                ProductDTO.Description = product.Description;
                ProductDTO.Name = product.Name;
                ProductDTO.Id = product.Id;
                ProductDTO.Discount = product.Discount;
                ProductDTO.DiscountPrice = product.Price - (product.Price * product.Discount / 100);
                ProductDTO.Image = product.Image;



                SupplierProductsDTOs.Add(ProductDTO);
            }
            
            return SupplierProductsDTOs;
        }
        public async Task<List<ProductInfoDto>> GetAllProductsBySupplierIDWithPagination(int pageNumber, int pageSize, int id)
        {
            List<ProductInfoDto> SupplierProductsDTOs;
            SupplierProductsDTOs = new List<ProductInfoDto>();


            IEnumerable<SupplierProduct> supplierProducts = await _unitOfWork.SupplierProducts
                .FindAllAsync(d => d.SupplierId == id && !d.IsDeleted);


            foreach (SupplierProduct product in supplierProducts)
            {
                ProductInfoDto ProductDTO = new ProductInfoDto();
                ProductDTO.Price = product.Price;
                ProductDTO.Description = product.Description;
                ProductDTO.Name = product.Name;
                ProductDTO.Id = product.Id;
                ProductDTO.Discount = product.Discount;
                ProductDTO.DiscountPrice = product.Price - (product.Price * product.Discount / 100);
                ProductDTO.Image = product.Image;

                SupplierProductsDTOs.Add(ProductDTO);
            }
            ResultrDto<ProductInfoDto> offerFilterResult = new ResultrDto<ProductInfoDto>();
            offerFilterResult.itemsCount = SupplierProductsDTOs.Count();
            int recSkip = (pageNumber - 1) * pageSize;
            SupplierProductsDTOs = SupplierProductsDTOs.Skip(recSkip).Take(pageSize).ToList();
            return SupplierProductsDTOs;


        }
        public async Task<List<OfferDetailsDto>?> GetAllOffersBySupplierIdWithPagination(int pageNumber, int pageSize, int id)
        {
            List<OfferDetailsDto> OfferListDto = null;
            IEnumerable<SupplierOffer> offerList = await _unitOfWork.SupplierOffers.FindAllAsync(o => o.SupplierID == id);

            if (offerList is not null)
            {
                OfferListDto = _mapper.Map<List<OfferDetailsDto>>(offerList);
            }
            ResultrDto<ProductInfoDto> offerFilterResult = new ResultrDto<ProductInfoDto>();
            offerFilterResult.itemsCount = OfferListDto.Count();
            int recSkip = (pageNumber - 1) * pageSize;
            OfferListDto = OfferListDto.Skip(recSkip).Take(pageSize).ToList();
            return OfferListDto;
        }
        public async Task<List<ProductInfoDto>?> GetAllProducts(int supplierId)
        {
            List<ProductInfoDto> products = null;

            IEnumerable<SupplierProduct> supplierProducts = await _unitOfWork.SupplierProducts
                .FindAllAsync(p => p.SupplierId == supplierId && !p.IsDeleted);

            if (supplierProducts is not null)
            {
                products = _mapper.Map<List<ProductInfoDto>>(supplierProducts);
            }

            return products;
        }
        public async Task<List<SupplierCategory>> GetAllCategories()
        {
            List<SupplierCategory> supplierCategories = (List<SupplierCategory>)_unitOfWork.SupplierCategories.GetAll();
            List<SupplierCategory> supplierCategoriesDto = _mapper.Map<List<SupplierCategory>>(supplierCategories);
            return supplierCategoriesDto;
        }

        public async Task<List<ReviewDto>?> GetAllOwnersReviewsBySupplierId(int supplierId)
        {
            List<ReviewDto> reviewListDto = null;

            IEnumerable<OwnerReview> reviewList = await _unitOfWork.OwnerReviews
                .FindAllAsync(r => r.SupplierId == supplierId && !r.IsDeleted,
                new List<Expression<Func<OwnerReview, object>>>()
                {
                    r => r.Owner.AppUser,
                });

            if (reviewList is not null)
            {
                reviewListDto = _mapper.Map<List<ReviewDto>>(reviewList);
            }
            return reviewListDto;
        }
        public async Task<List<ReviewDto>?> GetAllOwnersReviewsBySupplierIdWithPagination (int pageNumber, int pageSize, int supplierId)
        {
            List<ReviewDto> reviewListDto = null;

            IEnumerable<OwnerReview> reviewList = await _unitOfWork.OwnerReviews
                .FindAllAsync(r => r.SupplierId == supplierId && !r.IsDeleted,
                new List<Expression<Func<OwnerReview, object>>>()
                {
                    r => r.Owner.AppUser,
                });

            if (reviewList is not null)
            {
                reviewListDto = _mapper.Map<List<ReviewDto>>(reviewList);
            }
            ResultrDto<OwnerReview> offerFilterResult = new ResultrDto<OwnerReview>();
            offerFilterResult.itemsCount = reviewListDto.Count();
            int recSkip = (pageNumber - 1) * pageSize;
            reviewListDto = reviewListDto.Skip(recSkip).Take(pageSize).ToList();
            return reviewListDto;
        }
        public async Task<SupplierInfoDto?> GetSupplierInfo(int id)
        {
            SupplierInfoDto SupplierInfo = new SupplierInfoDto();

            Supplier supplier = await _unitOfWork.Suppliers.FindAsync(o => o.Id == id,
                new List<Expression<Func<Supplier, object>>>()
                {
                    o => o.AppUser,
                    o=>o.Reviews
                });
            if (supplier is not null)
            {
                int ratingSum = supplier.Reviews.Select(r => r.Rating).Sum();
                int ratingcount = supplier.Reviews.Select(r => r.Rating).Count();
                if (ratingcount != 0)
                {
                    SupplierInfo.Rating = ratingSum / ratingcount;

                }
                else
                {
                    SupplierInfo.Rating = 0;
                }


                SupplierInfo.Image = supplier.Image;
                SupplierInfo.PhoneNumber = supplier.AppUser.PhoneNumber;
                SupplierInfo.Name = supplier.AppUser.Name;
                SupplierInfo.Email = supplier.AppUser.Email;




            }
            return SupplierInfo;
        }
        public async Task<List<ReviewDto>?> GetAllOwnerReviewsBySupplierId(int supplierId)
        {
            List<ReviewDto> reviewListDto = null;

            IEnumerable<OwnerReview> reviewList = await _unitOfWork.OwnerReviews
                .FindAllAsync(r => r.Supplier.Id == supplierId && !r.IsDeleted,
                 new List<Expression<Func<OwnerReview, object>>>()
                 {
                     r => r.Owner.AppUser,
                 });


            if (reviewList is not null)
            {
                reviewListDto = _mapper.Map<List<ReviewDto>>(reviewList);
            }
            return reviewListDto;
        }
        public async Task<List<MenuCategoryDetailsDto>> GetMenuCategoiesBySupplierId(int id)
        {
            List<MenuCategoryDetailsDto> MenuCategoriesDTOs;

            MenuCategoriesDTOs = new List<MenuCategoryDetailsDto>();

            IEnumerable<SupplierMenuCategory> result = await _unitOfWork.SupplierMenuCategories.FindAllAsync(d => d.SupplierId == id && !d.IsDeleted);

            foreach (SupplierMenuCategory menu in result)
            {
                MenuCategoryDetailsDto MenuDTO = new MenuCategoryDetailsDto();
                MenuDTO.Id = menu.Id;
                MenuDTO.Name = menu.Name;
                MenuDTO.Image = menu.Image;
                MenuCategoriesDTOs.Add(MenuDTO);
            }
            return MenuCategoriesDTOs;
        }

        public async Task<List<SupplierOffer>> filterOffersByCity(int CityID, string categoryName)
        {

            List<SupplierOffer> offers;
            offers = (List<SupplierOffer>)await _unitOfWork.SupplierOffers.FindAllAsync(s => s.IsDeleted == false && s.Supplier.SupplierCategory.Name == categoryName, new List<Expression<Func<SupplierOffer, object>>>()
               {
                   o=>o.Supplier.AppUser.Addresses,
                   o=>o.Supplier.SupplierCategory
            });
            if (CityID != 0)
            {

                offers = offers.Where(s => _helperService.checkAddress(s.Supplier.AppUser.Addresses, CityID)).ToList();
                return offers;
            }
            return offers;



        }

        public async Task<List<Supplier>> filterSupplierByCity(int CityID, string categoryName)
        {
            List<Supplier> suppliers;
            suppliers = (List<Supplier>)await _unitOfWork.Suppliers.FindAllAsync(s => s.IsDeleted == false && s.SupplierCategory.Name == categoryName && s.Approved == true, new List<Expression<Func<Supplier, object>>>()
               {
                   o=>o.AppUser.Addresses,
                   o=>o.SupplierCategory,
                   o=>o.Reviews

            });
            if (CityID != 0)
            {
                suppliers = suppliers.Where(s => _helperService.checkAddress(s.AppUser.Addresses, CityID)).ToList();
                return suppliers;
            }

            return suppliers;
        }

        public List<SupplierOffer> sortingOfferData(List<SupplierOffer> offers, string sortBy)
        {
            if (sortBy == "MostPopular")
            {
                return offers.OrderByDescending(o => o.orders.Count).ToList();

            }
            else if (sortBy == "Cheapest")
            {
                return offers.OrderBy(O => O.Price).ToList();
            }
            else
            {
                return offers.OrderByDescending(O => O.CreatedTime).ToList();

            }
        }
        public async Task<int> calucaluteSupplierRating(Supplier supplier)
        {
            int ratingSum = supplier.Reviews.Select(r => r.Rating).Sum();
            int ratingcount = supplier.Reviews.Select(r => r.Rating).Count();
            if (ratingcount != 0)
            {
                return ratingSum / ratingcount;

            }
            return 0;

        }
        public async Task<int> calucaluteOrdersNumber(int supId)
        {
            List<OwnerOrder> SupplierOrders = (List<OwnerOrder>)await _unitOfWork.OwnerOrders.FindAllAsync(o => o.SupplierId == supId);
            return SupplierOrders.Count();
        }
        public List<Supplier> sortingSupplierData(List<Supplier> suppliers, string sortBy)
        {
            if (sortBy == "MostPopular")
            {
                return suppliers.OrderByDescending(s => calucaluteOrdersNumber(s.Id)).ToList();

            }
            else if (sortBy == "TopRated")
            {
                return suppliers.OrderBy(s => calucaluteSupplierRating(s)).ToList();
            }

            return suppliers;
        }


        public double GetPriceBeforeOffer(SupplierOffer supplierOffer)
        {
            List<SupplierOfferProduct> supplierOffers = (List<SupplierOfferProduct>)_unitOfWork.SupplierOfferProducts.FindAll(o => o.OfferId == supplierOffer.Id, new List<Expression<Func<SupplierOfferProduct, object>>>()
            {
                   o=>o.Offer.Supplier,
                   o=>o.Product
            });
            double PrefPrice = supplierOffers.Select(o => o.Product.Price * o.Quantity).Sum();

            return PrefPrice;
        }

        public async Task<ResultrDto<SupplierDto>> getSupplierByCategory(int PageNumber, int pageSize, int cityId, string name, string SortBy, string Category)
        {
            List<Supplier> suppliers;

            suppliers = await filterSupplierByCity(cityId, Category);

            if (SortBy != "")
            {
                suppliers = sortingSupplierData(suppliers, SortBy);
            }

            if (name != "")
            {
                suppliers = suppliers.Where(s => s.AppUser.Name.ToLower().Trim() == name.ToLower().Trim()).ToList();
            }

            ResultrDto<SupplierDto> suppierResult = new ResultrDto<SupplierDto>();
            suppierResult.itemsCount = suppliers.Count();
            int recSkip = (PageNumber - 1) * pageSize;
            suppliers = suppliers.Skip(recSkip).Take(pageSize).ToList();

            List<SupplierDto> supplierDtos = new List<SupplierDto>();
            suppliers.ForEach(async s =>
            {
                SupplierDto supplier = new SupplierDto();
                supplier = _mapper.Map<SupplierDto>(s);

                supplier.Rating = await calucaluteSupplierRating(s);

                supplierDtos.Add(supplier);

            });
            suppierResult.List = supplierDtos;

            return suppierResult;

        }

        public async Task<ResultrDto<OfferDto>> filterOffersData(int pageNumber, int pageSize, int cityId, string CategoryName, string sortBy)
        {
            List<SupplierOffer> offers;

            offers = await filterOffersByCity(cityId, CategoryName);

            if (sortBy != string.Empty)
            {
                offers = sortingOfferData(offers, sortBy);
            }


            ResultrDto<OfferDto> offerFilterResult = new ResultrDto<OfferDto>();
            offerFilterResult.itemsCount = offers.Count();
            int recSkip = (pageNumber - 1) * pageSize;
            offers = offers.Skip(recSkip).Take(pageSize).ToList();

            List<OfferDto> OfferDtos = new List<OfferDto>();
            offers.ForEach(o =>
            {
                OfferDto Offer = new OfferDto();
                Offer = _mapper.Map<OfferDto>(o);
                Offer.ownerId = o.SupplierID;

                Offer.PrefPrice = GetPriceBeforeOffer(o);
                Offer.TraderImage = o.Supplier.Image;

                OfferDtos.Add(Offer);

            });
            offerFilterResult.List = OfferDtos;

            return offerFilterResult;

        }
        public async Task<ResultrDto<OfferDto>> GetAllOffersWithPagination(int pageNumber, int pageSize, int cityId, string SortBy, string Category)
        {
            return await filterOffersData(pageNumber, pageSize, cityId, Category, SortBy);


        }
        public async Task<List<OfferDto>> GetAllOffersWithoutPagination(string CategoryName, string sortBy)
        {

            return filterOffersData(1, 3, 0, CategoryName, sortBy).Result.List;

        }
        public async Task<List<AddressInfoDTO>> GetAddressesBySupplierID(int id)
        {
            var ID = Convert.ToString(id);
            List<AddressInfoDTO> SupplierAddressesDTOs;

            SupplierAddressesDTOs = new List<AddressInfoDTO>();
            IEnumerable<Address> result = await _unitOfWork.Addresses.FindAllAsync(d => d.UserId == ID && !d.IsDeleted, new List<Expression<Func<Address, object>>>()
            {
                o=>o.City


            });
            foreach (Address address in result)
            {
                AddressInfoDTO AddressDTO = new AddressInfoDTO();
                AddressDTO.details = address.details;
                AddressDTO.CityName = address.City.Name;
                AddressDTO.CityId = address.CityId;
                AddressDTO.Id = address.Id;
                SupplierAddressesDTOs.Add(AddressDTO);
            }
            return SupplierAddressesDTOs;
        }
        public async Task<double> GetMinPriceoFProductBySupplierID(int id)
        {
            IEnumerable<SupplierProduct> supplierProducts = await _unitOfWork.SupplierProducts.FindAllAsync(o => o.SupplierId == id);
            return supplierProducts.Min(o => o.Price);

        }
        public async Task<double> GetMaxPriceoFProductBySupplierID(int id)
        {
            IEnumerable<SupplierProduct> supplierProducts = await _unitOfWork.SupplierProducts.FindAllAsync(o => o.SupplierId == id);

            return supplierProducts.Max(o => o.Price);

        }
        public async Task<List<ProductInfoDto>> GetProductsBySupplierIDAndPrice(int id, double selectedprice)
        {
            List<ProductInfoDto> SupplierProductsDTOs;
            SupplierProductsDTOs = new List<ProductInfoDto>();


            IEnumerable<SupplierProduct> supplierProducts = await _unitOfWork.SupplierProducts
                .FindAllAsync(d => d.SupplierId == id && !d.IsDeleted && d.Price - (d.Price * d.Discount / 100) <= selectedprice);


            foreach (SupplierProduct product in supplierProducts)
            {
                ProductInfoDto ProductDTO = new ProductInfoDto();
                ProductDTO.Price = product.Price;
                ProductDTO.Description = product.Description;
                ProductDTO.Name = product.Name;
                ProductDTO.Id = product.Id;
                ProductDTO.Discount = product.Discount;
                ProductDTO.DiscountPrice = product.Price - (product.Price * product.Discount / 100);
                ProductDTO.Image = product.Image;

                SupplierProductsDTOs.Add(ProductDTO);
            }


            return SupplierProductsDTOs;


        }
        public async Task<List<offerProductInfo>> getofferProduct(int OfferId)
        {
            List<SupplierOfferProduct> offers = _unitOfWork.SupplierOfferProducts.FindAllAsync(O => O.OfferId == OfferId && O.IsDeleted == false, new List<Expression<Func<SupplierOfferProduct, object>>>()
            {
                o=>o.Product


            }).Result.ToList();
            List<offerProductInfo> result = new List<offerProductInfo>();
            foreach (SupplierOfferProduct Offer in offers)
            {
                offerProductInfo offerProduct = new offerProductInfo
                {
                    Description = Offer.Product.Description,
                    Quantity = Offer.Quantity,
                    Name = Offer.Product.Name,
                    Image = Offer.Product.Image,
                    Price = Offer.Product.Price
                };
                result.Add(offerProduct);
            }
            return result;
        }
    }
}
