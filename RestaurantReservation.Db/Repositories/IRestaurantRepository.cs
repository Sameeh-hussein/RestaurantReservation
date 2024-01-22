using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Repositories
{
    public interface IRestaurantRepository
    {
        Task<IEnumerable<Restaurant>> GetAllAsync();
        Task<Restaurant?> GetByIdAsync(int id);
        Task<Restaurant> CreateAsync(Restaurant restaurant);
        Task DeleteAsync(Restaurant restaurant);
        Task<Restaurant> UpdateAsync(Restaurant restaurant);
    }
}