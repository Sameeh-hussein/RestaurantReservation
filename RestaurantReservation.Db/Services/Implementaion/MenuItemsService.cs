using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RestaurantReservation.Db.Models;
using RestaurantReservation.Db.Repositories;

namespace RestaurantReservation.Db.Services.Implementaion
{
    public class MenuItemsService : IMenuItemsService
    {
        private readonly IMenuItemsRepository _repository;

        public MenuItemsService(IMenuItemsRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<MenuItems>> GetMenuItemsInRestaurantAsync(int restaurantid)
        {
            return await _repository.GetMenuItemsInRestaurantAsync(restaurantid);
        }

        public async Task<MenuItems?> GetMenuItemInRestaurantAsync(int restaurantId, int menuItemId)
        {
            return await _repository.GetMenuItemInRestaurantAsync(restaurantId, menuItemId);
        }

        public async Task<MenuItems> CreateAsync(int restaurantId, MenuItems menuItems)
        {
            if (menuItems == null)
                throw new ArgumentNullException(nameof(menuItems), "cannot be null.");

            return await _repository.CreateAsync(restaurantId, menuItems);
        }

        public async Task<MenuItems> UpdateAsync(MenuItems menuItems)
        {
            if (menuItems == null)
                throw new ArgumentNullException(nameof(menuItems), "cannot be null.");

            return await _repository.UpdateAsync(menuItems);
        }

        public async Task DeleteAsync(MenuItems menuItems)
        {
            await _repository.DeleteAsync(menuItems);
        }

        public async Task<List<MenuItems>> ListOrderedMenuItemsAsync(int Id)
        {
            return await _repository.ListOrderedMenuItemsAsync(Id);
        }

        public async Task ListOrdersAndMenuItemsAsync(int reservationId)
        {
            await _repository.ListOrdersAndMenuItemsAsync(reservationId);
        }
    }
}
