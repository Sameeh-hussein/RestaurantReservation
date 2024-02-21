using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Repositories
{
    public interface ITableRepository
    {
        Task<IEnumerable<Table>> GetAllTablesInRestaurantAsync(int restaurantId);
        Task<Table?> GetTableByIdInRestaurantAsync(int restaurantId, int tableId);
        Task<Table> CreateAsync(Table table);
        Task<bool> DeleteAsync(int id);
        Task<Table> UpdateAsync(Table table);
    }
}