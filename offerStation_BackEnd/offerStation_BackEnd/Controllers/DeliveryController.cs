using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using offerStation.Core.Dtos;
using offerStation.Core.Interfaces.Services;
using offerStation.EF.Services;

namespace offerStation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeliveryController : ControllerBase
    {
        private readonly IDeliveryService _deliveryService;
        public DeliveryController(IDeliveryService deliveryService)
        {
            _deliveryService = deliveryService;
        }
        [HttpPost]
        public async Task<ActionResult<ApiResponse>> Delivery(DeliveryDto delivery)
        {
            bool success = await _deliveryService.AddDelivery(delivery);
            if(success)
            {
                return Ok(new ApiResponse(201, true, success));
            }
            return BadRequest(new ApiResponse(500, false, "server error"));
        }
        [HttpPut("id")]
        public async Task<ActionResult<ApiResponse>> Delivery(int Id, DeliveryDto delivery)
        {
            bool success = await _deliveryService.EditDelivery(Id, delivery);
            if (success)
            {
                return Ok(new ApiResponse(201, true, success));
            }
            return BadRequest(new ApiResponse(500, false, "server error"));
        }
        [HttpDelete("id")]
        public async Task<ActionResult<ApiResponse>> Delivery(int id)
        {
            bool success = await _deliveryService.DeleteDelivery(id);
            if (success)
            {
                return Ok(new ApiResponse(200, true, success));
            }
            return BadRequest(new ApiResponse(500, false, "server error"));
        }
    }
}
