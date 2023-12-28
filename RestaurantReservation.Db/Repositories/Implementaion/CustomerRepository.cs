using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Repositories.Implementaion
{
    public class CustomerRepository : Repository<Customer>
    {
        public CustomerRepository(RestaurantReservationDbContext context) : base(context)
        {
        }
    }
}
