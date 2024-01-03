using offerStation.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace offerStation.Core.Dtos
{
    public class OwnerCartDto
    {
        public int SupplierId { get; set; }
        public string SupplierName { get; set; }
        public virtual List<OwnerCartProductDto> Products { get; set; }
        public virtual List<OwnerCartOfferDto> Offers { get; set; }
        public double Total { get; set; }

    }
}
