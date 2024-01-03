using offerStation.Core.Dtos;
using offerStation.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace offerStation.Core.Interfaces.Services
{
    public interface ISupplierService
    {
        Task<PublicInfoDto?> GetSupplier(int id);
        Task<List<SupplierDto>?> GetAllSuppliers();
        Task<List<TraderDetailsDto>?> GetWaitingSuppliers();
        Task<List<TraderDetailsDto>?> GetSuspendedSuppliers();
        Task<bool> EditSupplier(int id, PublicInfoDto supplierInfo);
        Task<bool> PermanentDeleteSupplier(int id);
        Task<bool> SuspendSupplier(int id);
        Task<bool> RemoveSupplierSuspension(int id);
        Task<bool> ApproveSupplier(int id);
        Task<ProductInfoDto?> GetProductDetails(int id);
        Task<SupplierInfoDto?> GetSupplierInfo(int id);
        Task<List<ReviewDto>?> GetAllOwnerReviewsBySupplierId(int supplierId);
        Task<List<ReviewDto>?> GetAllOwnersReviewsBySupplierIdWithPagination(int pageNumber, int pageSize, int supplierId);
        Task<List<MenuCategoryDetailsDto>> GetMenuCategoiesBySupplierId(int id);
        Task<List<ProductInfoDto>> GetProductsByMenuCategoryID(int id);
        Task<bool> AddProduct(int supplierId, ProductDto productDto);
        Task<bool> EditProduct(int id, ProductDto productDto);
        Task<bool> DeleteProduct(int id);
        Task<bool> AddCategory(CategoryInfoDto categoryDto);
        Task<bool> EditCategory(int id, CategoryInfoDto categoryDto);
        Task<bool> DeleteCategory(int id);
   
        Task<List<ProductInfoDto>?> GetAllProducts(int supplierId);
        Task<List<SupplierOfferProductsDto>?> GetOfferDetailsByOfferId(int id);
        Task<List<ProductInfoDto>> GetAllProductsBySupplierIDWithPagination(int pageNumber, int pageSize, int id);
        Task<List<SupplierCategory>> GetAllCategories();
        Task<List<OfferDetailsDto>?> GetAllOffersBySupplierIdWithPagination(int pageNumber, int pageSize, int id);
        Task<ResultrDto<OfferDto>> GetAllOffersWithPagination(int PageNumber, int pageSize, int cityId, String SortBy, string Category);
        Task<ResultrDto<SupplierDto>> getSupplierByCategory(int PageNumber, int pageSize, int cityId, string name, String SortBy, string Category);
        Task<List<OfferDto>> GetAllOffersWithoutPagination(string CategoryName, string sortBy);
        Task<List<AddressInfoDTO>> GetAddressesBySupplierID(int id);
        Task<double> GetMinPriceoFProductBySupplierID(int id);
        Task<double> GetMaxPriceoFProductBySupplierID(int id);
        Task<List<ProductInfoDto>> GetProductsBySupplierIDAndPrice(int id, double selectedprice);
        Task<List<offerProductInfo>> getofferProduct(int OfferId);
    }

    public interface IsupplierAnalysisService
    {
        Task<List<AnalysisResult>> getTop5Product(int supplierId);
        Task<List<AnalysisResult>> getTop5Offer(int supplierId);
        Task<int> getTotalCustomer(int supplierId);
        Task<int> getTotalOrders(int supplierId);
        Task<double> getTotalProfit(int supplierId);
        Task<int> getOffersCount(int supplierId);
        Task<int> getProductsCount(int supplierId);
        Task<List<AnalysisResult>> getDiffernentOrdersStatus(int supplierId);
        Task<List<AnalysisResult>> getOrdersOffersProductCount(int supplierId);
        Task<List<customerInfoAnalysis>> getTopCustomerInfo(int supplierId);
       
    }
    public interface ISupplierOfferService
    {
        Task<OfferDetailsDto?> GetOfferDetails(int id);
        Task<List<OfferDetailsDto>?> GetAllOffersBySupplierId(int supplierId);
        Task<bool> AddOffer(int supplierId, OfferInfoDto offerDto);
        Task<bool> EditOffer(int id, OfferInfoDto offerDto);
        Task<bool> DeleteOffer(int id);
    }
    public interface ISupplierMenuCategoryService
    {
        Task<MenuCategoryDetailsDto?> GetMenuCategoryDetails(int id);
        Task<bool> AddMenuCategory(int SupplierId, MenuCategoryDto menuCategoryDto);
        Task<bool> EditMenuCategory(int id, MenuCategoryDto menuCategoryDto);
        Task<bool> DeleteMenuCategory(int id);
    }
}
