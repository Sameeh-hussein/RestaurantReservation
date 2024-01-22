namespace RestaurantReservation.API.DTOEntity
{
    public class MenuItemsForCreationDTO
    {
        public string name { get; set; } = string.Empty;
        public string description { get; set; } = string.Empty;
        public decimal price { get; set; }
    }
}
