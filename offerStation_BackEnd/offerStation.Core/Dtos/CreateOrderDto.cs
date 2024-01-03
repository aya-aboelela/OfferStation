using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace offerStation.Core.Dtos
{
    public class CreateOrderDto
    {
        public int ItemsCount { get; set; }
        public double Total { get; set; }
    }
}
