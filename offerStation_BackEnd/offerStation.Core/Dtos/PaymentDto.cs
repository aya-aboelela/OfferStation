using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace offerStation.Core.Dtos
{
    public class PaymentDto
    {
        public string PayerID { get; set; }
        public int CityId { get; set; }
   

    }

}
