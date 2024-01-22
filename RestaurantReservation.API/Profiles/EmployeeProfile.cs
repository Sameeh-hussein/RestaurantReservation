using AutoMapper;
using RestaurantReservation.API.DTOEntity;
using RestaurantReservation.Db.Models;

namespace RestaurantReservation.API.Mapper
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<Employee, EmployeeDTO>();
            CreateMap<EmployeeForCreationDTO, Employee>();
            CreateMap<EmployeeForUpdateDTO, Employee>();
        }
    }
}
