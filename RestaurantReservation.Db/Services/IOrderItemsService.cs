using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Services
{
    public interface IOrderItemsService
    {
        Task<IEnumerable<OrderItems>> GetAllOrderItemsInOrderAsync(int orderid);
        Task<OrderItems?> GetOrderItemsInOrderByIdAsync(int orderId, int orderItemsId);
        Task<OrderItems> CreateOrderItemsInOrderAsync(int orderId, OrderItems orderItems);
        Task DeleteAsync(OrderItems orderItems);
        Task<OrderItems> UpdateAsync(OrderItems orderItems);
    }
}
