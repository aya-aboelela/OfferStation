using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace offerStation.Core.Models
{
    public class ApplicationUser :IdentityUser
    {
        [Required, MaxLength(100)]
        public string Name { get; set; }
        public virtual Admin? Admin { get; set; }
        public virtual Customer? Customer { get; set; }
        public virtual Owner? Owner{ get; set; }
        public virtual Supplier? Supplier { get; set; }
        public virtual List<Address>? Addresses { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
