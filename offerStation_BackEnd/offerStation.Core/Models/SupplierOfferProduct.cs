using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace offerStation.Core.Models
{
    public class SupplierOfferProduct : BaseModel
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public virtual SupplierProduct Product { get; set; }

        [ForeignKey("Offer")]
        public int OfferId { get; set; }
        public virtual SupplierOffer Offer { get; set; }
    }
}
