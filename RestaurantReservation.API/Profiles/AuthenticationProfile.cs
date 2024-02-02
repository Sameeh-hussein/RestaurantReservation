using AutoMapper;
using RestaurantReservation.API.Authentication.DTO;
using RestaurantReservation.Db.Models;

namespace RestaurantReservation.API.Mapper
{
    public class AuthenticationProfile : Profile
    {
        public AuthenticationProfile()
        {
            CreateMap<RegesterRequestDTO, ApplicationUser>();
        }
    }
}
