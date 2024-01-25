using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Services
{
    public interface IReservationService
    {
        Task<IEnumerable<Reservation>> GetAllAsync();
        Task<Reservation?> GetByIdAsync(int id);
        Task<Reservation> CreateAsync(Reservation reservation);
        Task DeleteAsync(Reservation reservation);
        Task<Reservation> UpdateAsync(Reservation reservation);
        Task<List<Reservation>> GetReservationsByCustomerAsync(int id);
        Task<bool> ReservationExist(int reservationId);
    }
}
