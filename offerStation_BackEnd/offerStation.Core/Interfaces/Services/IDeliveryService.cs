using offerStation.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace offerStation.Core.Interfaces.Services
{
    public interface IDeliveryService
    {
        Task<bool> AddDelivery(DeliveryDto deliveryDto);
        Task<bool> EditDelivery(int id, DeliveryDto deliveryDto);
        Task<bool> DeleteDelivery(int id);
    }
}
