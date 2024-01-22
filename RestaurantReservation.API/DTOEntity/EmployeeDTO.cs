namespace RestaurantReservation.API.DTOEntity
{
    public class EmployeeDTO
    {
        public int employeeId { get; set; }
        public string firstName { get; set; } = string.Empty;
        public string lastName { get; set; } = string.Empty;
        public string position { get; set; } = string.Empty;
        public int restaurantId { get; set; }
    }
}
