using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace offerStation.Core.Models
{
    public class SupplierMenuCategory : BaseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte[]? Image { get; set; }

        [ForeignKey("Supplier")]
        public int SupplierId { get; set; }
        public virtual Supplier Supplier { get; set; }
        public virtual List<SupplierProduct> Products { get; set; }
    }   
}
