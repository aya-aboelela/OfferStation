using Microsoft.AspNetCore.Mvc;
using offerStation.Core.Dtos;
using offerStation.Core.Interfaces.Services;
using offerStation.Core.Wrappers;

namespace offerStation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OwnerController : ControllerBase
    {        
        private readonly IOwnerService _ownerService;
        public OwnerController(IOwnerService ownerService)
        {
            this._ownerService = ownerService;
        }

        [HttpGet("id")]
        public async Task<ActionResult<ApiResponse>> GetOwner(int id)
        {
            PublicInfoDto owner = await _ownerService.GetOwner(id);

            if (owner is null)
            {
                return BadRequest(new ApiResponse(404, false, "null object"));     
            }
            return Ok(new ApiResponse(200, true, owner));
        }
        [HttpGet("GetAllOwners")]
        public async Task<ActionResult<ApiResponse>> GetAllOwners()
        {
            List<OwnerDto> ownerList = await _ownerService.GetAllOwners();

            if (ownerList is null)
            {
                return BadRequest(new ApiResponse(404, false, "null object"));
            }
            return Ok(new ApiResponse(200, true, ownerList));
        }
        [HttpGet("GetOwnerInfo")]
        public async Task<ActionResult<ApiResponse>> GetOwnerInfo(int id)
        {
            OwnerInfoDto owner = await _ownerService.GetOwnerInfo(id);

            if (owner is null)
            {
                return BadRequest(new ApiResponse(404, false, "null object"));
            }
            return Ok(new ApiResponse(200, true, owner));
        }
        [HttpGet("GetWaitingOwners")]
        public async Task<ActionResult<ApiResponse>> GetWaitingOwners()
        {
            List<TraderDetailsDto> ownerList = await _ownerService.GetWaitingOwners();

            if (ownerList is null)
            {
                return BadRequest(new ApiResponse(404, false, "null object"));
            }
            return Ok(new ApiResponse(200, true, ownerList));
        }
        [HttpGet("GetSuspendedOwners")]
        public async Task<ActionResult<ApiResponse>> GetSuspendedOwners()
        {
            List<TraderDetailsDto> ownerList = await _ownerService.GetSuspendedOwners();

            if (ownerList is null)
            {
                return BadRequest(new ApiResponse(404, false, "null object"));
            }
            return Ok(new ApiResponse(200, true, ownerList));
        }

        [HttpPut("id")]
        public async Task<ActionResult<ApiResponse>> EditOwner(int id, PublicInfoDto owner)
        {
            var success = await _ownerService.EditOwner(id, owner);
            if (success)
            {
                return Ok(new ApiResponse(200, true, success));
            }
            return BadRequest(new ApiResponse(500, false, "server error"));
        }
        [HttpDelete("id")]
        public async Task<ActionResult<ApiResponse>> DeleteOwner(int id)
        {
            bool success = await _ownerService.PermanentDeleteOwner(id);
            if (success)
            {
                return Ok(new ApiResponse(200, true, success));
            }
            return BadRequest(new ApiResponse(500, false, success));
        }
        [HttpDelete("SuspendOwner/id")]
        public async Task<ActionResult<ApiResponse>> SuspendOwner(int id)
        {
            bool success = await _ownerService.SuspendOwner(id);
            if (success)
            {
                return Ok(new ApiResponse(200, true, success));
            }
            return BadRequest(new ApiResponse(500, false, success));
        }
        [HttpPut("RemoveOwnerSuspension/id")]
        public async Task<ActionResult<ApiResponse>> RemoveOwnerSuspension(int id)
        {
            bool success = await _ownerService.RemoveOwnerSuspension(id);
            if (success)
            {
                return Ok(new ApiResponse(200, true, success));
            }
            return BadRequest(new ApiResponse(500, false, success));
        }
        [HttpGet("Approve/id")]
        public async Task<ActionResult<ApiResponse>> ApproveOwner(int id)
        {
            bool success = await _ownerService.ApproveOwner(id);
            if (success)
            {
                return Ok(new ApiResponse(200, true, success));
            }
            return BadRequest(new ApiResponse(500, false, success));
        }
        [HttpGet("Product/id")]
        public async Task<ActionResult<ApiResponse>> ProductDetails(int id)
        {
            ProductInfoDto product = await _ownerService.GetProductDetails(id);
            if(product is null)
            {
                return BadRequest(new ApiResponse(404, false, "null object"));
            }
            return Ok(new ApiResponse(200, true, product));
        }
        [HttpPost("Product/id")]
        public async Task<ActionResult<ApiResponse>> AddProduct(int ownerId, ProductDto product)
        {
            bool success = await _ownerService.AddProduct(ownerId, product);
            if (success)
            {
                return Ok(new ApiResponse(201, true, success));
            }
            return BadRequest(new ApiResponse(500, false, "server error"));
        }
        [HttpPut("Product/id")]
        public async Task<ActionResult<ApiResponse>> EditProduct(int id, ProductDto product)
        {
            bool success = await _ownerService.EditProduct(id, product);
            if (success)
            {
                return Ok(new ApiResponse(200, true, success));
            }
            return BadRequest(new ApiResponse(500, false, "server error"));
        }
        [HttpDelete("Product/id")]
        public async Task<ActionResult<ApiResponse>> DeleteProduct(int id)
        {
            bool success = await _ownerService.DeleteProduct(id);
            if (success)
            {
                return Ok(new ApiResponse(200, true, success));
            }
            return BadRequest(new ApiResponse(500, false, "server error"));
        }
        // ==================================Admin ==================================
        [HttpPost("OwnerCategory")]
        public async Task<ActionResult<ApiResponse>> AddOwnerCategory(CategoryInfoDto category)
        {
            bool success = await _ownerService.AddCategory(category);
            if (success)
            {
                return Ok(new ApiResponse(201, true, success));
            }
            return BadRequest(new ApiResponse(500, false, "server error"));
        }

        // ==================================Admin ==================================
        [HttpPut("OwnerCategory/id")]
        public async Task<ActionResult<ApiResponse>> EditOwnerCategory(int id, CategoryInfoDto category)
        {
            bool success = await _ownerService.EditCategory(id, category);
            if (success)
            {
                return Ok(new ApiResponse(201, true, success));
            }
            return BadRequest(new ApiResponse(500, false, "server error"));
        }
        // ==================================Admin ==================================
        [HttpDelete("OwnerCategory/id")]
        public async Task<ActionResult<ApiResponse>> DeleteOwnerCategory(int id)
        {
            bool success = await _ownerService.DeleteCategory(id);
            if (success)
            {
                return Ok(new ApiResponse(200, true, success));
            }
            return BadRequest(new ApiResponse(500, false, "server error"));
        }
        
        [HttpGet("AllMenuCategoriesByOwnerId/id")]
        public async Task<ActionResult<ApiResponse>> GetMenuCategory(int id)
        {
            List<MenuCategoryDetailsDto> menu = await _ownerService.GetMenuCategoiesByOwnerId(id);

            if (menu is null)
            {
                return BadRequest(new ApiResponse(404, false, "null object"));
            }
            return Ok(new ApiResponse(200, true, menu));
        }

        [HttpGet("AllProductsByMenuCategoryID/id")]
        public async Task<ActionResult<ApiResponse>> GetProductsByMenuCategoryID(int id)
        {
            List<ProductInfoDto> product = await _ownerService.GetProductsByMenuCategoryID(id);

            if (product is null)
            {
                return BadRequest(new ApiResponse(404, false, "null object"));
            }
            return Ok(new ApiResponse(200, true, product));
        }
       
        [HttpGet("GetAllProductsByOwmerIDWithPagination/id")]
        public async Task<ActionResult<ApiResponse>> GetAllProductsByOwmerID(int pageNumber, int pageSize, int ownerid)
        {
            List<ProductInfoDto> products = await _ownerService.GetAllProductsByOwmerIDWithPagination(pageNumber, pageSize, ownerid);

            if (products is null)
            {
                return BadRequest(new ApiResponse(404, false, "null object"));
            }
            return Ok(new ApiResponse(200, true, products));
        }
        
        [HttpGet("AllProductsByOwnerID/id")]
        public async Task<ActionResult<ApiResponse>> GetAllProductsByOwmerID( int ownerid)
        {
            List<ProductInfoDto> products = await _ownerService.GetAllProductsByOwmerID( ownerid);

            if (products is null)
            {
                return BadRequest(new ApiResponse(404, false, "null object"));
            }
            return Ok(new ApiResponse(200, true, products));
        }

        [HttpGet("AllCustomerReviewsByOwnerId/id")]
        public async Task<ActionResult<ApiResponse>> GetAllCustomerReviews(int ownerId)
        {
            List<ReviewDto> reviews = await _ownerService.GetAllCustomerReviewsByOwnerId(ownerId);

            if (reviews is null)
            {
                return BadRequest(new ApiResponse(404, false, "null object"));
            }
            return Ok(new ApiResponse(200, true, reviews));
        }
        [HttpGet("Categories")]
        public async Task<IActionResult> GetAllCategories()
        {
            return Ok(new ApiResponse(200, true, await _ownerService.GetAllCategories()));
        }
        // ==================================Admin ==================================
        [HttpGet("CategoriesByPage")]
        public async Task<IActionResult> GetAllCategoriesByPage([FromQuery] PagingParameters pagingParameters)
        {
            var categories = await _ownerService.GetAllCategories();
            var pagedCategories = categories.ToPagedResponse(pagingParameters);

            return Ok(pagedCategories);
        }
        [HttpGet("All/Offers/filter/WithPagination")]
        public async Task<IActionResult> getAllOffersWithPagination(int PageNumber, int pageSize, string category, int cityId = 0, string SortBy = "")
        {
            var data = await _ownerService.GetAllOffersWithPagination(PageNumber, pageSize, cityId, SortBy,category);
            return Ok(new ApiResponse(200, true,data));

        }

        [HttpGet("All/Offers/filter/WithoutPagination")]
        public async Task<IActionResult> getAllOffersWithotPagination(string CategoryName,string sortBy="")
        {
            var data = await _ownerService.GetAllOffersWithoutPagination(CategoryName,sortBy);
            return Ok(new ApiResponse(200, true, data));

        }
        [HttpGet("All/Filter/Pagination")]
        public async Task<IActionResult> getAllOwners(int PageNumber, int pageSize, string category, int cityId = 0, string SortBy = "",string name="")
        {
            var data = await _ownerService.getOwnersByCategory(PageNumber, pageSize, cityId, name,SortBy, category);
            return Ok(new ApiResponse(200, true, data));

        }
        [HttpGet("GetAllOffersByOwnerId")]
        public async Task<ActionResult<ApiResponse>> GetAllOffersByOwnerId( int id)
        {
            IEnumerable<OfferDto> offer = await _ownerService.GetAllOffersByOwnerIdWithPagination(id);
            if (offer is null)
            {
                return BadRequest(new ApiResponse(404, false, "null object"));
            }
            return Ok(new ApiResponse(200, true, offer));
        }
        [HttpGet("GetAddressByOwnerId/id")]
        public async Task<ActionResult<ApiResponse>> GetAddressByOwnerId(int id)
        {
            IEnumerable<AddressInfoDTO> addres = await _ownerService.GetAddressesByOwnerID(id);
            if (addres is null)
            {
                return BadRequest(new ApiResponse(404, false, "null object"));
            }
            return Ok(new ApiResponse(200, true, addres));
        }
        [HttpGet("GetMinPriceoFProductByOwmerID/id")]
        public async Task<ActionResult<ApiResponse>> GetMinPriceoFProductByOwmerID(int id)
        {
            double price = await _ownerService.GetMinPriceoFProductByOwmerID(id);
            if (price is 0)
            {
                return BadRequest(new ApiResponse(404, false, "null object"));
            }
            return Ok(new ApiResponse(200, true, price));
        }
        [HttpGet("GetMaxPriceoFProductByOwmerID/id")]
        public async Task<ActionResult<ApiResponse>> GetMaxPriceoFProductByOwmerID(int id)
        {
            double price = await _ownerService.GetMaxPriceoFProductByOwmerID(id);
            if (price is 0)
            {
                return BadRequest(new ApiResponse(404, false, "null object"));
            }
            return Ok(new ApiResponse(200, true, price));
        }
        [HttpGet("GetProductsByOwmerIDAndPrice/ownerid/selectedprice")]
        public async Task<ActionResult<ApiResponse>> GetProductsByOwnerIDAndPrice(int ownerid, double selectedprice)
        {
            List<ProductInfoDto> products = await _ownerService.GetProductsByOwnerIDAndPrice(ownerid, selectedprice);

            if (products is null)
            {
                return BadRequest(new ApiResponse(404, false, "null object"));
            }
            return Ok(new ApiResponse(200, true, products));
        }
        [HttpGet("GetOfferDetailsByOfferId/id")]
        public async Task<ActionResult<ApiResponse>> GetOfferDetailsByOfferId(int id)
        {
            IEnumerable<OwnerOfferProductsDto> offer = await _ownerService.GetOfferDetailsByOfferId(id);
            if (offer is null)
            {
                return BadRequest(new ApiResponse(404, false, "null object"));
            }
            return Ok(new ApiResponse(200, true, offer));
        }
        [HttpGet("Offer/Products/Details")]
        public async Task<ActionResult<ApiResponse>> GetOfferProductDetails(int Offerid)
        {
          
            return Ok(new ApiResponse(200, true, await _ownerService.getofferProduct(Offerid)));
        }

    }
}
