using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Repositories
{
    public interface IEmployeeRepository
    {
        Task<decimal> CalculateAverageOrderAmount(int employeeId);
        Task<Employee> Create(Employee employee);
        Task<bool> Delete(int id);
        Task<List<Employee>> ListManagers();
        Task<Employee> Update(Employee employee);
    }
}