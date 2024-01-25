using RestaurantReservation.Db.Models;
using RestaurantReservation.Db.Repositories;

namespace RestaurantReservation.Db.Services.Implementaion
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _repository;

        public ReservationService(IReservationRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Reservation>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Reservation?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<Reservation> CreateAsync(Reservation orderItems)
        {
            if (orderItems == null)
                throw new ArgumentNullException(nameof(orderItems), "cannot be null.");

            return await _repository.CreateAsync(orderItems);
        }

        public async Task<Reservation> UpdateAsync(Reservation orderItems)
        {
            if (orderItems == null)
                throw new ArgumentNullException(nameof(orderItems), "cannot be null.");

            return await _repository.UpdateAsync(orderItems);
        }

        public async Task DeleteAsync(Reservation reservation)
        {
            await _repository.DeleteAsync(reservation);
        }

        public async Task<List<Reservation>> GetReservationsByCustomerAsync(int reservationId)
        {
            return await _repository.GetReservationsByCustomerAsync(reservationId);
        }

        public async Task<bool> ReservationExist(int reservationId)
        {
            return await _repository.ReservationExist(reservationId);
        }
    }

}
