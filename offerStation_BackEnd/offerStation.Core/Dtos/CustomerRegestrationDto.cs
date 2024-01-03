using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace offerStation.Core.Dtos
{
    public class CustomerRegestrationDto
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public List<AddressDTO> Address { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

    }
}
