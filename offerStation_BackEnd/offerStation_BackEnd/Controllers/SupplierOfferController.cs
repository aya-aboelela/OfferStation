using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using offerStation.Core.Dtos;
using offerStation.Core.Interfaces.Services;
using offerStation.EF.Services;

namespace offerStation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierOfferController : ControllerBase
    {
        private readonly ISupplierOfferService _supplierOfferService;
        public SupplierOfferController(ISupplierOfferService supplierOfferService)
        {
            _supplierOfferService = supplierOfferService;
        }
        [HttpGet("id")]
        public async Task<ActionResult<ApiResponse>> GetOfferDetails(int id)
        {
            OfferDetailsDto offer = await _supplierOfferService.GetOfferDetails(id);
            if (offer is null)
            {
                return BadRequest(new ApiResponse(404, false, "null object"));
            }
            return Ok(new ApiResponse(200, true, offer));
        }
        [HttpGet("all/id")]
        public async Task<ActionResult<ApiResponse>> AllOffersBySupplierId(int supplierId)
        {
            List<OfferDetailsDto> offerList = await _supplierOfferService.GetAllOffersBySupplierId(supplierId);
            if (offerList is null)
            {
                return BadRequest(new ApiResponse(404, false, "null object"));
            }
            return Ok(new ApiResponse(200, true, offerList));
        }
        [HttpPost("Offer/id")]
        public async Task<ActionResult<ApiResponse>> AddOffer(int supplierId, OfferInfoDto Offer)
        {
            bool success = await _supplierOfferService.AddOffer(supplierId, Offer);
            if (success)
            {
                return Ok(new ApiResponse(201, true, success));
            }
            return BadRequest(new ApiResponse(500, false, "server error"));
        }
        [HttpPut("Offer/id")]
        public async Task<ActionResult<ApiResponse>> EditOffer(int id, OfferInfoDto Offer)
        {
            bool success = await _supplierOfferService.EditOffer(id, Offer);
            if (success)
            {
                return Ok(new ApiResponse(201, true, success));
            }
            return BadRequest(new ApiResponse(500, false, "server error"));
        }
        [HttpDelete("Offer/id")]
        public async Task<ActionResult<ApiResponse>> DeleteOffer(int id)
        {
            bool success = await _supplierOfferService.DeleteOffer(id);
            if (success)
            {
                return Ok(new ApiResponse(200, true, success));
            }
            return BadRequest(new ApiResponse(500, false, "server error"));
        }
    }
}
