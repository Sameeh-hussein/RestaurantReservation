using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Services
{
    public interface IMenuItemsService 
    {
        Task<IEnumerable<MenuItems>> GetMenuItemsInRestaurantAsync(int restaurantid);
        Task<MenuItems?> GetMenuItemInRestaurantAsync(int restaurantId, int menuItemId);
        Task<MenuItems> CreateAsync(int restaurantId, MenuItems menuItems);
        Task DeleteAsync(MenuItems menuItems);
        Task<MenuItems> UpdateAsync(MenuItems menuItems);
        Task<List<MenuItems>> ListOrderedMenuItemsAsync(int reservationId);
        Task ListOrdersAndMenuItemsAsync(int reservationId);
    }
}