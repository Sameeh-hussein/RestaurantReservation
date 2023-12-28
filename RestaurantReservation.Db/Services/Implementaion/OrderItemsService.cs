using RestaurantReservation.Db.Models;
using RestaurantReservation.Db.Repositories;

namespace RestaurantReservation.Db.Services.Implementaion
{
    public class OrderItemsService : Service<OrderItems>
    {
        public OrderItemsService(IRepository<OrderItems> repository) : base(repository)
        {
        }
    }

}
