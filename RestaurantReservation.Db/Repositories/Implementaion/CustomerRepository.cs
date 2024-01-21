using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Repositories.Implementaion
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly RestaurantReservationDbContext _Context;

        public CustomerRepository(RestaurantReservationDbContext context)
        {
            _Context = context;
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            return await _Context.Customers.ToListAsync();
        }

        public async Task<Customer?> GetByIdAsync(int id)
        {
            return await _Context.Customers.FirstOrDefaultAsync(c => c.customerId == id);
        }

        public async Task<Customer> CreateAsync(Customer customer)
        {
            _Context.Customers.Add(customer);
            await _Context.SaveChangesAsync();
            return customer;
        }

        public async Task<Customer> UpdateAsync(Customer customer)
        {
            _Context.Customers.Update(customer);
            await _Context.SaveChangesAsync();
            return customer;
        }

        public async Task DeleteAsync(Customer customer)
        {
            _Context.Customers.Remove(customer);
            await _Context.SaveChangesAsync();
        }
    }
}