using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Repositories.Implementaion
{
    public class RestaurantRepository : IRestaurantRepository
    {
        private readonly RestaurantReservationDbContext _Context;

        public RestaurantRepository(RestaurantReservationDbContext context)
        {
            _Context = context;
        }

        public async Task<IEnumerable<Restaurant>> GetAllAsync()
        {
            return await _Context.Restaurants.ToListAsync();
        }

        public async Task<Restaurant?> GetByIdAsync(int id)
        {
            return await _Context.Restaurants.FirstOrDefaultAsync(c => c.RestaurantId == id);
        }

        public async Task<Restaurant> CreateAsync(Restaurant restaurant)
        {
            _Context.Restaurants.Add(restaurant);
            await _Context.SaveChangesAsync();
            return restaurant;
        }

        public async Task<Restaurant> UpdateAsync(Restaurant restaurant)
        {
            _Context.Restaurants.Update(restaurant);
            await _Context.SaveChangesAsync();
            return restaurant;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var restaurant = await _Context.Restaurants.FindAsync(id);
            if (restaurant != null)
            {
                _Context.Restaurants.Remove(restaurant);
                await _Context.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}