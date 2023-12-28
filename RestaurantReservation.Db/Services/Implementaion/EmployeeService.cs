using RestaurantReservation.Db.Models;
using RestaurantReservation.Db.Repositories;
using RestaurantReservation.Db.Repositories.Implementaion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReservation.Db.Services.Implementaion
{
    public class EmployeeService : Service<Employee>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
            : base(employeeRepository) // Ensure the base constructor is called
        {
            _employeeRepository = _employeeRepository ?? throw new ArgumentNullException(nameof(employeeRepository));
        }

        public async Task<decimal> CalculateAverageOrderAmount(int employeeId)
        {
            return await _employeeRepository.CalculateAverageOrderAmount(employeeId);
        }

        public async Task ListOrderedMenuItems()
        {
            await _employeeRepository.ListManagers();
        }
    }

}
