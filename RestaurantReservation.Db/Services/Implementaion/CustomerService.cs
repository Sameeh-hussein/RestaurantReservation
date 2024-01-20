using RestaurantReservation.Db.Models;
using RestaurantReservation.Db.Repositories;

namespace RestaurantReservation.Db.Services.Implementaion
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _repository;

        public CustomerService(ICustomerRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Customer?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<Customer> CreateAsync(Customer customer)
        {
            if (customer == null)
                throw new ArgumentNullException(nameof(customer), "cannot be null.");

            return await _repository.CreateAsync(customer);
        }

        public async Task<Customer> UpdateAsync(Customer customer)
        {
            if (customer == null)
                throw new ArgumentNullException(nameof(customer), "cannot be null.");

            return await _repository.UpdateAsync(customer);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
    }

}
