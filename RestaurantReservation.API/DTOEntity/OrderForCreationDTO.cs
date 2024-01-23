using RestaurantReservation.Db.Models;

namespace RestaurantReservation.API.DTOEntity
{
    public class OrderForCreationDTO
    {
        public DateTime orderDate { get; set; }
        public decimal totalAmount { get; set; }

        public int employeeId { get; set; }
    }
}
