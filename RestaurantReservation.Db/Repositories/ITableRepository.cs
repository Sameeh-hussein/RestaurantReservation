using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Repositories
{
    public interface ITableRepository
    {
        Task<IEnumerable<Table>> GetAllAsync();
        Task<Table?> GetByIdAsync(int id);
        Task<Table> CreateAsync(Table table);
        Task<bool> DeleteAsync(int id);
        Task<Table> UpdateAsync(Table table);
    }
}