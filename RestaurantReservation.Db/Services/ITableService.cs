using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Services
{
    public interface ITableService
    {
        Task<IEnumerable<Table>> GetAllAsync();
        Task<Table?> GetByIdAsync(int id);
        Task<Table> CreateAsync(Table table);
        Task<bool> DeleteAsync(int Id);
        Task<Table> UpdateAsync(Table table);
    }
}
