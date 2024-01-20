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

        public async Task<IEnumerable<MenuItems>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<MenuItems?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<MenuItems> CreateAsync(MenuItems menuItems)
        {
            if (menuItems == null)
                throw new ArgumentNullException(nameof(menuItems), "cannot be null.");

            return await _repository.CreateAsync(menuItems);
        }

        public async Task<MenuItems> UpdateAsync(MenuItems menuItems)
        {
            if (menuItems == null)
                throw new ArgumentNullException(nameof(menuItems), "cannot be null.");

            return await _repository.UpdateAsync(menuItems);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
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
