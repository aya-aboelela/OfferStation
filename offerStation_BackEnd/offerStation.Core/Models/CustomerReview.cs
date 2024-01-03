using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace offerStation.Core.Models
{
    public class CustomerReview : BaseModel
    {
        public int Id { get; set; }

        [Required, Range(1, 5)]
        public int Rating { get; set; }
        public string Comment { get; set; }

        [ForeignKey("customer")]
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

        [ForeignKey ("Owner")]
        public int OwnerId { get; set; }
        public virtual  Owner Owner { get; set; }
        public DateTime CreatedTime { get; set; }
    }
}
