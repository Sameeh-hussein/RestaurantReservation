﻿namespace RestaurantReservation.API.DTOEntity
{
    public class EmployeeForCreationDTO
    {
        public string firstName { get; set; } = string.Empty;
        public string lastName { get; set; } = string.Empty;
        public string position { get; set; } = string.Empty;
        public int restaurantId { get; set; }
    }
}
