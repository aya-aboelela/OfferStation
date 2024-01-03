using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace offerStation.Core.Dtos
{
    public class CategoryDto : CategoryInfoDto
    {
        public int Id { get; set; }
    }
    public class CategoryInfoDto 
    {
        public string Name { get; set; }
        public byte[]? Image { get; set; }
    }
}
