using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace offerStation.Core.Models
{
    public class CustomerOrderOffer : BaseModel
    {
        public int Id { get; set; }
        [ForeignKey("Order")]
        public int OrderId { get; set; }
        public virtual CustomerOrder Order { get; set; }

        [ForeignKey("Offer")]
        public int OwnerOffertId { get; set; }
        public virtual OwnerOffer Offer { get; set; }

        public int Quantity { get; set; }
    }
}
