using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace offerStation.Core.Models
{
    public class OwnerOffer : BaseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public byte[]? Image { get; set; }
        public double Price { get; set; }
        public DateTime  CreatedTime { get; set; }
       
        [ForeignKey("Owner")]
        public int OwnerId { get; set; }
        public virtual Owner Owner { get; set; }
        public virtual List<OwnerOfferProduct> Products { get; set; }
        public virtual List<CustomerOrderOffer> Orders { get; set; }
    }
}
