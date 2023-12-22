using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Repositories
{
    public interface IOrderRepository
    {
        Task<Order> Create(Order order);
        Task<bool> Delete(int id);
        Task<Order> Update(Order order);
    }
}