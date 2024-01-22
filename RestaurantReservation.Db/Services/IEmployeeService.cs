using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Services
{
    public interface IEmployeeService
    {
        Task<IEnumerable<Employee>> GetAllAsync();
        Task<Employee?> GetByIdAsync(int id);
        Task<Employee> CreateAsync(Employee employee);
        Task DeleteAsync(Employee employee);
        Task<Employee> UpdateAsync(Employee employee);
        Task<decimal> CalculateAverageOrderAmountAsync(int employeeId);
        Task<List<Employee>> ListManagersAsync();
    }
}
