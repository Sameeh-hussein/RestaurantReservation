using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Repositories
{
    public interface ITableRepository
    {
        Task<IEnumerable<Table>> GetAllTablesInRestaurantAsync(int restaurantId);
        Task<Table?> GetTableByIdInRestaurantAsync(int restaurantId, int tableId);
        Task<Table> CreateAsync(int restaurantId, Table table);
        Task DeleteAsync(Table table);
        Task<Table> UpdateAsync(Table table);
    }
}