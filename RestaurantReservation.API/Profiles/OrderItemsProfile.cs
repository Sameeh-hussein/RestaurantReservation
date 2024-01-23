using AutoMapper;
using RestaurantReservation.API.DTOEntity;
using RestaurantReservation.Db.Models;

namespace RestaurantReservation.API.Mapper
{
    public class OrderItemsProfile : Profile
    {
        public OrderItemsProfile()
        {
            CreateMap<OrderItems, OrderItemsDTO>();
            CreateMap<OrderItemsForCreationDTO, OrderItems>();
            CreateMap<OrderItemsForUpdateDTO, OrderItems>();
        }
    }
}
