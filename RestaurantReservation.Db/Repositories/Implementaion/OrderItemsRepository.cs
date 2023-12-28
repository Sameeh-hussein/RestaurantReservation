using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Repositories.Implementaion
{
    public class OrderItemsRepository : Repository<OrderItems>
    {
        public OrderItemsRepository(RestaurantReservationDbContext context) : base(context)
        {
        }
    }
}
