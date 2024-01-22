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
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _repository;


        public EmployeeService(IEmployeeRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Employee?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<Employee> CreateAsync(Employee employee)
        {
            if (employee == null)
                throw new ArgumentNullException(nameof(employee), "cannot be null.");

            return await _repository.CreateAsync(employee);
        }

        public async Task<Employee> UpdateAsync(Employee employee)
        {
            if (employee == null)
                throw new ArgumentNullException(nameof(employee), "cannot be null.");

            return await _repository.UpdateAsync(employee);
        }

        public async Task DeleteAsync(Employee employee)
        {
            await _repository.DeleteAsync(employee);
        }

        public async Task<decimal> CalculateAverageOrderAmountAsync(int employeeId)
        {
            return await _repository.CalculateAverageOrderAmountAsync(employeeId);
        }

        public async Task<List<Employee>> ListManagersAsync()
        {
            return await _repository.ListManagersAsync();
        }
    }

}
