using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Repositories.Implementaion
{
    public class OrderRepository : IOrderRepository
    {
        private readonly RestaurantReservationDbContext _Context;

        public OrderRepository(RestaurantReservationDbContext context)
        {
            _Context = context;
        }

        public async Task<IEnumerable<Order>> GetAllOrderInReservationAsync(int reservationId)
        {
            return await _Context.Orders.Where(o => o.reservationId == reservationId)
                                        .ToListAsync();
        }

        public async Task<Order?> GetOrderByIdInReservationAsync(int reservationId, int orderId)
        {
            return await _Context.Orders.FirstOrDefaultAsync(o => o.reservationId == reservationId && o.orderId == orderId);
        }

        public async Task<Order> CreateOrderInReservationAsync(int reservationId, Order order)
        {
            _Context.Orders.Add(order);
            order.reservationId = reservationId;
            await _Context.SaveChangesAsync();
            return order;
        }

        public async Task<Order> UpdateAsync(Order order)
        {
            _Context.Orders.Update(order);
            await _Context.SaveChangesAsync();
            return order;
        }

        public async Task DeleteAsync(Order order)
        {
            _Context.Orders.Remove(order);
            await _Context.SaveChangesAsync();
        }
    }
}