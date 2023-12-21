using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Repositories
{
    public class OrderRepository : IRepository<Order>
    {
        private readonly RestaurantReservationDbContext _Context;

        public OrderRepository(RestaurantReservationDbContext context)
        {
            _Context = context;
        }

        public async Task<Order> Create(Order order)
        {
            _Context.Orders.Add(order);
            await _Context.SaveChangesAsync();
            return order;
        }

        public async Task<Order> Update(Order order)
        {
            _Context.Orders.Update(order);
            await _Context.SaveChangesAsync();
            return order;
        }

        public async Task<bool> Delete(int id)
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
