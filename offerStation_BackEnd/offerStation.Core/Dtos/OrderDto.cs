using offerStation.Core.Constants;
using offerStation.Core.Dtos;
using offerStation.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderStation.Core.Dtos
{
    public class OrderDto
    {
        public int Id { get; set; }
        public DateTime orderDate { get; set; }      
        public string TraderName { get; set; }
        public string RequesterName { get; set; }
        public double AdminTotal { get; set; } = 0;
    }
    public class RequestedOrderDto
    {
        public int Id { get; set; }
        public DateTime orderDate { get; set; }
        public double NetTotal { get; set; } = 0;
        public OrderStatus orderStatus { get; set; }
        public int RequesterId { get; set; }
        public string RequesterName { get; set; }
        public List<OrderProductDto> Products { get; set; }
        public List<OrderOfferDto> Offers { get; set; }
    }
    public class OrderDetailsDto 
    {
        public int Id { get; set; }
        public DateTime orderDate { get; set; }
        public double Total { get; set; } = 0;
        public int TraderId { get; set; }
        public string TraderName { get; set; }
        public OrderStatus orderStatus { get; set; }
        public List<OrderProductDto> Products { get; set; }
        public List<OrderOfferDto> Offers { get; set; }
    }
}
