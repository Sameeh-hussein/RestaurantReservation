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

        public async Task<IEnumerable<Table>> GetAllTablesInRestaurantAsync(int restaurantId)
        {
            return await _Context.Tables.Where(t => t.restaurantId == restaurantId)
                                        .ToListAsync();
        }

        public async Task<Table?> GetTableByIdInRestaurantAsync(int restaurantId, int tableId)
        {
            return await _Context.Tables.FirstOrDefaultAsync(c => c.tableId == tableId && c.restaurantId == restaurantId);
        }

        public async Task<Table> CreateAsync(int restaurantId, Table table)
        {
            _Context.Tables.Add(table);
            table.restaurantId = restaurantId;
            await _Context.SaveChangesAsync();
            return table;
        }

        public async Task<Table> UpdateAsync(Table table)
        {
            _Context.Tables.Update(table);
            await _Context.SaveChangesAsync();
            return table;
        }

        public async Task DeleteAsync(Table table)
        {
            _Context.Tables.Remove(table);
            await _Context.SaveChangesAsync();
        }
    }
}