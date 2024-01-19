using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Repositories
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> GetAllAsync();
        Task<Customer?> GetByIdAsync(int id);
        Task<Customer> CreateAsync(Customer customer);
        Task<bool> DeleteAsync(int id);
        Task<Customer> UpdateAsync(Customer customer);
    }
}