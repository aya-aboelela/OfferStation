using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using offerStation.Core.Dtos;
using offerStation.Core.Interfaces.Services;
using offerStation.EF.Services;

namespace offerStation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierAnalysisController : ControllerBase
    {
        private readonly IsupplierAnalysisService _supplierAnalysisService;
        public SupplierAnalysisController(IsupplierAnalysisService supplierAnalysisService)
        {
            _supplierAnalysisService = supplierAnalysisService;
        }

        [HttpGet("Total/Customer")]
        public async Task<ActionResult<ApiResponse>> getTotalCustomer(int supplierId)
        {

            int count = await _supplierAnalysisService.getTotalCustomer(supplierId);
            return Ok(new ApiResponse(200, true, count));
        }

        [HttpGet("Top/product")]
        public async Task<ActionResult<ApiResponse>> getTopProduct(int supplierId)
        {

            return Ok(new ApiResponse(200, true, await _supplierAnalysisService.getTop5Product(supplierId)));
        }
        [HttpGet("Top/offer")]
        public async Task<ActionResult<ApiResponse>> getTopoffer(int supplierId)
        {

            return Ok(new ApiResponse(200, true, await _supplierAnalysisService.getTop5Offer(supplierId)));
        }

        [HttpGet("Total/orders")]
        public async Task<ActionResult<ApiResponse>> getOwnerTotalOrders(int supplierId)
        {

            return Ok(new ApiResponse(200, true, await _supplierAnalysisService.getTotalOrders(supplierId)));
        }

        [HttpGet("Total/profits")]
        public async Task<ActionResult<ApiResponse>> getTotalprofits(int supplierId)
        {

            return Ok(new ApiResponse(200, true, await _supplierAnalysisService.getTotalProfit(supplierId)));
        }

        [HttpGet("Count/Products")]
        public async Task<ActionResult<ApiResponse>> getProductCount(int supplierId)
        {

            return Ok(new ApiResponse(200, true, await _supplierAnalysisService.getProductsCount(supplierId)));
        }

        [HttpGet("Count/Offers")]
        public async Task<ActionResult<ApiResponse>> getOffersCount(int supplierId)
        {

            return Ok(new ApiResponse(200, true, await _supplierAnalysisService.getOffersCount(supplierId)));
        }


        [HttpGet("Count/orders/status")]
        public async Task<ActionResult<ApiResponse>> getorderstatus(int supplierId)
        {

            return Ok(new ApiResponse(200, true, await _supplierAnalysisService.getDiffernentOrdersStatus(supplierId)));
        }

        [HttpGet("Count/OffersOrders/Productorders")]
        public async Task<ActionResult<ApiResponse>> countProductOffersOrders(int supplierId)
        {

            return Ok(new ApiResponse(200, true, await _supplierAnalysisService.getOrdersOffersProductCount(supplierId)));
        }

        [HttpGet("Top/Customers")]
        public async Task<ActionResult<ApiResponse>> getTopCustomers(int supplierId)
        {

            return Ok(new ApiResponse(200, true, await _supplierAnalysisService.getTopCustomerInfo(supplierId)));
        }
    }
}
