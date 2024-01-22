using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Repositories
{
    public interface IMenuItemsRepository
    {
        Task<IEnumerable<MenuItems>> GetMenuItemsInRestaurantAsync(int restaurantId);
        Task<MenuItems?> GetMenuItemInRestaurantAsync(int restaurantId, int menuItemId);
        Task<MenuItems> CreateAsync(int restaurantId, MenuItems menuItems);
        Task DeleteAsync(MenuItems menuItems);
        Task<List<MenuItems>> ListOrderedMenuItemsAsync(int reservationId);
        Task ListOrdersAndMenuItemsAsync(int reservationId);
        Task<MenuItems> UpdateAsync(MenuItems menuItems);
    }
}