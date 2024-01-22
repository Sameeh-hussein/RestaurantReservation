using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Db.Models;
using System.Reflection.Metadata.Ecma335;

namespace RestaurantReservation.Db.Repositories.Implementaion
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly RestaurantReservationDbContext _Context;

        public EmployeeRepository(RestaurantReservationDbContext context)
        {
            _Context = context;
        }

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            return await _Context.Employees.ToListAsync();
        }

        public async Task<Employee?> GetByIdAsync(int id)
        {
            return await _Context.Employees.FirstOrDefaultAsync(c => c.employeeId == id);
        }

        public async Task<Employee> CreateAsync(Employee employee)
        {
            _Context.Employees.Add(employee);
            await _Context.SaveChangesAsync();
            return employee;
        }

        public async Task<Employee> UpdateAsync(Employee employee)
        {
            _Context.Employees.Update(employee);
            await _Context.SaveChangesAsync();
            return employee;
        }

        public async Task DeleteAsync(Employee employee)
        {
            _Context.Employees.Remove(employee);
            await _Context.SaveChangesAsync();
        }

        public async Task<List<Employee>> ListManagersAsync()
        {
            return await _Context.Employees
                           .Where(x => x.position == "Manager")
                           .ToListAsync();
        }

        public async Task<decimal> CalculateAverageOrderAmountAsync(int employeeId)
        {
            var employee = await _Context.Employees
                                         .Include(e => e.orders)
                                         .FirstOrDefaultAsync(e => e.employeeId == employeeId);

            if (employee == null)
            {
                return -1.0m;
            }

            if (employee.orders == null || !employee.orders.Any())
            {
                return 0m;
            }

            return employee.orders
                           .Average(o => o.totalAmount);
        }
    }
}