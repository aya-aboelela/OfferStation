using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace offerStation.Core.Dtos
{
    public class AddressDTO
    {
        public string details { get; set; }
        public int CityId { get; set; }
   

    }
    public class AddressCityNameDto: AddressDTO
    {
        public int Id { get; set; }
        public string CityName { get; set; }
    }
}
