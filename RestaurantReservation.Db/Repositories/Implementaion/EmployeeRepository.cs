using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Db.Models;
using System.Reflection.Metadata.Ecma335;

namespace RestaurantReservation.Db.Repositories.Implementaion
{
    public class EmployeeRepository : Repository<Employee>
    {
        private readonly RestaurantReservationDbContext _Context;

        public EmployeeRepository(RestaurantReservationDbContext context) : base(context) 
        {
            _Context = context;
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
