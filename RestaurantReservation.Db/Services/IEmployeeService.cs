using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Services
{
    public interface IEmployeeService : IService<Employee>
    {
        Task<decimal> CalculateAverageOrderAmount(int employeeId);
        Task<List<Employee>> ListManagers();
    }
}
