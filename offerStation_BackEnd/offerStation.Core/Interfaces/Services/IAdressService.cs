using offerStation.Core.Dtos;
using offerStation.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace offerStation.Core.Interfaces.Services
{
    public interface IAdressService
    {
        Task<AddressCityNameDto?> GetAddressDetailsById(int id);
        Task<List<AddressCityNameDto>?> GetAllAddresses(string id);
        Task<bool> AddAddress(string userId, AddressDTO addressDTO);
        Task<bool> EditAddress(int id, AddressDTO addressDTO);
        Task<bool> DeleteAddress(int id);
        Task<List<CityDto>> GetAllCities();
    }
}
