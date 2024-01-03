using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace offerStation.Core.Models
{
    public class CustomerOrderProduct : BaseModel
    {
        public int Id { get; set; }
        [ForeignKey("Order")]
        public int OrderId { get; set; }
        public virtual CustomerOrder Order { get; set; }

        [ForeignKey("OwnerProduct")]
        public int OwnerProductId { get; set; }
        public virtual OwnerProduct OwnerProduct { get; set; }

        public int Quantity { get; set; }
    }
}
