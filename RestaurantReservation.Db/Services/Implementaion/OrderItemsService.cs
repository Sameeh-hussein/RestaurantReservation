using RestaurantReservation.Db.Models;
using RestaurantReservation.Db.Repositories;

namespace RestaurantReservation.Db.Services.Implementaion
{
    public class OrderItemsService
    {
        private readonly IOrderItemsRepository _repository;

        public OrderItemsService(IOrderItemsRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<OrderItems>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<OrderItems?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<OrderItems> CreateAsync(OrderItems orderItems)
        {
            if (orderItems == null)
                throw new ArgumentNullException(nameof(orderItems), "cannot be null.");

            return await _repository.CreateAsync(orderItems);
        }

        public async Task<OrderItems> UpdateAsync(OrderItems orderItems)
        {
            if (orderItems == null)
                throw new ArgumentNullException(nameof(orderItems), "cannot be null.");

            return await _repository.UpdateAsync(orderItems);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
    }

}
