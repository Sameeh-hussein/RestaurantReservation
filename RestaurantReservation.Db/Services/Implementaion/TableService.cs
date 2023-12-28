using RestaurantReservation.Db.Models;
using RestaurantReservation.Db.Repositories;

namespace RestaurantReservation.Db.Services.Implementaion
{
    public class TableService : Service<Table>
    {
        public TableService(IRepository<Table> repository) : base(repository)
        {
        }
    }

}
