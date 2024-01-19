using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Repositories
{
    public interface IReservationRepository
    {
        Task<IEnumerable<Reservation>> GetAllAsync();
        Task<Reservation?> GetByIdAsync(int id);
        Task<Reservation> CreateAsync(Reservation reservation);
        Task<bool> DeleteAsync(int id);
        Task<List<Reservation>> GetReservationsByCustomerAsync(int id);
        Task<Reservation> UpdateAsync(Reservation reservation);
    }
}