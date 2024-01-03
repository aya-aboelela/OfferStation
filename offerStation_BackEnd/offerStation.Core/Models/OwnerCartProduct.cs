using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace offerStation.Core.Models
{
    public class OwnerCartProduct : BaseModel
    {
        public int Id { get; set; }

        [ForeignKey("Cart")]
        public int CartId { get; set; }
        public OwnerCart Cart { get; set; }
        
        [ForeignKey("SupplierProduct")]
        public int SupplierProductId { get; set; }
        public virtual SupplierProduct SupplierProduct { get; set; }

        public int Quantity { get; set; }
        public double Price { get; set; }
        public double Total { get; set; }
    }
}
