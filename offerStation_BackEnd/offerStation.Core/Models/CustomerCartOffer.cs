using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace offerStation.Core.Models
{
    public class CustomerCartOffer : BaseModel
    {
        public int Id { get; set; }

        [ForeignKey("CustomerCart")]
        public int CartId { get; set; }
        public virtual CustomerCart CustomerCart { get; set; }

        [ForeignKey("OwnerOffer")]
        public int OwnerOffertId { get; set; }
        public virtual OwnerOffer OwnerOffer { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public double Total { get; set; }


    }
}
