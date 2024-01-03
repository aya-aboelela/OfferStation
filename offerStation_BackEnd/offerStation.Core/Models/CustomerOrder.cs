using offerStation.Core.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace offerStation.Core.Models
{
    public class CustomerOrder : BaseModel
    {
        public int Id { get; set; }
        public DateTime orderDate { get; set; }
        public OrderStatus  orderStatus { get; set; }
        public CustomerOrderDelivery Delivery { get; set; }

        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

        [ForeignKey("Owner")]
        public int OwnerId { get; set; }
        public virtual Owner Owner { get; set; }

        public PaymentMethods PaymentMethod { get; set; }

        public virtual CustomerCardDetails? CardDetails { get; set; }

        public virtual List<CustomerOrderProduct> Products { get; set; }
        public virtual List<CustomerOrderOffer> Offers { get; set; }
        public double Total { get; set; }
    }
}
