using RestaurantReservation.Db.Models;
using RestaurantReservation.Db.Repositories;

namespace RestaurantReservation.Db.Services.Implementaion
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _repository;

        public OrderService(IOrderRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Order>> GetAllOrderInReservationAsync(int reservationId)
        {
            return await _repository.GetAllOrderInReservationAsync(reservationId);
        }

        public async Task<Order?> GetOrderByIdInReservationAsync(int reservationId, int orderId)
        {
            return await _repository.GetOrderByIdInReservationAsync(reservationId, orderId);
        }

        public async Task<Order> CreateOrderInReservationAsync(int reservationId, Order order)
        {
            if (order == null)
                throw new ArgumentNullException(nameof(order), "cannot be null.");

            return await _repository.CreateOrderInReservationAsync(reservationId, order);
        }

        public async Task<Order> UpdateAsync(Order order)
        {
            if (order == null)
                throw new ArgumentNullException(nameof(order), "cannot be null.");

            return await _repository.UpdateAsync(order);
        }

        public async Task DeleteAsync(Order order)
        {
            await _repository.DeleteAsync(order);
        }
    }

}
