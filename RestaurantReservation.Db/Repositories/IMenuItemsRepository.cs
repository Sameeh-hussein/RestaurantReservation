using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Repositories
{
    public interface IMenuItemsRepository
    {
        Task<IEnumerable<MenuItems>> GetAllAsync();
        Task<MenuItems?> GetByIdAsync(int id);
        Task<MenuItems> CreateAsync(MenuItems menuItems);
        Task<bool> DeleteAsync(int id);
        Task<List<MenuItems>> ListOrderedMenuItemsAsync(int reservationId);
        Task ListOrdersAndMenuItemsAsync(int reservationId);
        Task<MenuItems> UpdateAsync(MenuItems menuItems);
    }
}