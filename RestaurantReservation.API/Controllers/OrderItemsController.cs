using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantReservation.API.DTOEntity;
using RestaurantReservation.Db.Models;
using RestaurantReservation.Db.Services;

namespace RestaurantReservation.API.Controllers
{
    [Route("api/reservation/{reservationid}/orders/{orderid}/orderitems")]
    [ApiController]
    public class OrderItemsController : ControllerBase
    {
        private readonly IOrderItemsService _orderItemsService;
        private readonly IOrderService _orderService;
        private readonly IReservationService _reservationService;
        private readonly IMapper _mapper;

        public OrderItemsController(
            IOrderItemsService orderItemsService,
            IOrderService orderService,
            IReservationService reservationService,
            IMapper mapper)
        {
            _orderItemsService = orderItemsService ??
                throw new ArgumentNullException(nameof(orderItemsService));
            _orderService = orderService ??
                throw new ArgumentNullException(nameof(orderService));
            _reservationService = reservationService ??
                throw new ArgumentNullException(nameof(reservationService));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderItemsDTO>>> GetAllOrderItemsInOrderInReservation(
                int reservationid,
                int orderid
            )
        {
            var reservation = await _reservationService.GetByIdAsync(reservationid);
            if (reservation == null)
            {
                return NotFound();
            }

            var order = await _orderService.GetOrderByIdInReservationAsync(reservationid, orderid);
            if (order == null)
            {
                return NotFound();
            }

            var orderItems = await _orderItemsService.GetAllOrderItemsInOrderAsync(orderid);

            var orderItemsToReturn = _mapper.Map<IEnumerable<OrderItemsDTO>>(orderItems);

            return Ok(orderItemsToReturn);
        }

        [HttpGet("{orderitemsid}", Name = "GetOrderItemsById")]
        public async Task<ActionResult<OrderItemsDTO>> GetOrderItemsInOrderInReservation(
                int reservationid,
                int orderid,
                int orderitemsid
            )
        {
            var reservation = await _reservationService.GetByIdAsync(reservationid);
            if (reservation == null)
            {
                return NotFound();
            }

            var order = await _orderService.GetOrderByIdInReservationAsync(reservationid, orderid);
            if (order == null)
            {
                return NotFound();
            }

            var orderItem = await _orderItemsService.GetOrderItemsInOrderByIdAsync(orderid, orderitemsid);
            if (orderItem == null)
            {
                return NotFound();
            }

            var orderItemsToReturn = _mapper.Map<OrderItemsDTO>(orderItem);

            return Ok(orderItemsToReturn);
        }

        [HttpPost]
        public async Task<ActionResult> AddOrderItemsInOrderInReservation(
                int reservationid,
                int orderid,
                OrderItemsForCreationDTO orderItemsForCreationDTO
            )
        {
            var reservation = await _reservationService.GetByIdAsync(reservationid);
            if (reservation == null)
            {
                return NotFound();
            }

            var order = await _orderService.GetOrderByIdInReservationAsync(reservationid, orderid);
            if (order == null)
            {
                return NotFound();
            }

            var orderItems = _mapper.Map<OrderItems>(orderItemsForCreationDTO);

            await _orderItemsService.CreateOrderItemsInOrderAsync(orderid, orderItems);

            var orderItemsToReturn = _mapper.Map<OrderItemsDTO>(orderItems);

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

        [HttpPut("{orderitemsid}")]
        public async Task<ActionResult> UpdateOrderItemsInOrderInReservation(
                int reservationid,
                int orderid,
                int orderitemsid,
                OrderItemsForUpdateDTO orderItemsForUpdateDTO
            )
        {
            var reservation = await _reservationService.GetByIdAsync(reservationid);
            if (reservation == null)
            {
                return NotFound();
            }

            var order = await _orderService.GetOrderByIdInReservationAsync(reservationid, orderid);
            if (order == null)
            {
                return NotFound();
            }

            var orderItem = await _orderItemsService.GetOrderItemsInOrderByIdAsync(orderid, orderitemsid);
            if (orderItem == null)
            {
                return NotFound();
            }

            _mapper.Map(orderItemsForUpdateDTO, orderItem);

            await _orderItemsService.UpdateAsync(orderItem);

            return NoContent();
        }

        [HttpDelete("{orderitemsid}")]
        public async Task<ActionResult> DeleteOrderItemsInOrderInReservation(
                int reservationid,
                int orderid,
                int orderitemsid
            )
        {
            var reservation = await _reservationService.GetByIdAsync(reservationid);
            if (reservation == null)
            {
                return NotFound();
            }

            var order = await _orderService.GetOrderByIdInReservationAsync(reservationid, orderid);
            if (order == null)
            {
                return NotFound();
            }

            var orderItem = await _orderItemsService.GetOrderItemsInOrderByIdAsync(orderid, orderitemsid);
            if (orderItem == null)
            {
                return NotFound();
            }

            await _orderItemsService.DeleteAsync(orderItem);

            return NoContent();
        }
    }
}
