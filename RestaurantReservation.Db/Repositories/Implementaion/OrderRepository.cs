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

        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            return await _Context.Orders.ToListAsync();
        }

        public async Task<Order?> GetByIdAsync(int id)
        {
            return await _Context.Orders.FirstOrDefaultAsync(c => c.orderId == id);
        }

        public async Task<Order> CreateAsync(Order order)
        {
            _Context.Orders.Add(order);
            await _Context.SaveChangesAsync();
            return order;
        }

        public async Task<Order> UpdateAsync(Order order)
        {
            _Context.Orders.Update(order);
            await _Context.SaveChangesAsync();
            return order;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var order = await _Context.Orders.FindAsync(id);
            if (order != null)
            {
                _Context.Orders.Remove(order);
                await _Context.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}