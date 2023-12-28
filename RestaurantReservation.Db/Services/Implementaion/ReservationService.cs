using RestaurantReservation.Db.Models;
using RestaurantReservation.Db.Repositories;

namespace RestaurantReservation.Db.Services.Implementaion
{
    public class ReservationService : Service<Reservation>
    {
        private readonly IReservationRepository _reservationRepository;

        public ReservationService(IReservationRepository reservationRepository)
            : base(reservationRepository) // Ensure the base constructor is called
        {
            _reservationRepository = _reservationRepository ?? throw new ArgumentNullException(nameof(reservationRepository));
        }

        public async Task<List<Reservation>> CalculateAverageOrderAmount(int reservationId)
        {
            return await _reservationRepository.GetReservationsByCustomer(reservationId);
        }
    }

}
