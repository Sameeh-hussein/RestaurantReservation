namespace RestaurantReservation.Db.Models
{
    public class OrderItems
    {
        public int orderItemId { get; set; }
        public int quantity { get; set; }

        public int orderId { get; set; }
        public MenuItems menuItems { get; set; }

        public int itemId { get; set; }
        public Order Order { get; set; }
    }

}
