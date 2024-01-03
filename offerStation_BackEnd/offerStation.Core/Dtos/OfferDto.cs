using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace offerStation.Core.Dtos
{
    public class OfferDto : OfferInfoDto
    {
        public int Id { get; set; }
        public double PrefPrice { get; set; }
        public byte[]? TraderImage { get; set; }
        public int ownerId { get; set; }
    }
    public class OfferInfoDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public byte[]? Image { get; set; }
        public List<OfferProductDto> Products { get; set; }
    }
    public class OfferDetailsDto : OfferDto
    {
       
        public DateTime CreatedTime { get; set; }
    }

}
