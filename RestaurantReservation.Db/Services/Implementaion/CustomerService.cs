using RestaurantReservation.Db.Models;
using RestaurantReservation.Db.Repositories;

namespace RestaurantReservation.Db.Services.Implementaion
{
    public class CustomerService : Service<Customer>
    {
        public CustomerService(IRepository<Customer> repository) : base(repository)
        {
        }
    }

}
