using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace offerStation.Core.Models
{
    public class OwnerCardDetails : BaseModel
    {
        [ForeignKey("OwnerOrder")]
        public int Id { get; set; }
        public virtual OwnerOrder OwnerOrder { get; set; }
        public string Name { get; set; }
        public string CardNumber { get; set; }
    }
}
