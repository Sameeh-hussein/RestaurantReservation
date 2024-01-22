namespace RestaurantReservation.API.DTOEntity
{
    public class MenuItemsDTO
    {
        public int menuItemId { get; set; }
        public string name { get; set; } = string.Empty;
        public string description { get; set; } = string.Empty;
        public decimal price { get; set; }
        public int restaurantId { get; set; }
    }
}
