using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace offerStation.Core.Dtos
{
    public class OfferProductDto
    {
        public int? Id { get; set; }
        public int Quantity { get; set; }
        public int ProductId { get; set; }
    }
}
