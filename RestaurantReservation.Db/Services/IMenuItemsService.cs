using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Services
{
    public interface IMenuItemsService : IService<MenuItems>
    {
        Task<MenuItems> GetMenuItemById(int menuItemsId);
        Task<List<MenuItems>> ListOrderedMenuItems(int reservationId);
        Task ListOrdersAndMenuItems(int reservationId);
    }
}