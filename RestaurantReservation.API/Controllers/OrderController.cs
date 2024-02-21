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
    /// API endpoints for managing orders
    /// </summary>>

    [Authorize]
    [Route("api/reservation/{reservationid}/orders")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IReservationService _reservationService;
        private readonly IMapper _mapper;
        private readonly ILogger<OrderController> _logger;

        public OrderController(
            IOrderService orderService,
            IReservationService reservationService,
            IMapper mapper,
            ILogger<OrderController> logger)
        {
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
        /// Retrieves all orders within a reservation by reservation ID.
        /// </summary>
        /// <param name="reservationid">The ID of the reservation.</param>
        /// <returns>
        /// A collection of order data transfer objects (DTOs) representing the orders in the reservation.
        /// </returns>
        /// <response code="200">Returns the collection of orders in the reservation.</response>
        /// <response code="404">If the reservation with the given ID is not found.</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> GetAllOrdersInReservation(int reservationid)
        {
            _logger.LogInformation($"Attempting to retrieve all orders for reservation ID: {reservationid}.");

            if (!await _reservationService.ReservationExist(reservationid))
            {
                _logger.LogWarning($"Reservation with ID: {reservationid} not found.");
                return NotFound();
            }

            var orders = await _orderService.GetAllOrderInReservationAsync(reservationid);

            var orderToReturn = _mapper.Map<IEnumerable<OrderDTO>>(orders);

            _logger.LogInformation($"Successfully retrieved orders for reservation ID: {reservationid}.");
            return Ok(orderToReturn);
        }

        /// <summary>
        /// Retrieves an order within a reservation by reservation ID and order ID.
        /// </summary>
        /// <param name="reservationid">The ID of the reservation.</param>
        /// <param name="orderid">The ID of the order.</param>
        /// <returns>
        /// ActionResult representing the result of the retrieval operation.
        /// </returns>
        /// <response code="200">Returns the order with the specified ID in the reservation.</response>
        /// <response code="404">If the reservation or the order with the given IDs is not found.</response>
        [HttpGet("{orderid}", Name = "GetOrderInReservationById")]
        public async Task<ActionResult<OrderDTO>> GetOrderInReservation(int reservationid, int orderid)
        {
            _logger.LogInformation($"Attempting to retrieve order ID: {orderid} for reservation ID: {reservationid}.");

            if (!await _reservationService.ReservationExist(reservationid))
            {
                _logger.LogWarning($"Reservation with ID: {reservationid} not found.");
                return NotFound();
            }

            var order = await _orderService.GetOrderByIdInReservationAsync(reservationid, orderid);
            if (order == null)
            {
                _logger.LogWarning($"Order ID: {orderid} for reservation ID: {reservationid} not found.");
                return NotFound();
            }

            var orderToReturn = _mapper.Map<OrderDTO>(order);

            _logger.LogInformation($"Successfully retrieved order ID: {orderid} for reservation ID: {reservationid}.");
            return Ok(orderToReturn);
        }

        /// <summary>
        /// Creates a new order in a reservation.
        /// </summary>
        /// <param name="reservationid">The ID of the reservation where the order will be created.</param>
        /// <param name="orderForCreationDTO">The data transfer object (DTO) containing information about the order to be created.</param>
        /// <returns>
        /// ActionResult representing the newly created order, along with a location header for accessing it.
        /// </returns>
        /// <response code="201">Returns the newly created order and a location header for accessing it.</response>
        /// <response code="400">If the input data is invalid.</response>
        /// <response code="401">If the request is not authorized (user does not have the required role).</response>
        /// <response code="404">If the reservation with the given ID is not found.</response>
        /// <response code="500">If an unexpected error occurs during order creation.</response>
        [HttpPost]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult> AddOrderToReservation(
                int reservationid,
                OrderForCreationDTO orderForCreationDTO
            )
        {
            _logger.LogInformation($"Attempting to add an order to reservation ID: {reservationid}.");

            try
            {
                if (!await _reservationService.ReservationExist(reservationid))
                {
                    _logger.LogWarning($"Reservation with ID: {reservationid} not found.");
                    return NotFound();
                }

                var order = _mapper.Map<Order>(orderForCreationDTO);

                await _orderService.CreateOrderInReservationAsync(reservationid, order);

                var orderToReturn = _mapper.Map<OrderDTO>(order);

                _logger.LogInformation($"Successfully added order to reservation ID: {reservationid}.");
                return CreatedAtRoute("GetOrderInReservationById",
                        new
                        {
                            reservationid = reservationid,
                            orderid = orderToReturn.reservationId
                        },
                        orderToReturn
                    );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error adding order to reservation ID: {reservationid}.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        /// <summary>
        /// Updates a order within a reservation.
        /// </summary>
        /// <param name="reservationid">The ID of the reservation containing the order to be updated.</param>
        /// <param name="orderid">The ID of the order to be updated.</param>
        /// <param name="orderForUpdateDTO">The data transfer object (DTO) containing the updated information for the order.</param>
        /// <returns>
        /// ActionResult representing the result of the update operation.
        /// </returns>
        /// <response code="204">Indicates that the order was successfully updated.</response>
        /// <response code="400">If the input data is invalid.</response>
        /// <response code="401">If the request is not authorized (user does not have the required role).</response>
        /// <response code="404">If the reservation or order with the given IDs is not found.</response>
        /// <response code="500">If an unexpected error occurs during order update.</response>
        [HttpPut("{orderid}")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult> UpdateOrderInReservation(
                int reservationid, 
                int orderid,
                OrderForUpdateDTO orderForUpdateDTO
            )
        {
            _logger.LogInformation($"Attempting to update order ID: {orderid} in reservation ID: {reservationid}.");

            try
            {
                if (!await _reservationService.ReservationExist(reservationid))
                {
                    _logger.LogWarning($"Reservation with ID: {reservationid} not found.");
                    return NotFound();
                }

                var order = await _orderService.GetOrderByIdInReservationAsync(reservationid, orderid);
                if (order == null)
                {
                    _logger.LogWarning($"Order ID: {orderid} in reservation ID: {reservationid} not found.");
                    return NotFound();
                }

                _mapper.Map(orderForUpdateDTO, order);

                await _orderService.UpdateAsync(order);

                _logger.LogInformation($"Successfully updated order ID: {orderid} in reservation ID: {reservationid}.");
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating order ID: {orderid} in reservation ID: {reservationid}.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        /// <summary>
        /// Deletes a order from a reservation.
        /// </summary>
        /// <param name="reservationid">The ID of the reservation from which the order will be deleted.</param>
        /// <param name="orderid">The ID of the table to be deleted.</param>
        /// <returns>
        /// ActionResult representing the result of the delete operation.
        /// </returns>
        /// <response code="204">Indicates that the order was successfully deleted.</response>
        /// <response code="401">If the request is not authorized (user does not have the required role).</response>
        /// <response code="404">If the reservation or order with the given IDs is not found.</response>
        /// <response code="500">If an unexpected error occurs during order deletion.</response>
        [HttpDelete("{orderid}")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult> DeleteOrderInReservation(int reservationid, int orderid)
        {
            _logger.LogInformation($"Attempting to delete order ID: {orderid} from reservation ID: {reservationid}.");

            try
            {
                if (!await _reservationService.ReservationExist(reservationid))
                {
                    _logger.LogWarning($"Reservation with ID: {reservationid} not found.");
                    return NotFound();
                }

                var order = await _orderService.GetOrderByIdInReservationAsync(reservationid, orderid);
                if (order == null)
                {
                    _logger.LogWarning($"Order ID: {orderid} in reservation ID: {reservationid} not found.");
                    return NotFound();
                }

                await _orderService.DeleteAsync(order);

                _logger.LogInformation($"Successfully deleted order ID: {orderid} from reservation ID: {reservationid}.");
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting order ID: {orderid} from reservation ID: {reservationid}.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
    }
}
