using AutoMapper;
using offerStation.Core.Constants;
using offerStation.Core.Dtos;
using offerStation.Core.Interfaces;
using offerStation.Core.Interfaces.Services;
using offerStation.Core.Models;
using OrderStation.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace offerStation.EF.Services
{
    public class OrderService : IOrderService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public OrderService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> ChangeOwnerOrderStatus(int id, OrderStatus status)
        {
            var order = await _unitOfWork.OwnerOrders.GetByIdAsync(id);
            if (order is not null)
            {
                order.orderStatus = status;

                _unitOfWork.OwnerOrders.Update(order);
                _unitOfWork.Complete();

                return true;
            }
            return false;
        }
        public async Task<bool> ChangeCustomerOrderStatus(int id, OrderStatus status)
        {
            var order = await _unitOfWork.CustomerOrders.GetByIdAsync(id);
            if (order is not null)
            {
                order.orderStatus = status;

                _unitOfWork.CustomerOrders.Update(order);
                _unitOfWork.Complete();

                return true;
            }
            return false;
        }
        public async Task<bool> CreateOwnerOrderDelivery(int ownerOrderId, int deliveryId)
        {
            var delivery = await _unitOfWork.Deliveries.GetByIdAsync(deliveryId);
            var ownerOrder = await _unitOfWork.OwnerOrders.GetByIdAsync(ownerOrderId);

            if (ownerOrder is not null && delivery is not null)
            {
                OwnerOrderDelivery orderDelivery = new()
                {
                    OwnerOrderId = ownerOrderId,
                    DeliveryId = deliveryId,
                };

                _unitOfWork.OwnerOrderDeliveries.Add(orderDelivery);
                _unitOfWork.Complete();

                return true;
            }
            return false;
        }
        public async Task<bool> CreateCustomerOrderDelivery(int customerOrderId, int deliveryId)
        {
            var delivery = await _unitOfWork.Deliveries.GetByIdAsync(deliveryId);
            var customerOrder = await _unitOfWork.CustomerOrders.GetByIdAsync(customerOrderId);

            if (customerOrder is not null && delivery is not null)
            {
                CustomerOrderDelivery orderDelivery = new()
                {
                    CustomerOrderId = customerOrderId,
                    DeliveryId = deliveryId,
                };

                _unitOfWork.CustomerOrderDeliveries.Add(orderDelivery);
                _unitOfWork.Complete();

                return true;
            }
            return false;
        }
        public async Task<List<OrderDetailsDto>?> GetAllOwnerOrders(int ownerId)
        {
            List<OrderDetailsDto> ordersList = null;

            var orders = await _unitOfWork.OwnerOrders
                .FindAllAsync(o => o.OwnerId == ownerId && o.orderStatus != OrderStatus.delivered && !o.IsDeleted,
                new List<Expression<Func<OwnerOrder, object>>>()
                {
                    o => o.Supplier.AppUser,
                    o => o.Products,
                    o => o.Offers,
                });

            if(orders is not null)
            {
                ordersList = _mapper.Map<List<OrderDetailsDto>>(orders);
            }
            return ordersList;
        }
        public async Task<List<OrderDetailsDto>?> GetAllCustomerOrders(int customerId)
        {
            List<OrderDetailsDto> ordersList = null;

            var orders = await _unitOfWork.CustomerOrders
                .FindAllAsync(o => o.CustomerId == customerId && o.orderStatus != OrderStatus.delivered && !o.IsDeleted,
                new List<Expression<Func<CustomerOrder, object>>>()
                {
                    o => o.Owner.AppUser,
                    o => o.Products,
                    o => o.Offers,
                });

            if (orders is not null)
            {
                ordersList = _mapper.Map<List<OrderDetailsDto>>(orders);
            }
            return ordersList;
        }
        public async Task<List<RequestedOrderDto>?> GetOwnerOrdersRequested(int ownerId)
        {
            List<RequestedOrderDto> ordersList = null;

            var orders = await _unitOfWork.CustomerOrders
                .FindAllAsync(o => o.OwnerId == ownerId && o.orderStatus == OrderStatus.ordered && !o.IsDeleted,
                new List<Expression<Func<CustomerOrder, object>>>()
                {
                    o => o.Customer.AppUser,
                    o => o.Products,
                    o => o.Offers,
                });

            if (orders is not null)
            {
                ordersList = _mapper.Map<List<RequestedOrderDto>>(orders);
            }
            return ordersList;
        }
        public async Task<List<RequestedOrderDto>?> GetOwnerOrdersRequestedAftershipped(int ownerId)
        {
            List<RequestedOrderDto> ordersList = null;

            var orders = await _unitOfWork.CustomerOrders
                .FindAllAsync(o => o.OwnerId == ownerId && (o.orderStatus == OrderStatus.shipped || o.orderStatus== OrderStatus.delivered) && !o.IsDeleted,
                new List<Expression<Func<CustomerOrder, object>>>()
                {
                    o => o.Customer.AppUser,
                    o => o.Products,
                    o => o.Offers,
                });

            if (orders is not null)
            {
                ordersList = _mapper.Map<List<RequestedOrderDto>>(orders);
            }
            return ordersList;
        }
        public async Task<List<RequestedOrderDto>?> GetSupplierOrdersRequested(int supplierId)
        {
            List<RequestedOrderDto> ordersList = null;

            var orders = await _unitOfWork.OwnerOrders
                .FindAllAsync(o => o.SupplierId == supplierId && o.orderStatus == OrderStatus.ordered && !o.IsDeleted,
                new List<Expression<Func<OwnerOrder, object>>>()
                {
                    o => o.Owner.AppUser,
                    o => o.Products,
                    o => o.Offers,
                });

            if (orders is not null)
            {
                ordersList = _mapper.Map<List<RequestedOrderDto>>(orders);
            }
            return ordersList;
        }
        public async Task<List<RequestedOrderDto>?> GetSupplierOrdersRequestedAfterShipped(int supplierId)
        {
            List<RequestedOrderDto> ordersList = null;

            var orders = await _unitOfWork.OwnerOrders
                .FindAllAsync(o => o.SupplierId == supplierId && (o.orderStatus == OrderStatus.shipped || o.orderStatus == OrderStatus.delivered) && !o.IsDeleted,
                new List<Expression<Func<OwnerOrder, object>>>()
                {
                    o => o.Owner.AppUser,
                    o => o.Products,
                    o => o.Offers,
                });

            if (orders is not null)
            {
                ordersList = _mapper.Map<List<RequestedOrderDto>>(orders);
            }
            return ordersList;
        }
        public async Task<List<OrderDto>?> GetPendingOwnerOrders()
        {
            List<OrderDto> pendingOrdersList = null;

            var pendingOrders = await _unitOfWork.OwnerOrders
                .FindAllAsync(o => o.orderStatus == OrderStatus.pending && !o.IsDeleted,
                new List<Expression<Func<OwnerOrder, object>>>()
                {
                    o => o.Owner.AppUser,
                    o => o.Supplier.AppUser,
                });

            if (pendingOrders is not null)
            {
                pendingOrdersList = _mapper.Map<List<OrderDto>>(pendingOrders);
            }
            return pendingOrdersList;
        }
        public async Task<List<OrderDto>?> GetPendingCustomerOrders()
        {
            List<OrderDto> pendingOrdersList = null;

            var pendingOrders = await _unitOfWork.CustomerOrders
                .FindAllAsync(o => o.orderStatus == OrderStatus.pending && !o.IsDeleted,
                new List<Expression<Func<CustomerOrder, object>>>()
                {
                    o => o.Owner.AppUser,
                    o => o.Customer.AppUser,
                });

            if (pendingOrders is not null)
            {
                pendingOrdersList = _mapper.Map<List<OrderDto>>(pendingOrders);
            }
            return pendingOrdersList;
        }
        public async Task<List<DeliveryDto>> getAllDelivaries()
        {
           var deliveries=await _unitOfWork.Deliveries.FindAllAsync(d => d.IsDeleted == false);
            List<DeliveryDto> deliveryOrdersList = null;
            if (deliveries is not null)
            {
                deliveryOrdersList = _mapper.Map<List<DeliveryDto>>(deliveries);
            }
            return deliveryOrdersList;

        }
    }
}
