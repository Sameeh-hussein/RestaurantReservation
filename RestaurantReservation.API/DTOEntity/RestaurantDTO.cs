namespace RestaurantReservation.API.DTOEntity
{
    public class RestaurantDTO
    {
        public int RestaurantId { get; set; }
        public string name { get; set; } = string.Empty;
        public string address { get; set; } = string.Empty;
        public string phoneNumber { get; set; } = string.Empty;
        public string openingHours { get; set; } = string.Empty;
    }
}
