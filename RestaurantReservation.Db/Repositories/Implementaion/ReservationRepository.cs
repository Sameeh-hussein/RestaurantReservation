using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Repositories.Implementaion
{
    public class ReservationRepository : Repository<Reservation>
    {
        private readonly RestaurantReservationDbContext _Context;

        public ReservationRepository(RestaurantReservationDbContext context) : base(context) 
        {
            _Context = context;
        }

        public async Task<List<Reservation>> GetReservationsByCustomer(int id)
        {
            return await _Context.Reservations.Where(r => r.customerId == id)
                                              .ToListAsync();
        }
    }
}
