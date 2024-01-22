using AutoMapper;
using RestaurantReservation.API.DTOEntity;
using RestaurantReservation.Db.Models;

namespace RestaurantReservation.API.Mapper
{
    public class MenuItemsProfile : Profile
    {
        public MenuItemsProfile()
        {
            CreateMap<MenuItems, MenuItemsDTO>();
            CreateMap<MenuItemsForCreationDTO, MenuItems>();
            CreateMap<MenuItemsForUpdateDTO, MenuItems>();
        }
    }
}
