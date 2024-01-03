using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using offerStation.Core.Dtos;
using offerStation.Core.Interfaces.Services;
using System.Runtime.CompilerServices;

namespace offerStation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAdressService _addressService;

        public AddressController(IAdressService addressService)
        {
            this._addressService = addressService;
        }
        [HttpGet("id")]
        public async Task<ActionResult<ApiResponse>> GetAddress(int id)
        {
            AddressCityNameDto address = await _addressService.GetAddressDetailsById(id);
            if(address is null)
            {
                return BadRequest(new ApiResponse(404, false, "null object"));
            }
            return Ok(new ApiResponse(200, true, address));
        }

        [HttpGet("all/id")]
        public async Task<ActionResult<ApiResponse>> GetAllAddresses(string userId)
        {
            List<AddressCityNameDto> addresses = await _addressService.GetAllAddresses(userId);
            if(addresses is null)
            {
                return BadRequest(new ApiResponse(404, false, "null object"));
            }
            return Ok(new ApiResponse(200, true, addresses));
        }

        [HttpPost("id")]
        public async Task<ActionResult<ApiResponse>> AddAddress(string userId, AddressDTO address)
        {
            bool success = await _addressService.AddAddress(userId, address);
            if (success)
            {
                return new ApiResponse(201, true, success);
            }
            return new ApiResponse(500, false, success);
        } 

        [HttpPut("id")]
        public async Task<ActionResult<ApiResponse>> EditAddress(int id, AddressDTO address)
        {
            bool success = await _addressService.EditAddress(id, address);
            if (success)
            {
                return new ApiResponse(200, true, success);
            }
            return new ApiResponse(500, false, "server error");
        }
        [HttpDelete("id")]
        public async Task<ActionResult<ApiResponse>> DeleteAddress(int id)
        {
            bool success = await _addressService.DeleteAddress(id);
            if (success)
            {
                return new ApiResponse(200, true, success);
            }
            return new ApiResponse(500, false, "server error");
        }
        [HttpGet("cities")]
        public async Task<IActionResult> getAllCities()
        {
            return Ok(new ApiResponse(200, true, await _addressService.GetAllCities()));
        }

        //[HttpGet("Addresses")]
        //public async Task<ActionResult> GetAddresses()
        //{
        //    return Ok(await _addressService.GetAddresses());
        //}

    }
}
