using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Repositories
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetAllAsync();
        Task<Employee?> GetByIdAsync(int id);
        Task<decimal> CalculateAverageOrderAmountAsync(int employeeId);
        Task<Employee> CreateAsync(Employee employee);
        Task<bool> DeleteAsync(int id);
        Task<List<Employee>> ListManagersAsync();
        Task<Employee> UpdateAsync(Employee employee);
    }
}