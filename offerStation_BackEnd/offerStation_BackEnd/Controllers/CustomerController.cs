using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using offerStation.Core.Dtos;
using offerStation.Core.Interfaces.Services;
using offerStation.Core.Models;
using offerStation.EF.Services;
using System.Numerics;

namespace offerStation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet("id")]
        public async Task<ActionResult<ApiResponse>> GetCustomer(int id)
        {
            CustomerInfoDto customer = await _customerService.GetCustomer(id);
            
            if (customer is null)
            {
                return BadRequest(new ApiResponse(404, false, "null object"));
            }

            return Ok(new ApiResponse(200, true, customer));
        }

        [HttpPut("id")]
        public async Task<ActionResult<ApiResponse>> EditCustomer(int id, CustomerInfoDto customerDto)
        {
            bool success = await _customerService.EditCustomer(id, customerDto);
            if (success)
            {
                return Ok(new ApiResponse(200, true, success));
            }
            return BadRequest(new ApiResponse(500, false, "server error"));
        }
        [HttpDelete("id")]
        public async Task<ActionResult<ApiResponse>> SuspendCustomer(int id)
        {
            bool success = await _customerService.SuspendCustomer(id);
            if (success)
            {
                return Ok(new ApiResponse(200, true, success));
            }
            return BadRequest(new ApiResponse(500, false, success));
        }
        [HttpPut("RemoveCustomerSuspension/id")]
        public async Task<ActionResult<ApiResponse>> RemoveCustomerSuspension(int id)
        {
            bool success = await _customerService.RemoveCustomerSuspension(id);
            if (success)
            {
                return Ok(new ApiResponse(200, true, success));
            }
            return BadRequest(new ApiResponse(500, false, success));
        }
        [HttpPost("CustomerReview/id")]
        public async Task<ActionResult<ApiResponse>> AddReview(int CustomerId, int ownerId, ReviewInfoDto review)
        {
            bool success = await _customerService.AddReview(CustomerId, ownerId, review);
            if (success)
            {
                return Ok(new ApiResponse(201, true, success));
            }
            return BadRequest(new ApiResponse(500, false, success));
        }
        [HttpDelete("CustomerReview/id")]
        public async Task<ActionResult<ApiResponse>> DeleteCustomerReview(int id)
        {
            bool success = await _customerService.DeleteReview(id);
            if (success)
            {
                return Ok(new ApiResponse(200, true, success));
            }
            return BadRequest(new ApiResponse(500, false, success));
        }
        [HttpGet("AllCustomersReviews")]
        public async Task<ActionResult<ApiResponse>> GetAllCustomersReviews()
        {
            List<ReviewDto> reviews = await _customerService.GetAllCustomersReviews();
            if(reviews is null)
            {
                return BadRequest(new ApiResponse(404, false, "null object"));
            }
            return Ok(new ApiResponse(200, true, reviews));
        }
    }
}
