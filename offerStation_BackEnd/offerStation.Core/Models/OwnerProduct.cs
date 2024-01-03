using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace offerStation.Core.Models
{
    public class OwnerProduct : BaseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public int Discount { get; set; }
        public byte[]? Image { get; set; }
        public DateTime CreatedTime { get; set; }

        [ForeignKey("Owner")]
        public int OwnerId { get; set; }
        public Owner Owner { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public virtual OwnerMenuCategory Category { get; set; }
        public virtual List<OwnerOfferProduct> Offers { get; set; }
        public virtual List<CustomerOrderProduct> orders { get; set; }


    }
}
