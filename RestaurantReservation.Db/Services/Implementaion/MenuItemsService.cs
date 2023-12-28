using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RestaurantReservation.Db.Models;
using RestaurantReservation.Db.Repositories;

namespace RestaurantReservation.Db.Services.Implementaion
{
    public class MenuItemsService : Service<MenuItems>
    {
        private readonly IMenuItemsRepository _menuItemsRepository;

        public MenuItemsService(IMenuItemsRepository menuItemsRepository)
            : base(menuItemsRepository) // Ensure the base constructor is called
        {
            _menuItemsRepository = menuItemsRepository ?? throw new ArgumentNullException(nameof(menuItemsRepository));
        }

        public async Task<MenuItems> GetMenuItemById(int menuItemsId)
        {
            return await _menuItemsRepository.GetMenuItemById(menuItemsId);
        }

        public async Task<List<MenuItems>> ListOrderedMenuItems(int reservationId)
        {
            return await _menuItemsRepository.ListOrderedMenuItems(reservationId);
        }

        public async Task ListOrdersAndMenuItems(int reservationId)
        {
            await _menuItemsRepository.ListOrdersAndMenuItems(reservationId);
        }
    }
}
