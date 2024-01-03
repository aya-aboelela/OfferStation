using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace offerStation.Core.Models
{
    public class City : BaseModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } 
        public virtual List<Address> Address { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
