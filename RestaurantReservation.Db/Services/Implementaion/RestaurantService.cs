using RestaurantReservation.Db.Models;
using RestaurantReservation.Db.Repositories;

namespace RestaurantReservation.Db.Services.Implementaion
{
    public class RestaurantService : Service<Restaurant>
    {
        public RestaurantService(IRepository<Restaurant> repository) : base(repository)
        {
        }
    }

}
