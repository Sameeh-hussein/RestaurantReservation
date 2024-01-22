using AutoMapper;
using RestaurantReservation.API.DTOEntity;
using RestaurantReservation.Db.Models;

namespace RestaurantReservation.API.Mapper
{
    public class RestaurantProfile : Profile
    {
        public RestaurantProfile()
        {
            CreateMap<Restaurant, RestaurantDTO>();
            CreateMap<RestaurantForCreationDTO, Restaurant>();
            CreateMap<RestaurantForUpdateDTO, Restaurant>();
        }
    }

}
