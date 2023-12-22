using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Repositories.Implementaion
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly RestaurantReservationDbContext _Context;

        public ReservationRepository(RestaurantReservationDbContext context)
        {
            _Context = context;
        }

        public async Task<Reservation> Create(Reservation reservation)
        {
            _Context.Reservations.Add(reservation);
            await _Context.SaveChangesAsync();
            return reservation;
        }

        public async Task<Reservation> Update(Reservation reservation)
        {
            _Context.Reservations.Update(reservation);
            await _Context.SaveChangesAsync();
            return reservation;
        }

        public async Task<bool> Delete(int id)
        {
            var reservation = await _Context.Reservations.FindAsync(id);
            if (reservation != null)
            {
                _Context.Reservations.Remove(reservation);
                await _Context.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<List<Reservation>> GetReservationsByCustomer(int id)
        {
            return await _Context.Reservations.Where(r => r.customerId == id)
                                              .ToListAsync();
        }
    }
}
