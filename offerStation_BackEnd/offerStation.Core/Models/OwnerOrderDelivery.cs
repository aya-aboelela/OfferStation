using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace offerStation.Core.Models
{
    public class OwnerOrderDelivery : BaseModel
    {
        public int Id { get; set; }
        public int OwnerOrderId { get; set; }
        public virtual OwnerOrder OwnerOrder { get; set; }
        public int DeliveryId { get; set; }
        public virtual Delivery Delivery { get; set; }
    }
}
