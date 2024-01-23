using RestaurantReservation.Db.Models;

namespace RestaurantReservation.API.DTOEntity
{
    public class OrderForUpdateDTO
    {
        public DateTime orderDate { get; set; }
        public decimal totalAmount { get; set; }

        public int reservationId { get; set; }
        public int employeeId { get; set; }
    }
}
