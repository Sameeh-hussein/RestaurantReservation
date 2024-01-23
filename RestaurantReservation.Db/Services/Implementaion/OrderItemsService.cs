using RestaurantReservation.Db.Models;
using RestaurantReservation.Db.Repositories;

namespace RestaurantReservation.Db.Services.Implementaion
{
    public class OrderItemsService: IOrderItemsService
    {
        private readonly IOrderItemsRepository _repository;

        public OrderItemsService(IOrderItemsRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<OrderItems>> GetAllOrderItemsInOrderAsync(int orderid)
        {
            return await _repository.GetAllOrderItemsInOrderAsync(orderid);
        }

        public async Task<OrderItems?> GetOrderItemsInOrderByIdAsync(int orderId, int orderItemsId)
        {
            return await _repository.GetOrderItemsInOrderByIdAsync(orderId, orderItemsId);
        }

        public async Task<OrderItems> CreateOrderItemsInOrderAsync(int orderId, OrderItems orderItems)
        {
            if (orderItems == null)
                throw new ArgumentNullException(nameof(orderItems), "cannot be null.");

            return await _repository.CreateOrderItemsInOrderAsync(orderId, orderItems);
        }

        public async Task<OrderItems> UpdateAsync(OrderItems orderItems)
        {
            if (orderItems == null)
                throw new ArgumentNullException(nameof(orderItems), "cannot be null.");

            return await _repository.UpdateAsync(orderItems);
        }

        public async Task DeleteAsync(OrderItems orderItems)
        {
            await _repository.DeleteAsync(orderItems);
        }
    }

}
