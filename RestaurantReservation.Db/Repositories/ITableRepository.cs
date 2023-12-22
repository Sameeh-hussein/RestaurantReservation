using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Repositories
{
    public interface ITableRepository
    {
        Task<Table> Create(Table table);
        Task<bool> Delete(int id);
        Task<Table> Update(Table table);
    }
}