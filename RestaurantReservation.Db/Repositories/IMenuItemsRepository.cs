using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Repositories
{
    public interface IMenuItemsRepository: IRepository<MenuItems>
    {
        Task<MenuItems> GetMenuItemById(int menuItemsId);
        Task<List<MenuItems>> ListOrderedMenuItems(int reservationId);
        Task ListOrdersAndMenuItems(int reservationId);
    }
}