using offerStation.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace offerStation.Core.Interfaces.Services
{
    public interface IAccountService
    {
        Task<ApiResponse> LoginUser(UserLoginDto dto);
        Task<ApiResponse> CustomerRegister(CustomerRegestrationDto cusDto);
        Task<ApiResponse> OwnerRegister(OwnerRegestrationDto dto);
        Task<ApiResponse> SupplierRegister(SupplierRegestrationDto dto);
    }
}
