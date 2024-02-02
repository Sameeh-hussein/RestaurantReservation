using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        public OrderController(IOrderService orderService, IReservationService reservationService, IMapper mapper)
        {
            _orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));
            _reservationService = reservationService ?? throw new ArgumentNullException(nameof(reservationService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> GetAllOrdersInReservation(int reservationid)
        {
            if(! await _reservationService.ReservationExist(reservationid))
            {
                return NotFound();
            }

            var orders = await _orderService.GetAllOrderInReservationAsync(reservationid);

            var orderYoReturn = _mapper.Map<IEnumerable<OrderDTO>>(orders);

            return Ok(orderYoReturn);
        }

        [HttpGet("{orderid}", Name = "GetOrderInReservationById")]
        public async Task<ActionResult<OrderDTO>> GetOrderInReservation(int reservationid, int orderid)
        {
            if (!await _reservationService.ReservationExist(reservationid))
            {
                return NotFound();
            }

            var order = await _orderService.GetOrderByIdInReservationAsync(reservationid, orderid);
            if(order == null)
            {
                return NotFound();
            }

            var orderToReturn = _mapper.Map<OrderDTO>(order);

            return Ok(orderToReturn);
        }
        [HttpPost]
        public async Task<ActionResult> AddOrderToReservation(
                int reservationid,
                OrderForCreationDTO orderForCreationDTO
            )
        {
            if (!await _reservationService.ReservationExist(reservationid))
            {
                return NotFound();
            }

            var order = _mapper.Map<Order>(orderForCreationDTO);

            await _orderService.CreateOrderInReservationAsync(reservationid, order);

            var orderToReturn = _mapper.Map<OrderDTO>(order);

            return CreatedAtRoute("GetOrderInReservationById",
                    new
                    {
                        reservationid = reservationid,
                        orderid = orderToReturn.reservationId
                    },
                    orderToReturn
                );
        }

        [HttpPut("{orderid}")]
        public async Task<ActionResult> UpdateOrderInReservation(
                int reservationid, 
                int orderid,
                OrderForUpdateDTO orderForUpdateDTO
            )
        {
            if (!await _reservationService.ReservationExist(reservationid))
            {
                return NotFound();
            }

            var order = await _orderService.GetOrderByIdInReservationAsync(reservationid, reservationid);
            if(order == null)
            {
                return NotFound();
            }

            _mapper.Map(orderForUpdateDTO, order);

            await _orderService.UpdateAsync(order);

            return NoContent();
        }

        [HttpDelete("{orderid}")]
        public async Task<ActionResult> DeleteOrderInReservation(int reservationid, int orderid)
        {
            if (!await _reservationService.ReservationExist(reservationid))
            {
                return NotFound();
            }

            var order = await _orderService.GetOrderByIdInReservationAsync(reservationid, orderid);
            if(order == null)
            {
                return NotFound();
            }

            await _orderService.DeleteAsync(order);

            return NoContent();
        }
    }
}
