using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantReservation.API.Authentication;
using RestaurantReservation.API.DTOEntity;
using RestaurantReservation.Db.Models;
using RestaurantReservation.Db.Services;

namespace RestaurantReservation.API.Controllers
{
    [Authorize]
    [Route("api/reservation/{reservationid}/orders/{orderid}/orderitems")]
    [ApiController]
    public class OrderItemsController : ControllerBase
    {
        private readonly IOrderItemsService _orderItemsService;
        private readonly IOrderService _orderService;
        private readonly IReservationService _reservationService;
        private readonly IMapper _mapper;
        private readonly ILogger<OrderItemsController> _logger;

        public OrderItemsController(
            IOrderItemsService orderItemsService,
            IOrderService orderService,
            IReservationService reservationService,
            IMapper mapper,
            ILogger<OrderItemsController> logger)
        {
            _orderItemsService = orderItemsService ??
                throw new ArgumentNullException(nameof(orderItemsService));
            _orderService = orderService ??
                throw new ArgumentNullException(nameof(orderService));
            _reservationService = reservationService ??
                throw new ArgumentNullException(nameof(reservationService));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
            _logger = logger ??
                throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderItemsDTO>>> GetAllOrderItemsInOrderInReservation(
                int reservationid,
                int orderid
            )
        {
            _logger.LogInformation($"Attempting to retrieve all order items for order ID: {orderid} in reservation ID: {reservationid}.");

            if (!await _reservationService.ReservationExist(reservationid))
            {
                _logger.LogWarning($"Reservation with ID: {reservationid} not found.");
                return NotFound();
            }

            if (!await _orderService.OrderExist(reservationid, orderid))
            {
                _logger.LogWarning($"Order with ID: {orderid} in reservation ID: {reservationid} not found.");
                return NotFound();
            }

            var orderItems = await _orderItemsService.GetAllOrderItemsInOrderAsync(orderid);
            var orderItemsToReturn = _mapper.Map<IEnumerable<OrderItemsDTO>>(orderItems);

            _logger.LogInformation($"Successfully retrieved order items for order ID: {orderid} in reservation ID: {reservationid}.");
            return Ok(orderItemsToReturn);
        }

        [HttpGet("{orderitemsid}", Name = "GetOrderItemsById")]
        public async Task<ActionResult<OrderItemsDTO>> GetOrderItemsInOrderInReservation(
                int reservationid,
                int orderid,
                int orderitemsid
            )
        {
            _logger.LogInformation($"Attempting to retrieve order item ID: {orderitemsid} for order ID: {orderid} in reservation ID: {reservationid}.");

            if (!await _reservationService.ReservationExist(reservationid) || !await _orderService.OrderExist(reservationid, orderid))
            {
                _logger.LogWarning($"Order or reservation not found for reservation ID: {reservationid}, order ID: {orderid}.");
                return NotFound();
            }

            var orderItem = await _orderItemsService.GetOrderItemsInOrderByIdAsync(orderid, orderitemsid);
            if (orderItem == null)
            {
                _logger.LogWarning($"Order item ID: {orderitemsid} for order ID: {orderid} in reservation ID: {reservationid} not found.");
                return NotFound();
            }

            var orderItemsToReturn = _mapper.Map<OrderItemsDTO>(orderItem);
            
            _logger.LogInformation($"Successfully retrieved order item ID: {orderitemsid} for order ID: {orderid} in reservation ID: {reservationid}.");
            
            return Ok(orderItemsToReturn);
        }

        [HttpPost]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult> AddOrderItemsInOrderInReservation(
                int reservationid,
                int orderid,
                OrderItemsForCreationDTO orderItemsForCreationDTO
            )
        {
            _logger.LogInformation($"Attempting to add an order item to order ID: {orderid} in reservation ID: {reservationid}.");

            try
            {
                if (!await _reservationService.ReservationExist(reservationid) || !await _orderService.OrderExist(reservationid, orderid))
                {
                    _logger.LogWarning($"Order or reservation not found for reservation ID: {reservationid}, order ID: {orderid}.");
                    return NotFound();
                }

                var orderItems = _mapper.Map<OrderItems>(orderItemsForCreationDTO);
                await _orderItemsService.CreateOrderItemsInOrderAsync(orderid, orderItems);

                var orderItemsToReturn = _mapper.Map<OrderItemsDTO>(orderItems);
                _logger.LogInformation($"Successfully added an order item to order ID: {orderid} in reservation ID: {reservationid}.");
                return CreatedAtRoute("GetOrderItemsById",
                        new
                        {
                            reservationid = reservationid,
                            orderid = orderid,
                            orderitemsid = orderItemsToReturn.orderItemId
                        },
                        orderItemsToReturn
                    );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error adding order item to order ID: {orderid} in reservation ID: {reservationid}.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpPut("{orderitemsid}")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult> UpdateOrderItemsInOrderInReservation(
                int reservationid,
                int orderid,
                int orderitemsid,
                OrderItemsForUpdateDTO orderItemsForUpdateDTO
            )
        {
            _logger.LogInformation($"Attempting to update order item ID: {orderitemsid} in order ID: {orderid} in reservation ID: {reservationid}.");

            try
            {
                if (!await _reservationService.ReservationExist(reservationid) || !await _orderService.OrderExist(reservationid, orderid))
                {
                    _logger.LogWarning($"Order or reservation not found for reservation ID: {reservationid}, order ID: {orderid}.");
                    return NotFound();
                }

                var orderItem = await _orderItemsService.GetOrderItemsInOrderByIdAsync(orderid, orderitemsid);
                if (orderItem == null)
                {
                    _logger.LogWarning($"Order item ID: {orderitemsid} for order ID: {orderid} in reservation ID: {reservationid} not found.");
                    return NotFound();
                }

                _mapper.Map(orderItemsForUpdateDTO, orderItem);
                await _orderItemsService.UpdateAsync(orderItem);

                _logger.LogInformation($"Successfully updated order item ID: {orderitemsid} in order ID: {orderid} in reservation ID: {reservationid}.");
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating order item ID: {orderitemsid} in order ID: {orderid} in reservation ID: {reservationid}.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpDelete("{orderitemsid}")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult> DeleteOrderItemsInOrderInReservation(
                int reservationid,
                int orderid,
                int orderitemsid
            )
        {
            _logger.LogInformation($"Attempting to delete order item ID: {orderitemsid} from order ID: {orderid} in reservation ID: {reservationid}.");

            try
            {
                if (!await _reservationService.ReservationExist(reservationid) || !await _orderService.OrderExist(reservationid, orderid))
                {
                    _logger.LogWarning($"Order or reservation not found for reservation ID: {reservationid}, order ID: {orderid}.");
                    return NotFound();
                }

                var orderItem = await _orderItemsService.GetOrderItemsInOrderByIdAsync(orderid, orderitemsid);
                if (orderItem == null)
                {
                    _logger.LogWarning($"Order item ID: {orderitemsid} for order ID: {orderid} in reservation ID: {reservationid} not found.");
                    return NotFound();
                }

                await _orderItemsService.DeleteAsync(orderItem);
                _logger.LogInformation($"Successfully deleted order item ID: {orderitemsid} from order ID: {orderid} in reservation ID: {reservationid}.");
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting order item ID: {orderitemsid} from order ID: {orderid} in reservation ID: {reservationid}.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
    }
}
