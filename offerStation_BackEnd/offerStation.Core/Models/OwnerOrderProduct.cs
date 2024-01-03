using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace offerStation.Core.Models
{
    public class OwnerOrderProduct : BaseModel
    {
        public int Id { get; set; }

        [ForeignKey("Order")]
        public int OrderId { get; set; }
        public virtual OwnerOrder Order { get; set; }

        [ForeignKey("SupplierProduct")]
        public int SupplierProductId { get; set; }
        public virtual SupplierProduct SupplierProduct { get; set; }
        
        public int Quantity { get; set; }
    }
}
