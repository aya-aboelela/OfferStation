using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using offerStation.Core.Dtos;
using offerStation.Core.Interfaces.Services;
using offerStation.Core.Models;
using offerStation.EF;
using System.Reflection.PortableExecutable;
using System.Security.Claims;

namespace offerStation.API.Controllers
{

    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICustomerCartService _CustomerCartService;
        private readonly IOwnerCartService _OwnercartService;
        public CartController(ICustomerCartService CustomercartService, IOwnerCartService OwnercartService)
        {
            _CustomerCartService = CustomercartService;
            _OwnercartService = OwnercartService;
        }

        [HttpGet("GetCartDetails")]
        public async Task<ActionResult<ApiResponse>> GetCartDetails()
        {

            if (!ModelState.IsValid) { return BadRequest(new ApiResponse(400, false, ModelState)); }

            var useridentifier = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;

            return Ok(await _CustomerCartService.GetCartDetails(int.Parse(useridentifier)));
        }

        [HttpPost("addProductToCart")]
        public async Task<ActionResult<ApiResponse>> AddProductToCart(ProductDetailsDto Product)
        {

            if (!ModelState.IsValid) { return BadRequest(new ApiResponse(400, false, ModelState)); }

            var useridentifier = User.Claims.FirstOrDefault(c=>c.Type == ClaimTypes.NameIdentifier).Value;

            return Ok(await _CustomerCartService.AddProductToCart( int.Parse(useridentifier), Product));
        }

        [HttpPost("addOfferToCart")]
        public async Task<ActionResult<ApiResponse>> AddOfferToCart(ProductDetailsDto Product)
        {
            if (!ModelState.IsValid) { return BadRequest(new ApiResponse(400, false, ModelState)); }

            var useridentifier = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;

            return Ok(await _CustomerCartService.AddOfferToCart(int.Parse(useridentifier), Product));

        }

        [HttpPost("removeProductToCart")]
        public async Task<ActionResult<ApiResponse>> RemoveProductToCart(int ProductId)
        {

            if (!ModelState.IsValid) { return BadRequest(new ApiResponse(400, false, ModelState)); }

            var useridentifier = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;

            return Ok(await _CustomerCartService.RemoveProductFromCart(int.Parse(useridentifier), ProductId));
        }

        [HttpPost("removeOfferToCart")]
        public async Task<ActionResult<ApiResponse>> RemoveOfferToCart(int OfferId)
        {
            if (!ModelState.IsValid) { return BadRequest(new ApiResponse(400, false, ModelState)); }

            var useridentifier = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;

            return Ok(await _CustomerCartService.RemoveOfferFromCart(int.Parse(useridentifier), OfferId));
        }




        [HttpPost("productPlus")]
        public async Task<ActionResult<ApiResponse>> ProductPlus(int OfferId)
        {
            if (!ModelState.IsValid) { return BadRequest(new ApiResponse(400, false, ModelState)); }

            var useridentifier = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;

            return Ok(await _CustomerCartService.ProductPlus(int.Parse(useridentifier), OfferId));
        }

        [HttpPost("offerPlus")]
        public async Task<ActionResult<ApiResponse>> OfferPlus(int OfferId)
        {
            if (!ModelState.IsValid) { return BadRequest(new ApiResponse(400, false, ModelState)); }

            var useridentifier = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;

            return Ok(await _CustomerCartService.OfferPlus(int.Parse(useridentifier), OfferId));
        }

        [HttpPost("productMinus")]
        public async Task<ActionResult<ApiResponse>> ProductMinus(int OfferId)
        {
            if (!ModelState.IsValid) { return BadRequest(new ApiResponse(400, false, ModelState)); }

            var useridentifier = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;

            return Ok(await _CustomerCartService.ProductMinus(int.Parse(useridentifier), OfferId));
        }

        [HttpPost("offerMinus")]
        public async Task<ActionResult<ApiResponse>> OfferMinus(int OfferId)
        {
            if (!ModelState.IsValid) { return BadRequest(new ApiResponse(400, false, ModelState)); }

            var useridentifier = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;

            return Ok(await _CustomerCartService.OfferMinus(int.Parse(useridentifier), OfferId));
        }








        [HttpGet("getCreateOrder")]
        public async Task<ActionResult<ApiResponse>> GetCreateOrder()
        {
            if (!ModelState.IsValid) { return BadRequest(new ApiResponse(400, false, ModelState)); }

            var useridentifier = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;

            return Ok(await _CustomerCartService.GetCreateOrder(int.Parse(useridentifier)));
        }

        [HttpPost("postCreateOrder")]
        public async Task<ActionResult<ApiResponse>> PostCreateOrder()
        {
            if (!ModelState.IsValid) { return BadRequest(new ApiResponse(400, false, ModelState)); }

            var useridentifier = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;

            return Ok(await _CustomerCartService.PostCreateOrder(int.Parse(useridentifier)));
        }





        [HttpGet("GetOwnerCartDetails")]
        public async Task<ActionResult<ApiResponse>> GetOwnerCartDetails()
        {

            if (!ModelState.IsValid) { return BadRequest(new ApiResponse(400, false, ModelState)); }

            var useridentifier = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;

            return Ok(await _OwnercartService.GetCartDetails(int.Parse(useridentifier)));
        }

        [HttpPost("addOwnerProductToCart")]
        public async Task<ActionResult<ApiResponse>> AddOwnerProductToCart(ProductDetailsDto Product)
        {

            if (!ModelState.IsValid) { return BadRequest(new ApiResponse(400, false, ModelState)); }

            var useridentifier = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;

            return Ok(await _OwnercartService.AddProductToCart(int.Parse(useridentifier), Product));
        }


        [HttpPost("addOwnerOfferToCart")]
        public async Task<ActionResult<ApiResponse>> AddOwnerOfferToCart(ProductDetailsDto Product)
        {
            if (!ModelState.IsValid) { return BadRequest(new ApiResponse(400, false, ModelState)); }

            var useridentifier = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;

            return Ok(await _OwnercartService.AddOfferToCart(int.Parse(useridentifier), Product));

        }

        [HttpPost("removeOwnerProductToCart")]
        public async Task<ActionResult<ApiResponse>> RemoveOwnerProductToCart(int ProductId)
        {

            if (!ModelState.IsValid) { return BadRequest(new ApiResponse(400, false, ModelState)); }

            var useridentifier = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;

            return Ok(await _OwnercartService.RemoveProductFromCart(int.Parse(useridentifier), ProductId));
        }

        [HttpPost("removeOwnerOfferToCart")]
        public async Task<ActionResult<ApiResponse>> RemoveOwnerOfferToCart(int OfferId)
        {
            if (!ModelState.IsValid) { return BadRequest(new ApiResponse(400, false, ModelState)); }

            var useridentifier = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;

            return Ok(await _OwnercartService.RemoveOfferFromCart(int.Parse(useridentifier), OfferId));
        }




        [HttpPost("ownerProductPlus")]
        public async Task<ActionResult<ApiResponse>> OwnerProductPlus(int OfferId)
        {
            if (!ModelState.IsValid) { return BadRequest(new ApiResponse(400, false, ModelState)); }

            var useridentifier = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;

            return Ok(await _CustomerCartService.ProductPlus(int.Parse(useridentifier), OfferId));
        }

        [HttpPost("ownerofferPlus")]
        public async Task<ActionResult<ApiResponse>> OwnerOfferPlus(int OfferId)
        {
            if (!ModelState.IsValid) { return BadRequest(new ApiResponse(400, false, ModelState)); }

            var useridentifier = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;

            return Ok(await _CustomerCartService.OfferPlus(int.Parse(useridentifier), OfferId));
        }

        [HttpPost("ownerproductMinus")]
        public async Task<ActionResult<ApiResponse>> OwnerProductMinus(int OfferId)
        {
            if (!ModelState.IsValid) { return BadRequest(new ApiResponse(400, false, ModelState)); }

            var useridentifier = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;

            return Ok(await _CustomerCartService.ProductMinus(int.Parse(useridentifier), OfferId));
        }

        [HttpPost("ownerofferMinus")]
        public async Task<ActionResult<ApiResponse>> OwnerOfferMinus(int OfferId)
        {
            if (!ModelState.IsValid) { return BadRequest(new ApiResponse(400, false, ModelState)); }

            var useridentifier = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;

            return Ok(await _CustomerCartService.OfferMinus(int.Parse(useridentifier), OfferId));
        }





        [HttpGet("getOwnerCreateOrder")]
        public async Task<ActionResult<ApiResponse>> GetOwnerCreateOrder()
        {
            if (!ModelState.IsValid) { return BadRequest(new ApiResponse(400, false, ModelState)); }

            var useridentifier = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;

            return Ok(await _OwnercartService.GetCreateOrder(int.Parse(useridentifier)));
        }

        [HttpPost("postOwnerCreateOrder")]
        public async Task<ActionResult<ApiResponse>> PostOwnerCreateOrder()
        {
            if (!ModelState.IsValid) { return BadRequest(new ApiResponse(400, false, ModelState)); }

            var useridentifier = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;

            return Ok(await _OwnercartService.PostCreateOrder(int.Parse(useridentifier)));
        }
    }
}
