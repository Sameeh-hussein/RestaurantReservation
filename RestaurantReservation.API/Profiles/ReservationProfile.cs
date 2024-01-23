using AutoMapper;
using RestaurantReservation.API.DTOEntity;
using RestaurantReservation.Db.Models;

namespace RestaurantReservation.API.Mapper
{
    public class ReservationProfile : Profile
    {
        public ReservationProfile()
        {
            CreateMap<Reservation, ReservationDTO>();
            CreateMap<ReservationForCreationDTO, Reservation>();
            CreateMap<ReservationForUpdateDTO, Reservation>();
        }
    }
}
