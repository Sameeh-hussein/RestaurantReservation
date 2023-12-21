using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Repositories
{
    public class OrderItemsRepository : IRepository<OrderItems>
    {
        private readonly RestaurantReservationDbContext _Context;

        public OrderItemsRepository(RestaurantReservationDbContext context)
        {
            _Context = context;
        }

        public async Task<OrderItems> Create(OrderItems orderItems)
        {
            _Context.OrderItems.Add(orderItems);
            await _Context.SaveChangesAsync();
            return orderItems;
        }

        public async Task<OrderItems> Update(OrderItems orderItems)
        {
            _Context.OrderItems.Update(orderItems);
            await _Context.SaveChangesAsync();
            return orderItems;
        }

        public async Task<bool> Delete(int id)
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
