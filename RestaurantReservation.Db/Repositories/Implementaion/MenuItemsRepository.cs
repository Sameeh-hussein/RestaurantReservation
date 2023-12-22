using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Repositories.Implementaion
{
    public class MenuItemsRepository : IMenuItemsRepository
    {
        private readonly RestaurantReservationDbContext _Context;

        public MenuItemsRepository(RestaurantReservationDbContext context)
        {
            _Context = context;
        }

        public async Task<MenuItems> Create(MenuItems menuItems)
        {
            _Context.MenuItems.Add(menuItems);
            await _Context.SaveChangesAsync();
            return menuItems;
        }

        public async Task<MenuItems> Update(MenuItems menuItems)
        {
            _Context.MenuItems.Update(menuItems);
            await _Context.SaveChangesAsync();
            return menuItems;
        }

        public async Task<bool> Delete(int id)
        {
            var menuItems = await _Context.MenuItems.FindAsync(id);
            if (menuItems != null)
            {
                _Context.MenuItems.Remove(menuItems);
                await _Context.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
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
