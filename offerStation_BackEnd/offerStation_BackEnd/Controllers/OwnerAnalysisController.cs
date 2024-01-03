using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using offerStation.Core.Dtos;
using offerStation.Core.Interfaces.Services;
using offerStation.EF.Services;

namespace offerStation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OwnerAnalysisController : ControllerBase
    {
        private readonly IownerAnalysisService _ownerAnalysisService;
        public OwnerAnalysisController(IownerAnalysisService ownerAnalysisService)
        {
           _ownerAnalysisService = ownerAnalysisService;
        }

        [HttpGet("Total/Customer")]
        public async Task<ActionResult<ApiResponse>> getOwnerTotalCustomer(int ownerid)
        {

            int count = await _ownerAnalysisService.getOwnerTotalCustomer(ownerid);
            return Ok(new ApiResponse(200, true,count));
        }

        [HttpGet("Top/product")]
        public async Task<ActionResult<ApiResponse>> getTopProduct(int ownerid)
        {

            return Ok(new ApiResponse(200, true,await _ownerAnalysisService.getTop5OwnerProduct(ownerid)));
        }
        [HttpGet("Top/offer")]
        public async Task<ActionResult<ApiResponse>> getTopoffer(int ownerid)
        {

            return Ok(new ApiResponse(200, true, await _ownerAnalysisService.getTop5OwnerOffer(ownerid)));
        }

        [HttpGet("Total/orders")]
        public async Task<ActionResult<ApiResponse>> getOwnerTotalOrders(int ownerid)
        {

            return Ok(new ApiResponse(200, true, await _ownerAnalysisService.getOwnerTotalOrders(ownerid)));
        }

        [HttpGet("Total/profits")]
        public async Task<ActionResult<ApiResponse>> getTotalprofits(int ownerid)
        {

            return Ok(new ApiResponse(200, true, await _ownerAnalysisService.getTotalProfit(ownerid)));
        }

        [HttpGet("Count/Products")]
        public async Task<ActionResult<ApiResponse>> getProductCount(int ownerid)
        {

            return Ok(new ApiResponse(200, true, await _ownerAnalysisService.getProductsCount(ownerid)));
        }

        [HttpGet("Count/Offers")]
        public async Task<ActionResult<ApiResponse>> getOffersCount(int ownerid)
        {

            return Ok(new ApiResponse(200, true, await _ownerAnalysisService.getOffersCount(ownerid)));
        }


        [HttpGet("Count/orders/status")]
        public async Task<ActionResult<ApiResponse>> getorderstatus(int ownerid)
        {

            return Ok(new ApiResponse(200, true, await _ownerAnalysisService.getDiffernentOrdersStatus(ownerid)));
        }

        [HttpGet("Count/OffersOrders/Productorders")]
        public async Task<ActionResult<ApiResponse>> countProductOffersOrders(int ownerid)
        {

            return Ok(new ApiResponse(200, true, await _ownerAnalysisService.ownerOrdersOffersProductCount(ownerid)));
        }

        [HttpGet("Top/Customers")]
        public async Task<ActionResult<ApiResponse>> getTopCustomers(int ownerid)
        {

            return Ok(new ApiResponse(200, true, await _ownerAnalysisService.getTopCustomerInfo(ownerid)));
        }




    }
}
