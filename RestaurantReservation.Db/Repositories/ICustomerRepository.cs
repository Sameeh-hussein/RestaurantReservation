using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Repositories
{
    public interface ICustomerRepository
    {
        Task<Customer> Create(Customer customer);
        Task<bool> Delete(int id);
        Task<Customer> Update(Customer customer);
    }
}