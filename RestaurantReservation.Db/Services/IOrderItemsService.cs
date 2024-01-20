using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Services
{
    public interface IOrderItemsService
    {
        Task<IEnumerable<OrderItems>> GetAllAsync();
        Task<OrderItems?> GetByIdAsync(int id);
        Task<OrderItems> CreateAsync(OrderItems orderItems);
        Task<bool> DeleteAsync(int Id);
        Task<OrderItems> UpdateAsync(OrderItems orderItems);
    }
}
