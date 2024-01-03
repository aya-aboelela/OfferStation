using offerStation.Core.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace offerStation.Core.Dtos
{
    public class OwnerCartOfferDto
    {
        public int SupplierOffertId { get; set; }
        public string OfferName { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public int Total { get; set; }
    }
}