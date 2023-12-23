using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Repositories
{
    public interface IReservationRepository: IRepository<Reservation>
    {
        Task<List<Reservation>> GetReservationsByCustomer(int id);
    }
}