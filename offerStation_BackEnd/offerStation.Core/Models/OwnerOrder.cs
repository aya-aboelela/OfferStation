using offerStation.Core.Constants;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace offerStation.Core.Models
{
    public class OwnerOrder : BaseModel
    {
        public int Id { get; set; }
        public OrderStatus orderStatus { get; set; }
        public DateTime orderDate { get; set; }

        public OwnerOrderDelivery Delivery { get; set; }

        [ForeignKey("Supplier")]
        public int SupplierId { get; set; }
        public  Supplier Supplier { get; set; }

        
        [ForeignKey("Owner")]
        public int OwnerId { get; set; }
        public  Owner Owner { get; set; }
        
        public PaymentMethods PaymentMethod { get; set; }
        public OwnerCardDetails OwnerCardDetails { get; set; }

        public  List<OwnerOrderProduct> Products { get; set; }
        public  List<OwnerOrderOffer> Offers { get; set; }
        public double Total { get; set; }
    }
}
