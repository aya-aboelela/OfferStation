using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using offerStation.Core.Dtos;
using offerStation.Core.Interfaces.Services;
using offerStation.Core.Models;
using offerStation.Core.Wrappers;
using offerStation.EF.Services;
using System.Drawing.Printing;

namespace offerStation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private readonly ISupplierService _supplierService;
        public SupplierController(ISupplierService supplierService)
        {
            this._supplierService = supplierService;
        }

        [HttpGet("id")]
        public async Task<ActionResult<ApiResponse>> GetSupplier(int id)
        {
            PublicInfoDto supplier = await _supplierService.GetSupplier(id);

            if (supplier is null)
            {
                return BadRequest(new ApiResponse(404, false, "null object"));
            }

            return Ok(new ApiResponse(200, true, supplier));
        }
        [HttpGet("GetAllSuppliers")]
        public async Task<ActionResult<ApiResponse>> GetAllSuppliers()
        {
            List<SupplierDto> supplierList = await _supplierService.GetAllSuppliers();

            if (supplierList is null)
            {
                return BadRequest(new ApiResponse(404, false, "null object"));
            }
            return Ok(new ApiResponse(200, true, supplierList));
        }

        [HttpGet("GetAllOwnerReviewsbysupplierID")]
        public async Task<ActionResult<ApiResponse>> GetAllOwnerReviewsBySupplierId(int supplierId)
        {
            List<ReviewDto> supplierList = await _supplierService.GetAllOwnerReviewsBySupplierId(supplierId);

            if (supplierList is null)
            {
                return BadRequest(new ApiResponse(404, false, "null object"));
            }
            return Ok(new ApiResponse(200, true, supplierList));

        }
        [HttpGet("GetAllOwnersReviewsBySupplierIdWithPagination")]
        public async Task<ActionResult<ApiResponse>> GetAllOwnersReviewsBySupplierIdWithPagination(int pageNumber, int pageSize, int supplierId)
        {
            List<ReviewDto> supplierList = await _supplierService.GetAllOwnersReviewsBySupplierIdWithPagination(pageNumber, pageSize, supplierId);

            if (supplierList is null)
            {
                return BadRequest(new ApiResponse(404, false, "null object"));
            }
            return Ok(new ApiResponse(200, true, supplierList));

        }
        [HttpGet("GetMenuCategoiesBySupplierId")]
        public async Task<ActionResult<ApiResponse>> GetMenuCategoiesBySupplierId(int supplierid)
        {
            List<MenuCategoryDetailsDto> supplierList = await _supplierService.GetMenuCategoiesBySupplierId(supplierid);

            if (supplierList is null)
            {
                return BadRequest(new ApiResponse(404, false, "null object"));
            }
            return Ok(new ApiResponse(200, true, supplierList));
        }

        [HttpGet("GetWaitingSuppliers")]
        public async Task<ActionResult<ApiResponse>> GetWaitingSuppliers()
        {
            List<TraderDetailsDto> supplierList = await _supplierService.GetWaitingSuppliers();

            if (supplierList is null)
            {
                return BadRequest(new ApiResponse(404, false, "null object"));
            }
            return Ok(new ApiResponse(200, true, supplierList));
        }
        [HttpGet("GetSuspendedSuppliers")]
        public async Task<ActionResult<ApiResponse>> GetSuspendedSuppliers()
        {
            List<TraderDetailsDto> supplierList = await _supplierService.GetSuspendedSuppliers();

            if (supplierList is null)
            {
                return BadRequest(new ApiResponse(404, false, "null object"));
            }
            return Ok(new ApiResponse(200, true, supplierList));
        }

        [HttpPut("id")]
        public async Task<ActionResult<ApiResponse>> EditSupplier(int id, PublicInfoDto supplierDto)
        {
            var success = await _supplierService.EditSupplier(id, supplierDto);

            if (success)
            {
                return Ok(new ApiResponse(200, true, success));
            }
            return BadRequest(new ApiResponse(500, false, "server error"));
        }
        [HttpDelete("id")]
        public async Task<ActionResult<ApiResponse>> DeleteSupplier(int id)
        {
            bool success = await _supplierService.PermanentDeleteSupplier(id);
            if (success)
            {
                return Ok(new ApiResponse(200, true, success));
            }
            return BadRequest(new ApiResponse(500, false, success));
        }
        [HttpDelete("SuspendSupplier/id")]
        public async Task<ActionResult<ApiResponse>> SuspendSupplier(int id)
        {
            bool success = await _supplierService.SuspendSupplier(id);
            if (success)
            {
                return Ok(new ApiResponse(200, true, success));
            }
            return BadRequest(new ApiResponse(500, false, success));
        }
        [HttpPut("RemoveSupplierSuspension/id")]
        public async Task<ActionResult<ApiResponse>> RemoveSupplierSuspension(int id)
        {
            bool success = await _supplierService.RemoveSupplierSuspension(id);
            if (success)
            {
                return Ok(new ApiResponse(200, true, success));
            }
            return BadRequest(new ApiResponse(500, false, success));
        }
        [HttpGet("Approve/id")]
        public async Task<ActionResult<ApiResponse>> ApproveSupplier(int id)
        {
            bool success = await _supplierService.ApproveSupplier(id);
            if (success)
            {
                return Ok(new ApiResponse(200, true, success));
            }
            return BadRequest(new ApiResponse(500, false, success));
        }
        [HttpGet("Product/id")]
        public async Task<ActionResult<ApiResponse>> ProductDetails(int id)
        {
            ProductInfoDto product = await _supplierService.GetProductDetails(id);
            if (product is null)
            {
                return BadRequest(new ApiResponse(404, false, "null object"));
            }
            return Ok(new ApiResponse(200, true, product));
        }
        [HttpPost("Product/id")]
        public async Task<ActionResult<ApiResponse>> AddProduct(int supplierId, ProductDto product)
        {
            bool success = await _supplierService.AddProduct(supplierId, product);
            if (success)
            {
                return Ok(new ApiResponse(201, true, success));
            }
            return BadRequest(new ApiResponse(500, false, "server error"));
        }
        [HttpPut("Product/id")]
        public async Task<ActionResult<ApiResponse>> EditProduct(int id, ProductDto product)
        {
            bool success = await _supplierService.EditProduct(id, product);
            if (success)
            {
                return Ok(new ApiResponse(200, true, success));
            }
            return BadRequest(new ApiResponse(500, false, "server error"));
        }
        [HttpDelete("Product/id")]
        public async Task<ActionResult<ApiResponse>> DeleteProduct(int id)
        {
            bool success = await _supplierService.DeleteProduct(id);
            if (success)
            {
                return Ok(new ApiResponse(200, true, success));
            }
            return BadRequest(new ApiResponse(500, false, "server error"));
        }
        [HttpPost("SupplierCategory")]
        public async Task<ActionResult<ApiResponse>> AddSupplierCategory(CategoryInfoDto category)
        {
            bool success = await _supplierService.AddCategory(category);
            if (success)
            {
                return Ok(new ApiResponse(201, true, success));
            }
            return BadRequest(new ApiResponse(500, false, "server error"));
        }
        [HttpPut("SupplierCategory/id")]
        public async Task<ActionResult<ApiResponse>> EditSupplierCategory(int id, CategoryInfoDto category)
        {
            bool success = await _supplierService.EditCategory(id, category);
            if (success)
            {
                return Ok(new ApiResponse(200, true, success));
            }
            return BadRequest(new ApiResponse(500, false, "server error"));
        }
        [HttpDelete("SupplierCategory/id")]
        public async Task<ActionResult<ApiResponse>> DeleteSupplierCategory(int id)
        {
            bool success = await _supplierService.DeleteCategory(id);
            if (success)
            {
                return Ok(new ApiResponse(200, true, success));
            }
            return BadRequest(new ApiResponse(500, false, "server error"));
        }
        [HttpGet("CategoriesByPage")]
        public async Task<IActionResult> GetAllCategoriesByPage([FromQuery] PagingParameters pagingParameters)
        {
            var categories = await _supplierService.GetAllCategories();
            var pagedCategories = categories.ToPagedResponse(pagingParameters);

            return Ok(pagedCategories);
        }
        [HttpGet("GetProductsByMenuCategoryID/id")]
        public async Task<ActionResult<ApiResponse>> GetProductsByMenuCategoryID( int id)
        {
            List<ProductInfoDto> product = await _supplierService.GetProductsByMenuCategoryID(id);

            if (product is null)
            {
                return BadRequest(new ApiResponse(404, false, "null object"));
            }
            return Ok(new ApiResponse(200, true, product));
        }

        [HttpGet("allProductsBySupplierID/id")]
        public async Task<ActionResult<ApiResponse>> GetAllProductsBySupplierId(int pageNumber, int pageSize ,int supplierId)
        {
            List<ProductInfoDto> products = await _supplierService.GetAllProductsBySupplierIDWithPagination(pageNumber, pageSize,supplierId);
            if(products is not null)
            {
                return Ok(new ApiResponse(200, true, products));
            }
            return BadRequest(new ApiResponse(404, false, "null object"));
        }

        [HttpGet("Categories")]
        public async Task<IActionResult> GetAllCategories()
        {
            return Ok(new ApiResponse(200, true, await _supplierService.GetAllCategories()));

        }
        [HttpGet("GetSupplierInfo")]
        public async Task<ActionResult<ApiResponse>> GetSupplierInfo(int id)
        {
            SupplierInfoDto supplier = await _supplierService.GetSupplierInfo(id);

            if (supplier is null)
            {
                return BadRequest(new ApiResponse(404, false, "null object"));
            }
            return Ok(new ApiResponse(200, true, supplier));
        }

     
        [HttpGet("GetAllOffersBySupplierIdWithPagination/id")]
        public async Task<ActionResult<ApiResponse>> GetAllOffersBySupplierIdWithPagination(int PageNumber, int pageSize, int id)
        {
            IEnumerable<OfferDto> offer = await _supplierService.GetAllOffersBySupplierIdWithPagination(PageNumber, pageSize,id);
            if (offer is null)
            {
                return BadRequest(new ApiResponse(404, false, "null object"));
            }
            return Ok(new ApiResponse(200, true, offer));
        }
        [HttpGet("GetOfferDetailsByOfferId/id")]
        public async Task<ActionResult<ApiResponse>> GetOfferDetailsByOfferId(int id)
        {
            IEnumerable<SupplierOfferProductsDto> offer = await _supplierService.GetOfferDetailsByOfferId(id);
            if (offer is null)
            {
                return BadRequest(new ApiResponse(404, false, "null object"));
            }
            return Ok(new ApiResponse(200, true, offer));
        }
        [HttpGet("allProducts/id")]
        public async Task<ActionResult<ApiResponse>> GetAllProductsBySupplierId(int supplierId)
        {
            List<ProductInfoDto> products = await _supplierService.GetAllProducts(supplierId);
            if (products is not null)
            {
                return Ok(new ApiResponse(200, true, products));
            }
            return BadRequest(new ApiResponse(404, false, "null object"));
        }

        [HttpGet("All/Offers/filter/WithPagination")]
        public async Task<IActionResult> getAllOffersWithPagination(int PageNumber, int pageSize, string category, int cityId = 0, string SortBy = "")
        {
            var data = await _supplierService.GetAllOffersWithPagination(PageNumber, pageSize, cityId, SortBy, category);
            return Ok(new ApiResponse(200, true, data));

        }

        [HttpGet("All/Offers/filter/WithoutPagination")]
        public async Task<IActionResult> getAllOffersWithotPagination(string CategoryName, string sortBy = "")
        {
            var data = await _supplierService.GetAllOffersWithoutPagination(CategoryName, sortBy);
            return Ok(new ApiResponse(200, true, data));

        }
        [HttpGet("GetAddressBySupplierId/id")]
        public async Task<ActionResult<ApiResponse>> GetAddressesBySupplierID(int id)
        {
            IEnumerable<AddressInfoDTO> addres = await _supplierService.GetAddressesBySupplierID(id);
            if (addres is null)
            {
                return BadRequest(new ApiResponse(404, false, "null object"));
            }
            return Ok(new ApiResponse(200, true, addres));
        }
        [HttpGet("GetMinPriceoFProductBySupplierID/id")]
        public async Task<ActionResult<ApiResponse>> GetMinPriceoFProductBySupplierID(int id)
        {
            double price = await _supplierService.GetMinPriceoFProductBySupplierID(id);
            if (price is 0)
            {
                return BadRequest(new ApiResponse(404, false, "null object"));
            }
            return Ok(new ApiResponse(200, true, price));
        }
        [HttpGet("GetMaxPriceoFProductBySupplierID/id")]
        public async Task<ActionResult<ApiResponse>> GetMaxPriceoFProductBySupplierID(int id)
        {
            double price = await _supplierService.GetMaxPriceoFProductBySupplierID(id);
            if (price is 0)
            {
                return BadRequest(new ApiResponse(404, false, "null object"));
            }
            return Ok(new ApiResponse(200, true, price));
        }
        [HttpGet("GetProductsBySupplierIDAndPrice/supplierid/selectedprice")]
        public async Task<ActionResult<ApiResponse>> GetProductsBySupplierIDAndPrice(int supplierid, double selectedprice)
        {
            List<ProductInfoDto> products = await _supplierService.GetProductsBySupplierIDAndPrice(supplierid, selectedprice);

            if (products is null)
            {
                return BadRequest(new ApiResponse(404, false, "null object"));
            }
            return Ok(new ApiResponse(200, true, products));
        }
        [HttpGet("All/Filter/Pagination")]
        public async Task<IActionResult> getAllSupplier(int PageNumber, int pageSize, string category, int cityId = 0, string SortBy = "", string name = "")
        {
            var data = await _supplierService.getSupplierByCategory(PageNumber, pageSize, cityId, name, SortBy, category);
            return Ok(new ApiResponse(200, true, data));

        }
        [HttpGet("Offer/Products/Details")]
        public async Task<ActionResult<ApiResponse>> GetOfferProductDetails(int Offerid)
        {

            return Ok(new ApiResponse(200, true, await _supplierService.getofferProduct(Offerid)));
        }
    }
}
