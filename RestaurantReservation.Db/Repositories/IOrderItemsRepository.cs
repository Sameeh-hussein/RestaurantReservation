using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Repositories
{
    public interface IOrderItemsRepository
    {
        Task<OrderItems> Create(OrderItems orderItems);
        Task<bool> Delete(int id);
        Task<OrderItems> Update(OrderItems orderItems);
    }
}