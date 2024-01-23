using RestaurantReservation.Db.Models;

namespace RestaurantReservation.API.DTOEntity
{
    public class OrderItemsDTO
    {
        public int orderItemId { get; set; }
        public int quantity { get; set; }

        public int orderId { get; set; }
        public int menuItemId { get; set; }
    }
}
