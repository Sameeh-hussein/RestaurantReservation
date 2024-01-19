using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Db.Models;
using Table = RestaurantReservation.Db.Models.Table;

namespace RestaurantReservation.Db.Repositories.Implementaion
{
    public class TableRepository : ITableRepository
    {
        private readonly RestaurantReservationDbContext _Context;

        public TableRepository(RestaurantReservationDbContext context)
        {
            _Context = context;
        }

        public async Task<IEnumerable<Table>> GetAllAsync()
        {
            return await _Context.Tables.ToListAsync();
        }

        public async Task<Table?> GetByIdAsync(int id)
        {
            return await _Context.Tables.FirstOrDefaultAsync(c => c.tableId == id);
        }

        public async Task<Table> CreateAsync(Table table)
        {
            _Context.Tables.Add(table);
            await _Context.SaveChangesAsync();
            return table;
        }

        public async Task<Table> UpdateAsync(Table table)
        {
            _Context.Tables.Update(table);
            await _Context.SaveChangesAsync();
            return table;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var table = await _Context.Tables.FindAsync(id);
            if (table != null)
            {
                _Context.Tables.Remove(table);
                await _Context.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}