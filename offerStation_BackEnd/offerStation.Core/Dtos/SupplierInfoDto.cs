using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace offerStation.Core.Dtos
{
    public class SupplierInfoDto
    {
        public string Name { get; set; }
        public byte[]? Image { get; set; }
        public string PhoneNumber { get; set; }
        public int Rating { get; set; }
        public string Email { get; set; }
    }
}
