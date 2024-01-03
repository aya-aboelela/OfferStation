using offerStation.Core.Dtos;
using offerStation.Core.Interfaces.Services;
using offerStation.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace offerStation.EF.Services
{
    public class HelperService : IHelperService
    {
        public bool checkAddress(List<Address> addresses, int CityID)
        {
            Address address = addresses.FirstOrDefault(a => a.CityId == CityID);
            if (address != null)
            {
                return true;
            }
            return false;
        }

    }
}
