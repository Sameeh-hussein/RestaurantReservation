using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Repositories.Implementaion
{
    public class RestaurantRepository : Repository<Restaurant>
    {
        public RestaurantRepository(RestaurantReservationDbContext context) : base(context)
        {
        }
    }
}
