using Table = RestaurantReservation.Db.Models.Table;

namespace RestaurantReservation.Db.Repositories.Implementaion
{
    public class TableRepository : Repository<Table>
    {
        public TableRepository(RestaurantReservationDbContext context) : base(context)
        {
        }
    }
}
