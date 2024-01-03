using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using offerStation.API.Payment;
using offerStation.Core.Dtos;
using offerStation.Core.Interfaces.Services;
using offerStation.EF;

namespace offerStation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaypalService _PaypalService;

        public PaymentController(IPaypalService PaypalService)
        {
            _PaypalService = PaypalService;
        }

        [HttpPost("pay")]
        public ActionResult<ApiResponse> Login(PaymentDto dto)
        {
            if (!ModelState.IsValid) { return BadRequest(new ApiResponse(400, false, ModelState)); }

            return Ok( _PaypalService.PaymentWithPaypal(dto));
        }


    }
}
