using AutoMapper;
using RestaurantReservation.API.DTOEntity;
using RestaurantReservation.Db.Models;

namespace RestaurantReservation.API.Mapper
{
    public class TableProfile : Profile
    {
        public TableProfile()
        {
            CreateMap<Table, TableDTO>();
            CreateMap<TableForCreationDTO, Table>();
            CreateMap<TableForUpdateDTO, Table>();
        }
    }
}
