﻿using System.ComponentModel.DataAnnotations;

namespace RestaurantReservation.API.Authentication.DTO
{
    public class LoginRequestDTO
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
