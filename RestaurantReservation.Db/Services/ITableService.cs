using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Services
{
    public interface ITableService
    {
        Task<IEnumerable<Table>> GetAllTablesInRestaurantAsync(int restaurantId);
        Task<Table?> GetTableByIdInRestaurantAsync(int restaurantId, int tableId);
        Task<Table> CreateAsync(Table table);
        Task<bool> DeleteAsync(int Id);
        Task<Table> UpdateAsync(Table table);
    }
}
