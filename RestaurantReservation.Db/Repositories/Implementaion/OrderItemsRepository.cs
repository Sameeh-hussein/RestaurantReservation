using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Repositories.Implementaion
{
    public class OrderItemsRepository : IOrderItemsRepository
    {
        private readonly RestaurantReservationDbContext _Context;

        public OrderItemsRepository(RestaurantReservationDbContext context)
        {
            _Context = context;
        }

        public async Task<IEnumerable<OrderItems>> GetAllOrderItemsInOrderAsync(int orderid)
        {
            return await _Context.OrderItems.Where(oi => oi.orderId == orderid)
                                            .ToListAsync();
        }

        public async Task<OrderItems?> GetOrderItemsInOrderByIdAsync(int orderId, int orderItemsId)
        {
            return await _Context.OrderItems.FirstOrDefaultAsync(c => c.orderId == orderId && c.orderItemId == orderItemsId);
        }

        public async Task<OrderItems> CreateOrderItemsInOrderAsync(int orderId, OrderItems orderItems)
        {
            _Context.OrderItems.Add(orderItems);
            orderItems.orderId = orderId;
            await _Context.SaveChangesAsync();
            return orderItems;
        }

        public async Task<OrderItems> UpdateAsync(OrderItems orderItems)
        {
            _Context.OrderItems.Update(orderItems);
            await _Context.SaveChangesAsync();
            return orderItems;
        }

        public async Task DeleteAsync(OrderItems orderItems)
        {
            _Context.OrderItems.Remove(orderItems);
            await _Context.SaveChangesAsync();
        }
    }
}