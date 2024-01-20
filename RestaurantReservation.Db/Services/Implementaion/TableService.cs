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

        public async Task<IEnumerable<Table>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Table?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<Table> CreateAsync(Table Table)
        {
            if (Table == null)
                throw new ArgumentNullException(nameof(Table), "cannot be null.");

            return await _repository.CreateAsync(Table);
        }

        public async Task<Table> UpdateAsync(Table Table)
        {
            if (Table == null)
                throw new ArgumentNullException(nameof(Table), "cannot be null.");

            return await _repository.UpdateAsync(Table);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
    }

}
