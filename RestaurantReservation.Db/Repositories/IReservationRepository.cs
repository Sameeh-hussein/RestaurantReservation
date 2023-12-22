using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Repositories
{
    public interface IReservationRepository
    {
        Task<Reservation> Create(Reservation reservation);
        Task<bool> Delete(int id);
        Task<List<Reservation>> GetReservationsByCustomer(int id);
        Task<Reservation> Update(Reservation reservation);
    }
}