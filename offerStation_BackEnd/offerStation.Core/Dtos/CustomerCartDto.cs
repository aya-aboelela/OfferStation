using offerStation.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace offerStation.Core.Dtos
{
    public class CustomerCartDto
    {
        public int OwnerId { get; set; }
        public string OwnerName { get; set; }
        public virtual List<CustomerCartProductDto> Products { get; set; }
        public virtual List<CustomerCartOfferDto> Offers { get; set; }
        public double Total { get; set; }

    }
}
