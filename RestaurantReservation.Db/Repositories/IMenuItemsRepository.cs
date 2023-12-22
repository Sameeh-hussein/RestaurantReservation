using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Repositories
{
    public interface IMenuItemsRepository
    {
        Task<MenuItems> Create(MenuItems menuItems);
        Task<bool> Delete(int id);
        Task<List<MenuItems>> ListOrderedMenuItems(int reservationId);
        Task ListOrdersAndMenuItems(int reservationId);
        Task<MenuItems> Update(MenuItems menuItems);
    }
}