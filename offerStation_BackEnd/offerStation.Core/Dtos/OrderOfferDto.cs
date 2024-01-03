using offerStation.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace offerStation.Core.Dtos
{
    public class OrderOfferDto
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int TraderOfferId { get; set; }
        public int Quantity { get; set; }
    }
}
