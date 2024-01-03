using offerStation.Core.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace offerStation.Core.Dtos
{
    public class OwnerCartProductDto
    {

        public int SupplierProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public double Total { get; set; }
    }
}