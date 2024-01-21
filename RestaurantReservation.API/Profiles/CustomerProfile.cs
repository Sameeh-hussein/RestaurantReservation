using AutoMapper;
using RestaurantReservation.API.DTOEntity;
using RestaurantReservation.Db.Models;

namespace RestaurantReservation.API.Mapper
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<Customer, CustomerDTO>();
            CreateMap<CustomerForCreationDTO, Customer>();
            CreateMap<CustomerForUpdateDTO, Customer>();
        }
    }
}
