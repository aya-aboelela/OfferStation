using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using offerStation.Core.Dtos;
using offerStation.Core.Interfaces.Services;
using offerStation.EF.Services;

namespace offerStation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierMenuCategoryController : ControllerBase
    {
        private readonly ISupplierMenuCategoryService _supplierMenuCategoryService;
        public SupplierMenuCategoryController(ISupplierMenuCategoryService supplierMenuCategoryService)
        {
            _supplierMenuCategoryService = supplierMenuCategoryService;
        }
        [HttpGet("id")]
        public async Task<ActionResult<ApiResponse>> MenuCategoryDetails(int id)
        {
            MenuCategoryDetailsDto MenuCategory = await _supplierMenuCategoryService.GetMenuCategoryDetails(id);
            if (MenuCategory is null)
            {
                return BadRequest(new ApiResponse(404, false, "null object"));
            }
            return Ok(new ApiResponse(200, true, MenuCategory));
        }
        [HttpPost("id")]
        public async Task<ActionResult<ApiResponse>> AddMenuCategory(int ownerId, MenuCategoryDto menuCategory)
        {
            bool success = await _supplierMenuCategoryService.AddMenuCategory(ownerId, menuCategory);
            if (success)
            {
                return Ok(new ApiResponse(201, true, success));
            }
            return BadRequest(new ApiResponse(500, false, "server error"));
        }
        [HttpPut("id")]
        public async Task<ActionResult<ApiResponse>> EditMenuCategory(int id, MenuCategoryDto menuCategory)
        {
            bool success = await _supplierMenuCategoryService.EditMenuCategory(id, menuCategory);
            if (success)
            {
                return Ok(new ApiResponse(200, true, success));
            }
            return BadRequest(new ApiResponse(500, false, "server error"));
        }
        [HttpDelete("id")]
        public async Task<ActionResult<ApiResponse>> DeleteMenuCategory(int id)
        {
            bool success = await _supplierMenuCategoryService.DeleteMenuCategory(id);
            if (success)
            {
                return Ok(new ApiResponse(200, true, success));
            }
            return BadRequest(new ApiResponse(500, false, "server error"));
        }
    }
}
