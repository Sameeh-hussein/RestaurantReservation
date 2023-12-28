using RestaurantReservation.Db.Models;
using RestaurantReservation.Db.Repositories;

namespace RestaurantReservation.Db.Services.Implementaion
{
    public class OrderService : Service<Order>
    {
        public OrderService(IRepository<Order> repository) : base(repository)
        {
        }
    }

}
