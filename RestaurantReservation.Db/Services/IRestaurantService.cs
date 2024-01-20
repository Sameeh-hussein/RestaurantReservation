using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Services
{
    public interface IRestaurantService
    {
        Task<IEnumerable<Restaurant>> GetAllAsync();
        Task<Restaurant?> GetByIdAsync(int id);
        Task<Restaurant> CreateAsync(Restaurant restaurant);
        Task<bool> DeleteAsync(int Id);
        Task<Restaurant> UpdateAsync(Restaurant restaurant);
    }
}
