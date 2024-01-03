using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace offerStation.Core.Models
{
    public class OwnerMenuCategory : BaseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte[]? Image { get; set; }

        [ForeignKey("Owner")]
        public int OwnerId { get; set; }
        public virtual Owner Owner { get; set; }

        public virtual List<OwnerProduct> OwnerProducts { get; set; }
    }
}
