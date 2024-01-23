namespace RestaurantReservation.API.DTOEntity
{
    public class ReservationForCreationDTO
    {
        public DateTime reservationDate { get; set; }
        public int partySize { get; set; }

        public int restaurantId { get; set; }
        public int tableId { get; set; }
        public int customerId { get; set; }
    }
}
