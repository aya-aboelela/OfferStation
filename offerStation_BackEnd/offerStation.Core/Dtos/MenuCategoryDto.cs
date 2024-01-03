using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace offerStation.Core.Dtos
{
    public class MenuCategoryDto
    {
        public string Name { get; set; }
        public byte[]? Image { get; set; }
    }
    public class MenuCategoryDetailsDto : MenuCategoryDto
    {
        public int Id { get; set; }
    }
}
