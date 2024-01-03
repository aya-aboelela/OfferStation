using offerStation.Core.Dtos;
using offerStation.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace offerStation.Core.Interfaces.Services
{
    public interface IOwnerService
    {
        Task<PublicInfoDto?> GetOwner(int id);
        Task<OwnerInfoDto?> GetOwnerInfo(int id);
        Task<List<OwnerDto>?> GetAllOwners();
        Task<List<TraderDetailsDto>?> GetWaitingOwners();
        Task<List<TraderDetailsDto>?> GetSuspendedOwners();
        Task<bool> EditOwner(int id, PublicInfoDto ownerInfo);
        Task<bool> PermanentDeleteOwner(int id);
        Task<bool> SuspendOwner(int id);
        Task<bool> RemoveOwnerSuspension(int id);
        Task<bool> ApproveOwner(int id);
        Task<ProductInfoDto?> GetProductDetails(int id);
        Task<bool> AddProduct(int ownerId, ProductDto productDto);
        Task<bool> EditProduct(int id, ProductDto productDto);
        Task<bool> DeleteProduct(int id);
        Task<bool> AddCategory(CategoryInfoDto categoryDto);
        Task<bool> EditCategory(int id, CategoryInfoDto categoryDto);
        Task<bool> DeleteCategory(int id);
        Task<bool> AddReview(int ownerId, int supplierId, ReviewInfoDto reviewDto);
        Task<bool> DeleteReview(int id);
        Task<List<ReviewDto>?> GetAllOwnersReviews();
        Task<List<ReviewDto>?> GetAllCustomerReviewsByOwnerIdWithPagination(int pageNumber, int pageSize, int id);
        Task<List<ReviewDto>?> GetAllCustomerReviewsByOwnerId(int id);
        Task<List<CategoryDto>> GetAllCategories();
        Task<List<MenuCategoryDetailsDto>> GetMenuCategoiesByOwnerId(int id);
        Task<List<ProductInfoDto>> GetProductsByMenuCategoryID(int id);
        Task<List<ProductInfoDto>> GetAllProductsByOwmerIDWithPagination(int pageNumber, int pageSize, int id);
        Task<List<ProductInfoDto>> GetAllProductsByOwmerID(int id);
        Task<ResultrDto<OwnerDto>> getOwnersByCategory(int PageNumber, int pageSize, int cityId, string name, String SortBy, string Category);
        Task<ResultrDto<OfferDto>> GetAllOffersWithPagination(int PageNumber, int pageSize, int cityId, String SortBy, string Category);
        Task<List<OfferDto>> GetAllOffersWithoutPagination(string CategoryName, string sortBy);
        Task<List<OwnerOfferProductsDto>?> GetOfferDetailsByOfferId(int id);
   
        Task<List<AddressInfoDTO>> GetAddressesByOwnerID(int id);
        Task<double> GetMinPriceoFProductByOwmerID(int id);
        Task<double> GetMaxPriceoFProductByOwmerID(int id);
        Task<List<ProductInfoDto>> GetProductsByOwnerIDAndPrice(int id, double selectedprice);
        Task<List<OfferDetailsDto>?> GetAllOffersByOwnerIdWithPagination( int id);

        Task<List<offerProductInfo>> getofferProduct(int OfferId);
    }
    public interface IownerAnalysisService{
        Task<List<AnalysisResult>> getTop5OwnerProduct(int OwnerId);
        Task<List<AnalysisResult>> getTop5OwnerOffer(int OwnerId);
        Task<int> getOwnerTotalCustomer(int OwnerId);
        Task<int> getOwnerTotalOrders(int ownerId);
        Task<double> getTotalProfit(int ownerId);
        Task<int> getOffersCount(int ownerId);
        Task<int> getProductsCount(int ownerId);
        Task<List<AnalysisResult>> getDiffernentOrdersStatus(int ownerId);
        Task<List<AnalysisResult>> ownerOrdersOffersProductCount(int ownerId);
        Task<List<customerInfoAnalysis>> getTopCustomerInfo(int ownerid);
       
    }
    public interface IOwnerOfferService
    {
        Task<OfferDetailsDto?> GetOfferDetails(int id);
        Task<List<OfferDetailsDto>?> GetAllOffersByOwnerId(int ownerId);
        Task<bool> AddOffer(int ownerId, OfferInfoDto offerDto);
        Task<bool> EditOffer(int id, OfferInfoDto offerDto);
        Task<bool> DeleteOffer(int id);
    }
    public interface IOwnerMenuCategoryService
    {
        Task<MenuCategoryDetailsDto?> GetMenuCategoryDetails(int id);
        Task<bool> AddMenuCategory(int ownerId, MenuCategoryDto menuCategoryDto);
        Task<bool> EditMenuCategory(int id, MenuCategoryDto menuCategoryDto);
        Task<bool> DeleteMenuCategory(int id);
    }
    public interface IOwnerCartService
    {
        Task<ApiResponse> AddProductToCart(int userIdentifier, ProductDetailsDto Product);
        Task<ApiResponse> GetCartDetails(int userIdentifier);
        Task<ApiResponse> AddOfferToCart(int userIdentifier, ProductDetailsDto Offer);
        Task<ApiResponse> RemoveProductFromCart(int userIdentifier, int productId);
        Task<ApiResponse> RemoveOfferFromCart(int userIdentifier, int offerId);
        Task<ApiResponse> ProductPlus(int userIdentifier, int offerId);
        Task<ApiResponse> OfferPlus(int userIdentifier, int offerId);
        Task<ApiResponse> ProductMinus(int userIdentifier, int offerId);
        Task<ApiResponse> OfferMinus(int userIdentifier, int offerId);
        Task<ApiResponse> GetCreateOrder(int userIdentifier);
        Task<ApiResponse> PostCreateOrder(int userIdentifier);
    }
}
