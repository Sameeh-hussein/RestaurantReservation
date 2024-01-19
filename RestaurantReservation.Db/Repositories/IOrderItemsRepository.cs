using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Repositories
{
    public interface IOrderItemsRepository
    {
        Task<IEnumerable<OrderItems>> GetAllAsync();
        Task<OrderItems?> GetByIdAsync(int id);
        Task<OrderItems> CreateAsync(OrderItems orderItems);
        Task<bool> DeleteAsync(int id);
        Task<OrderItems> UpdateAsync(OrderItems orderItems);
    }
}