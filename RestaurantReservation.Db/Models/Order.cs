namespace RestaurantReservation.Db.Models
{
    public class Order
    {
        public int orderId { get; set; }
        public DateTime orderDate { get; set; }
        public decimal totalAmount { get; set; }

        public int reservationId { get; set; }
        public Reservation reservation { get; set; }

        public int employeeId { get; set; }
        public Employee employee { get; set; }

        public List<OrderItems> OrderItems { get; set; }
    }

}
