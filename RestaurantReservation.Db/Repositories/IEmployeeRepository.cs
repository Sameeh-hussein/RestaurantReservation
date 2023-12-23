using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Repositories
{
    public interface IEmployeeRepository: IRepository<Employee>
    {
        Task<decimal> CalculateAverageOrderAmount(int employeeId);
        Task<List<Employee>> ListManagers();
    }
}