using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Services
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> GetAllAsync();
        Task<Order?> GetByIdAsync(int id);
        Task<Order> CreateAsync(Order order);
        Task<bool> DeleteAsync(int Id);
        Task<Order> UpdateAsync(Order order);
    }
}
