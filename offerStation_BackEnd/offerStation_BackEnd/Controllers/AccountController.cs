using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using offerStation.Core.Dtos;
using offerStation.Core.Interfaces.Services;

namespace offerStation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        private readonly RoleManager<IdentityRole> RoleManager;

        public AccountController(IAccountService accountService, RoleManager<IdentityRole> roleManager)
        {
            this._accountService = accountService;
            RoleManager = roleManager;
        }

        [HttpPost("login")]
        public async Task<ActionResult<ApiResponse>> Login(UserLoginDto dto)
        {
            if (!ModelState.IsValid) { return BadRequest(new ApiResponse(400, false, ModelState)); }

            return Ok(await _accountService.LoginUser(dto));
        }

        [HttpPost("Customer/register")]
        public async Task<ActionResult<ApiResponse>> CustomerRegister(CustomerRegestrationDto dto)
        {
            if (!ModelState.IsValid) { return BadRequest(new ApiResponse(400, false, ModelState)); }

            return Ok(await _accountService.CustomerRegister(dto));
        }

        [HttpPost("Owner/register")]
        public async Task<ActionResult<ApiResponse>> OwnerRegister(OwnerRegestrationDto dto)
        {
            if (!ModelState.IsValid) { return BadRequest(new ApiResponse(400, false, ModelState)); }

            return Ok(await _accountService.OwnerRegister(dto));
        }
        [HttpPost("Supplier/register")]
        public async Task<ActionResult<ApiResponse>> SupplierRegister(SupplierRegestrationDto dto)
        {
            if (!ModelState.IsValid) { return BadRequest(new ApiResponse(400, false, ModelState)); }

            return Ok(await _accountService.SupplierRegister(dto));
        }

        [HttpPost("createRole")]
        public async Task<ActionResult<ApiResponse>> CreateRole(string Role)
        {

            var roleExists = await RoleManager.RoleExistsAsync(Role);
            if (!roleExists)
            {
                var role = new IdentityRole(Role);
                var result = await RoleManager.CreateAsync(role);
                return new ApiResponse(200, true, null, "Created");
            }

            return BadRequest(new ApiResponse(400, false, ModelState));
        }

        //[HttpPost("Patient/register")]
        //public async Task<ActionResult<ApiResponse>> PatientRegister(UserRegisterDto dto)
        //{
        //    if (!ModelState.IsValid) { return BadRequest(new ApiResponse(400, false, ModelState)); };

        //    return Ok(await _accountService.RegisterPatient(dto));
        //}
    }
}
