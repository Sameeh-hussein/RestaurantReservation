namespace RestaurantReservation.Db.Models
{
    public class MenuItems
    {
        public int itemId { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public decimal price { get; set; }

        public int restaurantId { get; set; }
        public Restaurant Restaurant { get; set; }

        public List<OrderItems> OrderItems { get; set; }
    }

}
