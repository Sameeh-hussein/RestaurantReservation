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
