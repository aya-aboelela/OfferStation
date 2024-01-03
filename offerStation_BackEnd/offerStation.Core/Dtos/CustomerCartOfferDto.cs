using offerStation.Core.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace offerStation.Core.Dtos
{
    public class CustomerCartOfferDto
    {
        public int OwnerOffertId { get; set; }
        public string OfferName { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public int Total { get; set; }
    }
}