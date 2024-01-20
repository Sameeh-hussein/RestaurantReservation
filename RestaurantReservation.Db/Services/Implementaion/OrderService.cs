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

        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Order?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<Order> CreateAsync(Order order)
        {
            if (order == null)
                throw new ArgumentNullException(nameof(order), "cannot be null.");

            return await _repository.CreateAsync(order);
        }

        public async Task<Order> UpdateAsync(Order order)
        {
            if (order == null)
                throw new ArgumentNullException(nameof(order), "cannot be null.");

            return await _repository.UpdateAsync(order);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
    }

}
