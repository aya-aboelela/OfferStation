using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using offerStation.Core.Constants;
using offerStation.Core.Dtos;
using offerStation.Core.Interfaces.Services;
using offerStation.EF.Services;
using OrderStation.Core.Dtos;

namespace offerStation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        [HttpGet("OwnerOrderStatus")]
        public async Task<ActionResult<ApiResponse>> OwnerOrderStatus(int id, OrderStatus status)
        {
            var success = await _orderService.ChangeOwnerOrderStatus(id, status);
            if (success)
            {
                return Ok(new ApiResponse(201, true, success));
            }
            return BadRequest(new ApiResponse(500, false, "server error"));
        }
        [HttpGet("CustomerOrderStatus")]
        public async Task<ActionResult<ApiResponse>> CustomerOrderStatus(int id, OrderStatus status)
        {
            var success = await _orderService.ChangeCustomerOrderStatus(id, status);
            if (success)
            {
                return Ok(new ApiResponse(201, true, success));
            }
            return BadRequest(new ApiResponse(500, false, "server error"));
        }
        [HttpGet("ownerOrderDelivery")]
        public async Task<ActionResult<ApiResponse>> OwnerOrderDelivery(int ownerOrderId, int deliveryId)
        {
            var success = await _orderService.CreateOwnerOrderDelivery(ownerOrderId, deliveryId);
            if (success)
            {
                return Ok(new ApiResponse(201, true, success));
            }
            return BadRequest(new ApiResponse(500, false, "server error"));
        }
        [HttpGet("customerOrderDelivery")]
        public async Task<ActionResult<ApiResponse>> CustomerOrderDelivery(int customerOrderId, int deliveryId)
        {
            var success = await _orderService.CreateCustomerOrderDelivery(customerOrderId, deliveryId);
            if (success)
            {
                return Ok(new ApiResponse(201, true, success));
            }
            return BadRequest(new ApiResponse(500, false, "server error"));
        }
        [HttpGet("ownerOrders")]
        public async Task<ActionResult<ApiResponse>> AllOwnerOrders(int ownerId)
        {
             List<OrderDetailsDto> orders = await _orderService.GetAllOwnerOrders(ownerId);

            if (orders is null)
            {
                return BadRequest(new ApiResponse(404, false, "null object"));
            }
            return Ok(new ApiResponse(200, true, orders));
        }
        
        [HttpGet("customerOrders")]
        public async Task<ActionResult<ApiResponse>> AllCustomerOrders(int customerId)
        {
            List<OrderDetailsDto> orders = await _orderService.GetAllCustomerOrders(customerId);

            if (orders is null)
            {
                return BadRequest(new ApiResponse(404, false, "null object"));
            }
            return Ok(new ApiResponse(200, true, orders));
        }
        [HttpGet("ownerOrdersRequested")]
        public async Task<ActionResult<ApiResponse>> AllOwnerOrdersRequested(int ownerId)
        {
            List<RequestedOrderDto> orders = await _orderService.GetOwnerOrdersRequested(ownerId);

            if (orders is null)
            {
                return BadRequest(new ApiResponse(404, false, "null object"));
            }
            return Ok(new ApiResponse(200, true, orders));
        }
        [HttpGet("ownerOrdersRequestedAfterShipped")]
        public async Task<ActionResult<ApiResponse>> AllOwnerOrdersRequestedAfterShipped(int ownerId)
        {
            List<RequestedOrderDto> orders = await _orderService.GetOwnerOrdersRequestedAftershipped(ownerId);

            if (orders is null)
            {
                return BadRequest(new ApiResponse(404, false, "null object"));
            }
            return Ok(new ApiResponse(200, true, orders));
        }
        [HttpGet("supplierOrdersRequested")]
        public async Task<ActionResult<ApiResponse>> AllSupplierOrdersRequested(int supplierId)
        {
            List<RequestedOrderDto> orders = await _orderService.GetSupplierOrdersRequested(supplierId);

            if (orders is null)
            {
                return BadRequest(new ApiResponse(404, false, "null object"));
            }
            return Ok(new ApiResponse(200, true, orders));
        }
        [HttpGet("supplierOrdersRequestedAfterShipped")]
        public async Task<ActionResult<ApiResponse>> AllSupplierOrdersRequestedAfterShipped(int supplierId)
        {
            List<RequestedOrderDto> orders = await _orderService.GetSupplierOrdersRequestedAfterShipped(supplierId);

            if (orders is null)
            {
                return BadRequest(new ApiResponse(404, false, "null object"));
            }
            return Ok(new ApiResponse(200, true, orders));
        }
        [HttpGet("pendingOwnersOrders")]
        public async Task<ActionResult<ApiResponse>> PendingOwnersOrders()
        {
            List<OrderDto> orders = await _orderService.GetPendingOwnerOrders();

            if (orders is null)
            {
                return BadRequest(new ApiResponse(404, false, "null object"));
            }
            return Ok(new ApiResponse(200, true, orders));
        }
        [HttpGet("pendingCustomersOrders")]
        public async Task<ActionResult<ApiResponse>> PendingCustomersOrders()
        {
            List<OrderDto> orders = await _orderService.GetPendingCustomerOrders();

            if (orders is null)
            {
                return BadRequest(new ApiResponse(404, false, "null object"));
            }
            return Ok(new ApiResponse(200, true, orders));
        }

        [HttpGet("AllDeliveries")]
        public async Task<ActionResult<ApiResponse>> getAllDeliveres()
        {
            List<DeliveryDto> Deliveraes = await _orderService.getAllDelivaries();


            if (Deliveraes is null)
            {
                return BadRequest(new ApiResponse(404, false, "null object"));
            }
            return Ok(new ApiResponse(200, true, Deliveraes));
        }
    }
}
