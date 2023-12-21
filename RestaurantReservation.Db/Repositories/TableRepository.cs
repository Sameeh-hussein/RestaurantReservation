using Table = RestaurantReservation.Db.Models.Table;

namespace RestaurantReservation.Db.Repositories
{
    public class TableRepository : IRepository<Table>
    {
        private readonly RestaurantReservationDbContext _Context;

        public TableRepository(RestaurantReservationDbContext context)
        {
            _Context = context;
        }

        public async Task<Table> Create(Table table)
        {
            _Context.Tables.Add(table);
            await _Context.SaveChangesAsync();
            return table;
        }

        public async Task<Table> Update(Table table)
        {
            _Context.Tables.Update(table);
            await _Context.SaveChangesAsync();
            return table;
        }

        public async Task<bool> Delete(int id)
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
