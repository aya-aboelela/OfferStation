using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace offerStation.Core.Dtos
{
    public class ReviewDto : ReviewInfoDto
    {
        public int Id { get; set; }
        public string PersonName { get; set; }
    }
    public class ReviewInfoDto
    {
        public int Rating { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedTime { get; set; }
    }
    
}
