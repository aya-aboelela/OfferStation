using offerStation.Core.Constants;
using offerStation.Core.Dtos;
using OrderStation.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace offerStation.Core.Interfaces.Services
{
    public interface IOrderService
    {
        Task<bool> ChangeOwnerOrderStatus(int id, OrderStatus status);
        Task<bool> ChangeCustomerOrderStatus(int id, OrderStatus status);
        Task<bool> CreateOwnerOrderDelivery(int ownerOrderId, int deliveryId);
        Task<bool> CreateCustomerOrderDelivery(int customerOrderId, int deliveryId);
        Task<List<OrderDto>?> GetPendingOwnerOrders();
        Task<List<OrderDto>?> GetPendingCustomerOrders();
        Task<List<OrderDetailsDto>?> GetAllOwnerOrders(int customerId);
        Task<List<OrderDetailsDto>?> GetAllCustomerOrders(int customerId);
        Task<List<RequestedOrderDto>?> GetOwnerOrdersRequested(int ownerId);
        Task<List<RequestedOrderDto>?> GetSupplierOrdersRequested(int supplierId);
        Task<List<RequestedOrderDto>?> GetOwnerOrdersRequestedAftershipped(int ownerId);
        Task<List<RequestedOrderDto>?> GetSupplierOrdersRequestedAfterShipped(int supplierId);
        Task<List<DeliveryDto>> getAllDelivaries();

    }
}
