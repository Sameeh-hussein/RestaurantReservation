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
    /// <summary>
    /// API endpoints for managing order items
    /// </summary>>

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

        /// <summary>
        /// Retrieves all order items in an order within a reservation.
        /// </summary>
        /// <param name="reservationid">The ID of the reservation.</param>
        /// <param name="orderid">The ID of the order within the reservation.</param>
        /// <returns>
        /// ActionResult representing a collection of order item data transfer objects (DTOs).
        /// </returns>
        /// <response code="200">Returns the collection of order items.</response>
        /// <response code="404">If the reservation or order with the given IDs are not found.</response>
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

        /// <summary>
        /// Retrieves a specific order item within an order in a reservation.
        /// </summary>
        /// <param name="reservationid">The ID of the reservation.</param>
        /// <param name="orderid">The ID of the order within the reservation.</param>
        /// <param name="orderitemsid">The ID of the order item to retrieve.</param>
        /// <returns>
        /// ActionResult representing the order item data transfer object (DTO) with the specified ID.
        /// </returns>
        /// <response code="200">Returns the order item with the specified ID.</response>
        /// <response code="404">If the reservation, order, or order item with the given IDs are not found.</response>
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

        /// <summary>
        /// Adds an order item to an order within a reservation.
        /// </summary>
        /// <param name="reservationid">The ID of the reservation.</param>
        /// <param name="orderid">The ID of the order within the reservation.</param>
        /// <param name="orderItemsForCreationDTO">The data transfer object (DTO) containing information about the order item to be added.</param>
        /// <returns>
        /// ActionResult representing the result of the add operation.
        /// </returns>
        /// <response code="201">Indicates that the order item was successfully added to the order within the reservation.</response>
        /// <response code="400">If the input data is invalid.</response>
        /// <response code="401">If the request is not authorized (user does not have the required role).</response>
        /// <response code="404">If the reservation or order with the given IDs are not found.</response>
        /// <response code="500">If an unexpected error occurs during order item addition.</response>
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

        /// <summary>
        /// Updates an order item within an order in a reservation.
        /// </summary>
        /// <param name="reservationid">The ID of the reservation.</param>
        /// <param name="orderid">The ID of the order within the reservation.</param>
        /// <param name="orderitemsid">The ID of the order item to be updated.</param>
        /// <param name="orderItemsForUpdateDTO">The data transfer object (DTO) containing updated information for the order item.</param>
        /// <returns>
        /// ActionResult representing the result of the update operation.
        /// </returns>
        /// <response code="204">Indicates that the order item was successfully updated.</response>
        /// <response code="400">If the input data is invalid.</response>
        /// <response code="401">If the request is not authorized (user does not have the required role).</response>
        /// <response code="404">If the reservation, order, or order item with the given IDs are not found.</response>
        /// <response code="500">If an unexpected error occurs during order item update.</response>
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

        /// <summary>
        /// Deletes an order item from an order within a reservation.
        /// </summary>
        /// <param name="reservationid">The ID of the reservation.</param>
        /// <param name="orderid">The ID of the order within the reservation.</param>
        /// <param name="orderitemsid">The ID of the order item to be deleted.</param>
        /// <returns>
        /// ActionResult representing the result of the delete operation.
        /// </returns>
        /// <response code="204">Indicates that the order item was successfully deleted from the order within the reservation.</response>
        /// <response code="401">If the request is not authorized (user does not have the required role).</response>
        /// <response code="404">If the reservation, order, or order item with the given IDs are not found.</response>
        /// <response code="500">If an unexpected error occurs during order item deletion.</response>
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
