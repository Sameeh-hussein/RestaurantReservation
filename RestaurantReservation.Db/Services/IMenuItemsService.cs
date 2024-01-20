using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Services
{
    public interface IMenuItemsService 
    {
        Task<IEnumerable<MenuItems>> GetAllAsync();
        Task<MenuItems?> GetByIdAsync(int id);
        Task<MenuItems> CreateAsync(MenuItems menuItems);
        Task<bool> DeleteAsync(int Id);
        Task<MenuItems> UpdateAsync(MenuItems menuItems);

        Task<List<MenuItems>> ListOrderedMenuItemsAsync(int reservationId);
        Task ListOrdersAndMenuItemsAsync(int reservationId);
    }
}