using AutoMapper;
using offerStation.Core.Dtos;
using offerStation.Core.Interfaces;
using offerStation.Core.Interfaces.Services;
using offerStation.Core.Models;
using System.Drawing.Printing;
using System.Linq.Expressions;

namespace offerStation.EF.Services
{
    public class OwnerService : IOwnerService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHelperService _helperService;
        public OwnerService(IMapper mapper, IUnitOfWork unitOfWork, IHelperService helperService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _helperService = helperService;
        }
        public async Task<PublicInfoDto?> GetOwner(int id)
        {
            PublicInfoDto? ownerInfo = null;

            Owner owner = await _unitOfWork.Owners.FindAsync(o => o.Id == id,
                new List<Expression<Func<Owner, object>>>()
                {
                    o => o.AppUser,
                });


            if (owner is not null)
            {
                ownerInfo = new PublicInfoDto();
                ownerInfo = _mapper.Map<PublicInfoDto>(owner);
            }

            return ownerInfo;
        }
        public async Task<OwnerInfoDto?> GetOwnerInfo(int id)
        {
            OwnerInfoDto ownerInfo = new OwnerInfoDto();

            Owner owner = await _unitOfWork.Owners.FindAsync(o => o.Id== id,
                new List<Expression<Func<Owner, object>>>()
                {
                    o => o.AppUser,
                    o=>o.CustomersReviews
                });
            if (owner is not null)
            {
                int ratingSum = owner.CustomersReviews.Select(r => r.Rating).Sum();
                int ratingcount = owner.CustomersReviews.Select(r => r.Rating).Count();
                if (ratingcount != 0)
                {
                    ownerInfo.Rating = ratingSum / ratingcount;

                }
                else
                {
                    ownerInfo.Rating = 0;   
                }
         

                ownerInfo.Image = owner.Image;
                ownerInfo.PhoneNumber = owner.AppUser.PhoneNumber;
                ownerInfo.Name = owner.AppUser.Name;
                ownerInfo.Email = owner.AppUser.Email;
               
            }
            return ownerInfo;   
        }
        public async Task<List<OwnerDto>?> GetAllOwners()
        {
            List<OwnerDto> ownerDtoList = null;

            IEnumerable<Owner> ownerList = await _unitOfWork.Owners
                .FindAllAsync(o => !o.IsDeleted && o.Approved);

            if (ownerList is not null)
            {
                ownerDtoList = new List<OwnerDto>();
                ownerDtoList = _mapper.Map<List<OwnerDto>>(ownerList);
            }
            return ownerDtoList;
        }
        public async Task<List<TraderDetailsDto>?> GetSuspendedOwners()
        {
            List<TraderDetailsDto> ownerDtoList = null;

            IEnumerable<Owner> ownerList = await _unitOfWork.Owners
                .FindAllAsync(o => o.IsDeleted && o.Approved, new List<Expression<Func<Owner, object>>>()
                {
                    o => o.AppUser,
                });

            if (ownerList is not null)
            {
                ownerDtoList = new ();
                ownerDtoList = _mapper.Map<List<TraderDetailsDto>>(ownerList);
            }
            return ownerDtoList;
        }
        public async Task<List<TraderDetailsDto>?> GetWaitingOwners()
        {
            List<TraderDetailsDto> ownerDtoList = null;

            IEnumerable<Owner> ownerList = await _unitOfWork.Owners
                .FindAllAsync(o => !o.IsDeleted && !o.Approved, 
                new List<Expression<Func<Owner, object>>>()
                {
                    o => o.AppUser,
                });

            if (ownerList is not null)
            {
                ownerDtoList = new ();
                ownerDtoList = _mapper.Map<List<TraderDetailsDto>>(ownerList);
            }
            return ownerDtoList;
        }
        public async Task<bool> EditOwner(int id, PublicInfoDto ownerInfo)
        {
            Owner owner = await _unitOfWork.Owners.FindAsync(o => o.Id == id,
                new List<Expression<Func<Owner, object>>>()
                {
                    o => o.AppUser,
                });

            if (owner.IsDeleted is false)
            {
                owner.Image = ownerInfo.Image;
                owner.AppUser.Name = ownerInfo.Name;
                owner.AppUser.Email = ownerInfo.Email;
                owner.AppUser.PhoneNumber = ownerInfo.PhoneNumber;

                _unitOfWork.Owners.Update(owner);
                _unitOfWork.Complete();

                return true;
            }
            return false;
        }
        public async Task<bool> PermanentDeleteOwner(int id)
        {
            Owner owner = await _unitOfWork.Owners.GetByIdAsync(id);

            if (owner is not null)
            {
                owner.Approved = false;
                owner.IsDeleted = true;

                _unitOfWork.Owners.Update(owner);
                _unitOfWork.Complete();

                return true;
            }
            return false;
        }
        public async Task<bool> SuspendOwner(int id)
        {
            Owner owner = await _unitOfWork.Owners.GetByIdAsync(id);

            if (owner is not null)
            {
                _unitOfWork.Owners.Delete(owner);
                _unitOfWork.Complete();

                return true;
            }
            return false;
        }
        public async Task<bool> RemoveOwnerSuspension(int id)
        {
            Owner owner = await _unitOfWork.Owners.GetByIdAsync(id);

            if (owner is not null)
            {
                owner.IsDeleted = false;

                _unitOfWork.Owners.Update(owner);
                _unitOfWork.Complete();

                return true;
            }
            return false;
        }
        public async Task<bool> ApproveOwner(int id)
        {
            Owner owner = await _unitOfWork.Owners.GetByIdAsync(id);

            if (owner is not null)
            {
                owner.Approved = true;

                _unitOfWork.Owners.Update(owner);
                _unitOfWork.Complete();

                return true;
            }
            return false;
        }
        public async Task<ProductInfoDto?> GetProductDetails(int id)
        {
            ProductInfoDto productDto = null;

            OwnerProduct product = await _unitOfWork.OwnerProducts.GetByIdAsync(id);
            if (product is not null)
            {
                productDto = _mapper.Map<ProductInfoDto>(product);
            }
            return productDto;
        }
        public async Task<bool> AddProduct(int ownerId, ProductDto productDto)
        {
            if (productDto is not null)
            {
                OwnerProduct product = new OwnerProduct();
                product = _mapper.Map<OwnerProduct>(productDto);
                product.OwnerId = ownerId;

                _unitOfWork.OwnerProducts.Add(product);
                _unitOfWork.Complete();

                return true;
            }
            return false;
        }
        public async Task<bool> EditProduct(int id, ProductDto productDto)
        {
            OwnerProduct product = await _unitOfWork.OwnerProducts.GetByIdAsync(id);

            if (product is not null && productDto is not null)
            {
                product.Name = productDto.Name;
                product.Price = productDto.Price;
                product.Image = productDto.Image;
                product.Discount = productDto.Discount;
                product.CategoryId = productDto.CategoryId;
                product.Description = productDto.Description;

                _unitOfWork.OwnerProducts.Update(product);
                _unitOfWork.Complete();

                return true;
            }
            return false;
        }
        public async Task<bool> DeleteProduct(int id)
        {
            OwnerProduct product = await _unitOfWork.OwnerProducts.GetByIdAsync(id);

            if (product is not null)
            {
                _unitOfWork.OwnerProducts.Delete(product);
                _unitOfWork.Complete();

                return true;
            }
            return false;
        }
        // =============================== Admin =======================
        public async Task<bool> AddCategory(CategoryInfoDto categoryDto)
        {
            if (categoryDto is not null)
            {
                OwnerCategory category = new OwnerCategory();
                category = _mapper.Map<OwnerCategory>(categoryDto);

                _unitOfWork.OwnerCategories.Add(category);
                _unitOfWork.Complete();

                return true;
            }
            return false;
        }
        // =============================== Admin =======================
        public async Task<bool> EditCategory(int id, CategoryInfoDto categoryDto)
        {
            OwnerCategory category = await _unitOfWork.OwnerCategories.GetByIdAsync(id);

            if (category is not null && categoryDto is not null)
            {
                category.Name = categoryDto.Name;
                category.Image = categoryDto.Image;

                _unitOfWork.OwnerCategories.Update(category);
                _unitOfWork.Complete();

                return true;
            }
            return false;
        }
        // =============================== Admin =======================
        public async Task<bool> DeleteCategory(int id)
        {
            OwnerCategory category = await _unitOfWork.OwnerCategories.GetByIdAsync(id);

            if (category is not null)
            {
                _unitOfWork.OwnerCategories.Delete(category);
                _unitOfWork.Complete();

                return true;
            }
            return false;
        }
        public async Task<bool> AddReview(int ownerId, int supplierId, ReviewInfoDto reviewDto)
        {
            if (reviewDto is not null)
            {
                OwnerReview review = new OwnerReview();
                review = _mapper.Map<OwnerReview>(reviewDto);

                review.OwnerId = ownerId;
                review.SupplierId = supplierId;

                _unitOfWork.OwnerReviews.Add(review);
                _unitOfWork.Complete();

                return true;
            }
            return false;
        }
        public async Task<bool> DeleteReview(int id)
        {
            OwnerReview review = await _unitOfWork.OwnerReviews.GetByIdAsync(id);

            if (review is not null)
            {
                _unitOfWork.OwnerReviews.Delete(review);
                _unitOfWork.Complete();

                return true;
            }
            return false;
        }
        public async Task<List<ReviewDto>?> GetAllOwnersReviews()
        {
            List<ReviewDto> reviewListDto = null;
            IEnumerable<OwnerReview> reviewList = await _unitOfWork.OwnerReviews
                .FindAllAsync(r => !r.IsDeleted,
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
        public async Task<List<OfferDetailsDto>?> GetAllOffersByOwnerIdWithPagination( int id)
        {
            List<OfferDetailsDto> OfferListDto = null;
            IEnumerable<OwnerOffer> offerList = await _unitOfWork.OwnerOffers.FindAllAsync(o=>o.OwnerId==id && !o.IsDeleted);

            if (offerList is not null)
            {
                OfferListDto = _mapper.Map<List<OfferDetailsDto>>(offerList);
            }
           
            return OfferListDto;
        }
        public async Task<List<OwnerOfferProductsDto>?> GetOfferDetailsByOfferId(int id)
        {
            List<OwnerOfferProductsDto> OfferListDto = new List<OwnerOfferProductsDto> { };
            IEnumerable<OwnerOfferProduct> offerList = await _unitOfWork.OwnerOfferProducts.FindAllAsync(o => o.OfferId == id,
            new List<Expression<Func<OwnerOfferProduct, object>>>()
            {
                O=>O.Product,
 
            });
            foreach (OwnerOfferProduct offer in offerList)
            {
                OwnerOfferProductsDto OfferList = new OwnerOfferProductsDto();
              
                OfferList.ProductName = offer.Product.Name;
                OfferList.ProductImage= offer.Product.Image;    
                OfferList.price=offer.Product.Price;
                OfferList.ProductDescription = offer.Product.Description;
                OfferList.Quantity = offer.Quantity;    
                OfferListDto.Add(OfferList);

            }
            return OfferListDto;
         
        }
        public async Task<List<ReviewDto>?> GetAllCustomerReviewsByOwnerIdWithPagination(int pageNumber, int pageSize, int id)
        {
            List<ReviewDto> reviewListDto = null;

            IEnumerable<CustomerReview> reviewList = await _unitOfWork.CustomerReviews
                .FindAllAsync(r => r.OwnerId == id && !r.IsDeleted,
                 new List<Expression<Func<CustomerReview, object>>>()
                 {
                     r => r.Customer.AppUser,
                 });


            if (reviewList is not null)
            {
                reviewListDto = _mapper.Map<List<ReviewDto>>(reviewList);
            }
            ResultrDto<CustomerReview> offerFilterResult = new ResultrDto<CustomerReview>();
            offerFilterResult.itemsCount = reviewListDto.Count();
            int recSkip = (pageNumber - 1) * pageSize;
            reviewListDto = reviewListDto.Skip(recSkip).Take(pageSize).ToList();
            return reviewListDto;
        }
        public async Task<List<ReviewDto>?> GetAllCustomerReviewsByOwnerId(int id)
        {
            List<ReviewDto> reviewListDto = null;

            IEnumerable<CustomerReview> reviewList = await _unitOfWork.CustomerReviews
                .FindAllAsync(r => r.OwnerId == id && !r.IsDeleted,
                 new List<Expression<Func<CustomerReview, object>>>()
                 {
                     r => r.Customer.AppUser,
                 });


            if (reviewList is not null)
            {
                reviewListDto = _mapper.Map<List<ReviewDto>>(reviewList);
            }
            
            return reviewListDto;
        }
        public async Task<List<ReviewDto>?> GetAllAddressByOwnerId(int ownerId)
        {
            List<ReviewDto> reviewListDto = null;

            IEnumerable<CustomerReview> reviewList = await _unitOfWork.CustomerReviews
                .FindAllAsync(r => r.OwnerId == ownerId && !r.IsDeleted,
                 new List<Expression<Func<CustomerReview, object>>>()
                 {
                     r => r.Customer.AppUser,
                 });


            if (reviewList is not null)
            {
                reviewListDto = _mapper.Map<List<ReviewDto>>(reviewList);
            }
            return reviewListDto;
        }
        public async Task<List<OwnerOffer>> filterOffersByCity(int CityID, string categoryName)
        {
            List<OwnerOffer> offers;
            offers = (List<OwnerOffer>)await _unitOfWork.OwnerOffers.FindAllAsync(o => o.IsDeleted == false && o.Owner.OwnerCategory.Name == categoryName, new List<Expression<Func<OwnerOffer, object>>>()
               {
                   o=>o.Owner.AppUser.Addresses,
                   o=>o.Owner.OwnerCategory,
                   o=>o.Orders
               });

            if (CityID != 0)
            {

                offers = offers.Where(o => _helperService.checkAddress(o.Owner.AppUser.Addresses, CityID)).ToList();
                return offers;
            }

            return offers;
        }
        public async Task<List<Owner>> filterOwnersByCity(int CityID, string categoryName)
        {
            List<Owner> owners;
            owners = (List<Owner>)await _unitOfWork.Owners.FindAllAsync(o => o.IsDeleted == false && o.OwnerCategory.Name == categoryName && o.Approved == true, new List<Expression<Func<Owner, object>>>()
               {
                   o=>o.AppUser.Addresses,
                   o=>o.OwnerCategory,
                   o=>o.CustomersReviews,
                   o=>o.CustomerOrders

            });
            if (CityID != 0)
            {
                owners = owners.Where(o => _helperService.checkAddress(o.AppUser.Addresses, CityID)).ToList();
                return owners;
            }

            return owners;
        }
        public List<OwnerOffer> sortingOwnerOfferData(List<OwnerOffer> offers, string sortBy)
        {
            if (sortBy == "MostPopular")
            {
                return offers.OrderByDescending(o => o.Orders.Count).ToList();

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
        public async Task<int> calucaluteOwnerRating(Owner owner)
        {
            int ratingSum = owner.CustomersReviews.Select(r => r.Rating).Sum();
            int ratingcount = owner.CustomersReviews.Select(r => r.Rating).Count();
            if (ratingcount != 0)
            {
                return ratingSum / ratingcount;

            }
            return 0;
        }
        public List<Owner> sortingOwnerData(List<Owner> owners, string sortBy)
        {
            if (sortBy == "MostPopular")
            {
                return owners.OrderByDescending(o => o.CustomerOrders.Count()).ToList();

            }
            else if (sortBy == "TopRated")
            {
                return owners.OrderBy(calucaluteOwnerRating).ToList();
            }

            return owners;
        }
        ///===================================== Admin
        public async Task<List<CategoryDto>> GetAllCategories()
        {
            List<OwnerCategory> ownerCategories = (List<OwnerCategory>)_unitOfWork.OwnerCategories.GetAll();
        List<CategoryDto> ownerCategoriesDto = _mapper.Map<List<CategoryDto>>(ownerCategories);
        return ownerCategoriesDto;
        }
        public async Task<List<MenuCategoryDetailsDto>> GetMenuCategoiesByOwnerId(int id)
        {
            List<MenuCategoryDetailsDto> MenuCategoriesDTOs;

            MenuCategoriesDTOs = new List<MenuCategoryDetailsDto>();

            IEnumerable<OwnerMenuCategory> result = await _unitOfWork.OwnerMenuCategories.FindAllAsync(d => d.OwnerId == id && !d.IsDeleted);

            foreach (OwnerMenuCategory menu in result)
            {
                MenuCategoryDetailsDto MenuDTO = new MenuCategoryDetailsDto();
                MenuDTO.Id = menu.Id;
                MenuDTO.Name = menu.Name;
                MenuDTO.Image = menu.Image;
                MenuCategoriesDTOs.Add(MenuDTO);
            }
            return MenuCategoriesDTOs;
        }
        public async Task<List<ProductInfoDto>> GetProductsByMenuCategoryID(int id)
        {
            List<ProductInfoDto> OwnerProductsDTOs;

            OwnerProductsDTOs = new List<ProductInfoDto>();
            IEnumerable<OwnerProduct> result = await _unitOfWork.OwnerProducts.FindAllAsync(d => d.CategoryId == id && !d.IsDeleted);

            foreach (OwnerProduct product in result)
            {
                ProductInfoDto ProductDTO = new ProductInfoDto();
                ProductDTO.Price = product.Price;
                ProductDTO.Description = product.Description;
                ProductDTO.Name = product.Name;
                ProductDTO.Id = product.Id;
                ProductDTO.Discount = product.Discount;
                ProductDTO.DiscountPrice = product.Price-(product.Price * product.Discount/100);   
                ProductDTO.Image = product.Image;
               


                OwnerProductsDTOs.Add(ProductDTO);
            }
            return OwnerProductsDTOs;
        }
        public async Task<List<ProductInfoDto>> GetAllProductsByOwmerIDWithPagination(int pageNumber, int pageSize,int id)
        {
            List<ProductInfoDto> OwnerProductsDTOs ;
            OwnerProductsDTOs = new List<ProductInfoDto>();
           

            IEnumerable<OwnerProduct> ownerProducts = await _unitOfWork.OwnerProducts
                .FindAllAsync(d => d.OwnerId == id && !d.IsDeleted);
         

            foreach (OwnerProduct product in ownerProducts)
            {
                ProductInfoDto ProductDTO = new ProductInfoDto();
                ProductDTO.Price = product.Price;
                ProductDTO.Description = product.Description;
                ProductDTO.Name = product.Name;
                ProductDTO.Id = product.Id;
                ProductDTO.Discount = product.Discount;
                ProductDTO.DiscountPrice = product.Price - (product.Price * product.Discount / 100);
                ProductDTO.Image = product.Image;

                OwnerProductsDTOs.Add(ProductDTO);
            }
            ResultrDto<ProductInfoDto> offerFilterResult = new ResultrDto<ProductInfoDto>();
            offerFilterResult.itemsCount = OwnerProductsDTOs.Count();
            int recSkip = (pageNumber - 1) * pageSize;
            OwnerProductsDTOs = OwnerProductsDTOs.Skip(recSkip).Take(pageSize).ToList();
            return OwnerProductsDTOs;
          

        }
        public async Task<List<ProductInfoDto>> GetAllProductsByOwmerID( int id)
        {
            List<ProductInfoDto> OwnerProductsDTOs;
            OwnerProductsDTOs = new List<ProductInfoDto>();


            IEnumerable<OwnerProduct> ownerProducts = await _unitOfWork.OwnerProducts
                .FindAllAsync(d => d.OwnerId == id && !d.IsDeleted);


            foreach (OwnerProduct product in ownerProducts)
            {
                ProductInfoDto ProductDTO = new ProductInfoDto();
                ProductDTO.Price = product.Price;
                ProductDTO.Description = product.Description;
                ProductDTO.Name = product.Name;
                ProductDTO.Id = product.Id;
                ProductDTO.Discount = product.Discount;
                ProductDTO.DiscountPrice = product.Price - (product.Price * product.Discount / 100);
                ProductDTO.Image = product.Image;

                OwnerProductsDTOs.Add(ProductDTO);
            }
    
          
            return OwnerProductsDTOs;


        }
        public double GetPriceBeforeOffer(OwnerOffer ownerOffer)
        {
            List<OwnerOfferProduct> ownerOffers = (List<OwnerOfferProduct>)_unitOfWork.OwnerOfferProducts.FindAll(o => o.OfferId == ownerOffer.Id, new List<Expression<Func<OwnerOfferProduct, object>>>()
            {
                   o=>o.Offer.Owner,
                   o=>o.Product
            });
            double PrefPrice = ownerOffers.Select(o => o.Product.Price * o.Quantity).Sum();

            return PrefPrice;
        }
        public async Task<ResultrDto<OwnerDto>> getOwnersByCategory(int PageNumber, int pageSize, int cityId, string name, string SortBy, string Category)
        {
            List<Owner> owners;

            owners = await filterOwnersByCity(cityId, Category);

            if (SortBy != "")
            {
                owners = sortingOwnerData(owners, SortBy);
            }

            if (name != "")
            {
                owners = owners.Where(o => o.AppUser.Name.ToLower().Trim().Contains(name.ToLower().Trim())).ToList();
            }

            ResultrDto<OwnerDto> ownerResult = new ResultrDto<OwnerDto>();
            ownerResult.itemsCount = owners.Count();
            int recSkip = (PageNumber - 1) * pageSize;
            owners = owners.Skip(recSkip).Take(pageSize).ToList();

            List<OwnerDto> ownerDtos = new List<OwnerDto>();
            owners.ForEach(async o =>
            {
                OwnerDto owner = new OwnerDto();
                owner = _mapper.Map<OwnerDto>(o);

                owner.Rating = await calucaluteOwnerRating(o);

                ownerDtos.Add(owner);

            });
            ownerResult.List = ownerDtos;

            return ownerResult;
        }
        public async Task<ResultrDto<OfferDto>> filterOffersData(int pageNumber,int pageSize,int cityId,string CategoryName,string sortBy)
        {
            List<OwnerOffer> offers;

            offers = await filterOffersByCity(cityId, CategoryName);

            if (sortBy != string.Empty)
            {
                offers = sortingOwnerOfferData(offers, sortBy);
            }

            ResultrDto<OfferDto> offerFilterResult = new ResultrDto<OfferDto>();
            offerFilterResult.itemsCount = offers.Count();
            int recSkip = (pageNumber - 1) * pageSize;
            offers = offers.Skip(recSkip).Take(pageSize).ToList();

            List<OfferDto> OfferDtos = new List<OfferDto>();
            offers.ForEach(o =>
            {
                OfferDto ownerOffer = new OfferDto();
                ownerOffer = _mapper.Map<OfferDto>(o);
                ownerOffer.ownerId = o.OwnerId;

                ownerOffer.PrefPrice = GetPriceBeforeOffer(o);
                ownerOffer.TraderImage = o.Owner.Image;

                OfferDtos.Add(ownerOffer);

            });
            offerFilterResult.List = OfferDtos;

            return offerFilterResult;
        }
        public async Task<ResultrDto<OfferDto>> GetAllOffersWithPagination(int pageNumber, int pageSize, int cityId, string SortBy, string Category)
        {
            return await filterOffersData(pageNumber, pageSize, cityId, Category, SortBy);
        }
        public async Task<List<OfferDto>> GetAllOffersWithoutPagination(string CategoryName,string sortBy)
        {          
            return filterOffersData(1,3,0, CategoryName, sortBy).Result.List;
        }
        public async Task<List<AddressInfoDTO>> GetAddressesByOwnerID(int id)
        {
            var ID = Convert.ToString(id);
            List<AddressInfoDTO> OwnerAddressesDTOs;

            OwnerAddressesDTOs = new List<AddressInfoDTO>();
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
             
               

                OwnerAddressesDTOs.Add(AddressDTO);
            }
            return OwnerAddressesDTOs;
        }
        public async Task<double> GetMinPriceoFProductByOwmerID(int id)
        {
            IEnumerable<OwnerProduct> ownerProducts = await _unitOfWork.OwnerProducts.FindAllAsync(o => o.OwnerId == id );
            return ownerProducts.Min(o => o.Price);

        }
        public async Task<double> GetMaxPriceoFProductByOwmerID(int id)
        {
            IEnumerable<OwnerProduct> ownerProducts = await _unitOfWork.OwnerProducts.FindAllAsync(o => o.OwnerId == id);

            return ownerProducts.Max(o => o.Price);

        }
        public async Task<List<offerProductInfo>> getofferProduct(int OfferId)
        {
           List<OwnerOfferProduct> offers= _unitOfWork.OwnerOfferProducts.FindAllAsync(O => O.OfferId == OfferId && O.IsDeleted == false, new List<Expression<Func<OwnerOfferProduct, object>>>()
            {
                o=>o.Product


            }).Result.ToList();
            List< offerProductInfo > result= new List<offerProductInfo>();
            foreach (OwnerOfferProduct Offer in offers)
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
        public async Task<List<ProductInfoDto>> GetProductsByOwnerIDAndPrice(int id,double selectedprice)
        {
            List<ProductInfoDto> OwnerProductsDTOs;
            OwnerProductsDTOs = new List<ProductInfoDto>();


            IEnumerable<OwnerProduct> ownerProducts = await _unitOfWork.OwnerProducts
                .FindAllAsync(d => d.OwnerId == id && !d.IsDeleted && d.Price- (d.Price * d.Discount / 100) <= selectedprice);


            foreach (OwnerProduct product in ownerProducts)
            {
                ProductInfoDto ProductDTO = new ProductInfoDto();
                ProductDTO.Price = product.Price;
                ProductDTO.Description = product.Description;
                ProductDTO.Name = product.Name;
                ProductDTO.Id = product.Id;
                ProductDTO.Discount = product.Discount;
                ProductDTO.DiscountPrice = product.Price - (product.Price * product.Discount / 100);
                ProductDTO.Image = product.Image;

                OwnerProductsDTOs.Add(ProductDTO);
            }


            return OwnerProductsDTOs;


        }


    }
}