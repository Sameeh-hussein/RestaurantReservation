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

        public async Task<Employee> Create(Employee employee)
        {
            _Context.Employees.Add(employee);
            await _Context.SaveChangesAsync();
            return employee;
        }

        public async Task<Employee> Update(Employee employee)
        {
            _Context.Employees.Update(employee);
            await _Context.SaveChangesAsync();
            return employee;
        }

        public async Task<bool> Delete(int id)
        {
            var employee = await _Context.Employees.FindAsync(id);
            if (employee != null)
            {
                _Context.Employees.Remove(employee);
                await _Context.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<List<Employee>> ListManagers()
        {
            return await _Context.Employees
                           .Where(x => x.position == "Manager")
                           .ToListAsync();
        }

        public async Task<decimal> CalculateAverageOrderAmount(int employeeId)
        {
            var employee = await _Context.Employees
                                         .Include(e => e.orders)
                                         .FirstOrDefaultAsync(e => e.employeeId == employeeId);

            if (employee == null)
            {
                return -1.0m;
            }

            return employee.orders
                           .Average(o => o.totalAmount);
        }
    }
}
