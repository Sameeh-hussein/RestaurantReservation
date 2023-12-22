using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RestaurantReservation.Db.Models;
using RestaurantReservation.Db.Repositories;

namespace RestaurantReservation.Services
{
    public class MenuItemsService
    {
        private readonly IMenuItemsRepository _menuItemsRepository;

        public MenuItemsService(IMenuItemsRepository menuItemsRepository)
        {
            _menuItemsRepository = menuItemsRepository ?? throw new ArgumentNullException(nameof(menuItemsRepository));
        }

        public async Task<MenuItems> CreateMenuItem(MenuItems menuItems)
        {
            if (menuItems == null)
                throw new ArgumentNullException(nameof(menuItems), "Menu item cannot be null.");

            return await _menuItemsRepository.Create(menuItems);
        }

        public async Task<MenuItems> UpdateMenuItem(MenuItems menuItems)
        {
            if (menuItems == null)
                throw new ArgumentNullException(nameof(menuItems), "Menu item cannot be null.");

            return await _menuItemsRepository.Update(menuItems);
        }

        public async Task<bool> DeleteMenuItem(int menuItemsId)
        {
            return await _menuItemsRepository.Delete(menuItemsId);
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
