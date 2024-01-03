using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using offerStation.Core.Dtos;
using offerStation.Core.Interfaces.Services;
using offerStation.EF.Services;

namespace offerStation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OwnerOfferController : ControllerBase
    {
        private readonly IOwnerOfferService _ownerOfferService;
        private object  _ownerService;

        public OwnerOfferController(IOwnerOfferService ownerOfferService)
        {
            _ownerOfferService = ownerOfferService;
        }
        [HttpGet("id")]
        public async Task<ActionResult<ApiResponse>> GetOfferDetails(int id)
        {
            OfferDetailsDto offer = await _ownerOfferService.GetOfferDetails(id);
            if(offer is null)
            {
                return BadRequest(new ApiResponse(404, false, "null object"));
            }
            return Ok(new ApiResponse(200, true, offer));
        }
        [HttpGet("all/id")]
        public async Task<ActionResult<ApiResponse>> AllOffersByOwnerId(int ownerId)
        {
            List<OfferDetailsDto> offerList = await _ownerOfferService.GetAllOffersByOwnerId(ownerId);
            if (offerList is null)
            {
                return BadRequest(new ApiResponse(404, false, "null object"));
            }
            return Ok(new ApiResponse(200, true, offerList));
        }
        [HttpPost("Offer/id")] 
        public async Task<ActionResult<ApiResponse>> AddOffer(int ownerId, OfferInfoDto Offer)
        {
            bool success = await _ownerOfferService.AddOffer(ownerId, Offer);
            if (success)
            {
                return Ok(new ApiResponse(201, true, success));
            }
            return BadRequest(new ApiResponse(500, false, "server error"));
        }
        [HttpPut("Offer/id")]
        public async Task<ActionResult<ApiResponse>> EditOffer(int id, OfferInfoDto Offer)
        {
            bool success = await _ownerOfferService.EditOffer(id, Offer);
            if (success)
            {
                return Ok(new ApiResponse(201, true, success));
            }
            return BadRequest(new ApiResponse(500, false, "server error"));
        }
        [HttpDelete("Offer/id")]
        public async Task<ActionResult<ApiResponse>> DeleteOffer(int id)
        {
            bool success = await _ownerOfferService.DeleteOffer(id);
            if (success)
            {
                return Ok(new ApiResponse(200, true, success));
            }
            return BadRequest(new ApiResponse(500, false, "server error"));
        }


    }

}
