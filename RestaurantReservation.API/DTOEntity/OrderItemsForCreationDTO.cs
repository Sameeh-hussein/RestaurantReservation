using RestaurantReservation.Db.Models;

namespace RestaurantReservation.API.DTOEntity
{
    public class OrderItemsForCreationDTO
    {
        public int quantity { get; set; }
        public int menuItemId { get; set; }
    }
}
