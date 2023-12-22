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

        public async Task<Customer> Create(Customer customer)
        {
            _Context.Customers.Add(customer);
            await _Context.SaveChangesAsync();
            return customer;
        }

        public async Task<Customer> Update(Customer customer)
        {
            _Context.Customers.Update(customer);
            await _Context.SaveChangesAsync();
            return customer;
        }

        public async Task<bool> Delete(int id)
        {
            var customer = await _Context.Customers.FindAsync(id);
            if (customer != null)
            {
                _Context.Customers.Remove(customer);
                await _Context.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
