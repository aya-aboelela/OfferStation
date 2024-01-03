using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace offerStation.Core.Models
{
    public class OwnerReview : BaseModel
    {
        public int Id { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }

        [ForeignKey("Owner")]
        public int OwnerId { get; set; }
        public  Owner Owner { get; set; }

        [ForeignKey("Supplier")]
        public int SupplierId { get; set; }
        public  Supplier Supplier { get; set; }
    }
}
