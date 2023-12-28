using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Repositories.Implementaion
{
    public class OrderRepository : Repository<Order>
    {
        public OrderRepository(RestaurantReservationDbContext context) : base(context)
        {
        }
    }
}
