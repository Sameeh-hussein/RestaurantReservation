using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Repositories
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetAllAsync();
        Task<Employee?> GetByIdAsync(int id);
        Task<decimal> CalculateAverageOrderAmountAsync(int employeeId);
        Task<Employee> CreateAsync(Employee employee);
        Task DeleteAsync(Employee employee);
        Task<List<Employee>> ListManagersAsync();
        Task<Employee> UpdateAsync(Employee employee);
    }
}