using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace offerStation.Core.Dtos
{
    public class ProductDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; } 
        public int Discount { get; set; }
        public byte[]? Image { get; set; }
        public int CategoryId { get; set; }
    }
    public class ProductDetailsDto
    {
        public int Id { get; set; }
        public double Price { get; set; }
    }
    public class offerProductInfo:ProductDto
    {
        public int Quantity { get; set; }


    }
    public class ProductInfoDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public double DiscountPrice { get; set; }
        public int Discount { get; set; }
        public byte[]? Image { get; set; }
    }
}
