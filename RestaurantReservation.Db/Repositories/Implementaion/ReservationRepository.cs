using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Db.Models;
using RestaurantReservation.Db.Services.Implementaion;

namespace RestaurantReservation.Db.Repositories.Implementaion
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly RestaurantReservationDbContext _Context;

        public ReservationRepository(RestaurantReservationDbContext context)
        {
            _Context = context;
        }

        public async Task<IEnumerable<Reservation>> GetAllAsync()
        {
            return await _Context.Reservations.ToListAsync();
        }

        public async Task<Reservation?> GetByIdAsync(int id)
        {
            return await _Context.Reservations.FirstOrDefaultAsync(c => c.reservationId == id);
        }

        public async Task<Reservation> CreateAsync(Reservation reservation)
        {
            _Context.Reservations.Add(reservation);
            await _Context.SaveChangesAsync();
            return reservation;
        }

        public async Task<Reservation> UpdateAsync(Reservation reservation)
        {
            _Context.Reservations.Update(reservation);
            await _Context.SaveChangesAsync();
            return reservation;
        }

        public async Task DeleteAsync(Reservation reservation)
        {
            _Context.Reservations.Remove(reservation);
            await _Context.SaveChangesAsync();
        }

        public async Task<List<Reservation>> GetReservationsByCustomerAsync(int id)
        {
            return await _Context.Reservations.Where(r => r.customerId == id)
                                              .ToListAsync();
        }
    }
}