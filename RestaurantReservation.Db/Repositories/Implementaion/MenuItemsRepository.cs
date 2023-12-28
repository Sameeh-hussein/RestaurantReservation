using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Repositories.Implementaion
{
    public class MenuItemsRepository : Repository<MenuItems>
    {
        private readonly RestaurantReservationDbContext _Context;

        public MenuItemsRepository(RestaurantReservationDbContext context) : base(context) 
        {
            _Context = context;
        }

        public async Task<MenuItems> GetMenuItemById(int menuItemsId)
        {
            return await _Context.MenuItems.FindAsync(menuItemsId);
        }

        public async Task<List<MenuItems>> ListOrderedMenuItems(int reservationId)
        {
            var reservation = await _Context.Reservations
                                    .Include(r => r.orders)
                                        .ThenInclude(o => o.OrderItems)
                                            .ThenInclude(oi => oi.menuItems)
                                    .FirstOrDefaultAsync(r => r.reservationId == reservationId);

            if (reservation == null)
            {
                return new List<MenuItems>();
            }

            return reservation.orders
                              .SelectMany(o => o.OrderItems
                                    .Select(oi => oi.menuItems))
                              .ToList();
        }

        public async Task ListOrdersAndMenuItems(int reservationId)
        {
            var reservation = await _Context.Reservations
                                    .Include(r => r.orders)
                                        .ThenInclude(o => o.OrderItems)
                                            .ThenInclude(oi => oi.menuItems)
                                    .FirstOrDefaultAsync(r => r.reservationId == reservationId);

            if (reservation == null)
            {
                return;
            }

            foreach (var order in reservation.orders)
            {
                Console.WriteLine($"Order id : {order.orderId}, its menu items id :");
                foreach (var orderItem in order.OrderItems)
                {
                    Console.WriteLine(orderItem.menuItems.menuItemId);
                }
            }
        }
    }
}
