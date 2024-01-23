using RestaurantReservation.Db.Models;

namespace RestaurantReservation.API.DTOEntity
{
    public class OrderItemsForUpdateDTO
    {
        public int quantity { get; set; }
        public int orderId { get; set; }
        public int menuItemId { get; set; }
    }
}
