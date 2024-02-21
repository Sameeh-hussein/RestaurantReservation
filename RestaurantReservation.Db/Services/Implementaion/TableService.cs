using RestaurantReservation.Db.Models;
using RestaurantReservation.Db.Repositories;

namespace RestaurantReservation.Db.Services.Implementaion
{
    public class TableService : ITableService
    {
        private readonly ITableRepository _repository;

        public TableService(ITableRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Table>> GetAllTablesInRestaurantAsync(int restaurantId)
        {
            return await _repository.GetAllTablesInRestaurantAsync(restaurantId);
        }

        public async Task<Table?> GetTableByIdInRestaurantAsync(int restaurantId, int tableId)
        {
            return await _repository.GetTableByIdInRestaurantAsync(restaurantId, tableId);
        }

        public async Task<Table> CreateAsync(int restaurantId, Table table)
        {
            if (table == null)
                throw new ArgumentNullException(nameof(table), "cannot be null.");

            return await _repository.CreateAsync(restaurantId, table);
        }

        public async Task<Table> UpdateAsync(Table Table)
        {
            if (Table == null)
                throw new ArgumentNullException(nameof(Table), "cannot be null.");

            return await _repository.UpdateAsync(Table);
        }

        public async Task DeleteAsync(Table table)
        {
            await _repository.DeleteAsync(table);
        }
    }

}
