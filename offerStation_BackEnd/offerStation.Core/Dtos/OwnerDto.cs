using offerStation.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace offerStation.Core.Dtos
{
    public class OwnerDto
    {
        public int Id { get; set; }
        public byte[]? Image { get; set; }
        public string Name { get; set; }
        public int Rating { get; set; }
        public virtual List<Address>? Addresses { get; set; }


    }
}
