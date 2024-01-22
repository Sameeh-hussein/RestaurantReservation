using RestaurantReservation.Db.Models;
using RestaurantReservation.Db.Repositories;

namespace RestaurantReservation.Db.Services.Implementaion
{
    public class RestaurantService : IRestaurantService
    {
        private readonly IRestaurantRepository _repository;

        public RestaurantService(IRestaurantRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Restaurant>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Restaurant?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<Restaurant> CreateAsync(Restaurant restaurant)
        {
            if (restaurant == null)
                throw new ArgumentNullException(nameof(restaurant), "cannot be null.");

            return await _repository.CreateAsync(restaurant);
        }

        public async Task<Restaurant> UpdateAsync(Restaurant restaurant)
        {
            if (restaurant == null)
                throw new ArgumentNullException(nameof(restaurant), "cannot be null.");

            return await _repository.UpdateAsync(restaurant);
        }

        public async Task DeleteAsync(Restaurant restaurant)
        {
            await _repository.DeleteAsync(restaurant);
        }
    }

}
