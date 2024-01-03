using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace offerStation.Core.Models
{
    public class CustomerOrderDelivery : BaseModel
    {
        public int Id { get; set; }
        public int CustomerOrderId { get; set; }
        public virtual CustomerOrder CustomerOrder { get; set; }
        public int DeliveryId { get; set; }
        public virtual Delivery Delivery { get; set; }
    }
}
