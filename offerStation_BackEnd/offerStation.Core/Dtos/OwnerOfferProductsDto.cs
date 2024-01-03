using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace offerStation.Core.Dtos
{
    public class OwnerOfferProductsDto
    {
        
        public int Quantity { get; set; }
        public double price { get; set; }
        public byte[]? ProductImage { get; set; }
        public string ProductName { get; set; } 
        public string ProductDescription { get; set; }

     

    }
}
