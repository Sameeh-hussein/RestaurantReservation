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

        public async Task<IEnumerable<OrderItems>> GetAllAsync()
        {
            return await _Context.OrderItems.ToListAsync();
        }

        public async Task<OrderItems?> GetByIdAsync(int id)
        {
            return await _Context.OrderItems.FirstOrDefaultAsync(c => c.orderItemId == id);
        }

        public async Task<OrderItems> CreateAsync(OrderItems orderItems)
        {
            _Context.OrderItems.Add(orderItems);
            await _Context.SaveChangesAsync();
            return orderItems;
        }

        public async Task<OrderItems> UpdateAsync(OrderItems orderItems)
        {
            _Context.OrderItems.Update(orderItems);
            await _Context.SaveChangesAsync();
            return orderItems;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var orderItems = await _Context.OrderItems.FindAsync(id);
            if (orderItems != null)
            {
                _Context.OrderItems.Remove(orderItems);
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