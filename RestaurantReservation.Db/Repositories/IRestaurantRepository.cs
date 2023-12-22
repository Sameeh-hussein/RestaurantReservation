using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Repositories
{
    public interface IRestaurantRepository
    {
        Task<Restaurant> Create(Restaurant restaurant);
        Task<bool> Delete(int id);
        Task<Restaurant> Update(Restaurant restaurant);
    }
}