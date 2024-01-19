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

        public async Task<IEnumerable<MenuItems>> GetAllAsync()
        {
            return await _Context.MenuItems.ToListAsync();
        }

        public async Task<MenuItems?> GetByIdAsync(int id)
        {
            return await _Context.MenuItems.FirstOrDefaultAsync(c => c.menuItemId == id);
        }

        public async Task<MenuItems> CreateAsync(MenuItems menuItems)
        {
            _Context.MenuItems.Add(menuItems);
            await _Context.SaveChangesAsync();
            return menuItems;
        }

        public async Task<MenuItems> UpdateAsync(MenuItems menuItems)
        {
            _Context.MenuItems.Update(menuItems);
            await _Context.SaveChangesAsync();
            return menuItems;
        }

        public async Task<bool> DeleteAsync(int id)
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

        public async Task<List<MenuItems>> ListOrderedMenuItemsAsync(int reservationId)
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

        public async Task ListOrdersAndMenuItemsAsync(int reservationId)
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