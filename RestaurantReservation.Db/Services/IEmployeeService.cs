using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Services
{
    public interface IEmployeeService
    {
        Task<IEnumerable<Employee>> GetAllAsync();
        Task<Employee?> GetByIdAsync(int id);
        Task<Employee> CreateAsync(Employee employee);
        Task<bool> DeleteAsync(int Id);
        Task<Employee> UpdateAsync(Employee employee);
        Task<decimal> CalculateAverageOrderAmountAsync(int employeeId);
        Task<List<Employee>> ListManagersAsync();
    }
}
