using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RestaurantReservation.Db;
using RestaurantReservation.Db.Repositories.Implementaion;
using System.Reflection;

namespace RestaurantReservation
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            using (var context = new RestaurantReservationDbContext())
            {
                var menuItemsRepository = new MenuItemsRepository(context);

                await menuItemsRepository.ListOrdersAndMenuItems(2);
            }
        }
    }
}
