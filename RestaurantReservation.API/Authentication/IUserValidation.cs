
namespace RestaurantReservation.Db.Models
{
    public interface IUserValidation
    {
        Task<(ApplicationUser, IList<string>)?> Validate(string userName, string password);
    }
}