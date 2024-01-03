using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using offerStation.Core.Dtos;
using offerStation.Core.Interfaces.Services;
using offerStation.EF.Services;

namespace offerStation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminAnalysisController : ControllerBase
    {
        
            private readonly IadminAnalysisService _adminAnalysisService;
            public AdminAnalysisController(IadminAnalysisService adminAnalysisService)
            {
                _adminAnalysisService = adminAnalysisService;
            }

            [HttpGet("Total/Customer")]
            public async Task<ActionResult<ApiResponse>> getTotalCustomer()
            {

                return Ok(new ApiResponse(200, true, await _adminAnalysisService.getTotalCustomer()));

            }
            [HttpGet("Total/owners")]
            public async Task<ActionResult<ApiResponse>> getTotalOwners()
            {

                return Ok(new ApiResponse(200, true, await _adminAnalysisService.getTotalOwners()));
            }

            [HttpGet("Total/Supplier")]
            public async Task<ActionResult<ApiResponse>> getTotalSipplier()
            {

                return Ok(new ApiResponse(200, true, await _adminAnalysisService.getTotalSupplier()));
            }

            //[HttpGet("Top/product")]
            //public async Task<ActionResult<ApiResponse>> getTopProduct(int ownerid)
            //{

            //    return Ok(new ApiResponse(200, true, await _adminAnalysisService.getTop5OwnerProduct(ownerid)));
            //}
            //[HttpGet("Top/offer")]
            //public async Task<ActionResult<ApiResponse>> getTopoffer(int ownerid)
            //{

            //    return Ok(new ApiResponse(200, true, await _adminAnalysisService.getTop5OwnerOffer(ownerid)));
            //}

            //[HttpGet("Total/orders")]
            //public async Task<ActionResult<ApiResponse>> getOwnerTotalOrders(int ownerid)
            //{

            //    return Ok(new ApiResponse(200, true, await _adminAnalysisService.getOwnerTotalOrders(ownerid)));
            //}

            [HttpGet("Total/profits")]
            public async Task<ActionResult<ApiResponse>> getTotalprofits()
            {

                return Ok(new ApiResponse(200, true, await _adminAnalysisService.getTotalProfit()));
            }

            [HttpGet("Count/Products")]
            public async Task<ActionResult<ApiResponse>> getProductCount()
            {

                return Ok(new ApiResponse(200, true, await _adminAnalysisService.getTotalProducts()));
            }

            [HttpGet("Count/Offers")]
            public async Task<ActionResult<ApiResponse>> getOffersCount()
            {

                return Ok(new ApiResponse(200, true, await _adminAnalysisService.getTotaloffers()));
            }

            [HttpGet("Count/orders")]
            public async Task<ActionResult<ApiResponse>> getTotalOrders()
            {

                return Ok(new ApiResponse(200, true, await _adminAnalysisService.getTotalOrders()));
            }

        //[HttpGet("Count/orders/status")]
        //public async Task<ActionResult<ApiResponse>> getorderstatus(int ownerid)
        //{

        //    return Ok(new ApiResponse(200, true, await _adminAnalysisService.getDiffernentOrdersStatus(ownerid)));
        //}

        [HttpGet("Count/OffersOrders/owner/supplier")]
        public async Task<ActionResult<ApiResponse>> countOffersOrders()
        {

            return Ok(new ApiResponse(200, true, await _adminAnalysisService.getownerSupplierOffersCount()));
        }

        [HttpGet("Count/ProductOrders/owner/supplier")]
        public async Task<ActionResult<ApiResponse>> countProductOrders()
        {

            return Ok(new ApiResponse(200, true, await _adminAnalysisService.getownerSupplierProductCount()));
        }
        [HttpGet("Total/Ordered/Customers")]
            public async Task<ActionResult<ApiResponse>> getOrderdCustomer()
            {

                return Ok(new ApiResponse(200, true, await _adminAnalysisService.getOrderdCustomer()));
            }
        [HttpGet("Total/Ordered/Owners")]
        public async Task<ActionResult<ApiResponse>> getOrderdOwner()
        {

            return Ok(new ApiResponse(200, true, await _adminAnalysisService.getOrderdOwner()));
        }

    }
       

}
    

