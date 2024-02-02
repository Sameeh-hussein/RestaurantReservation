
namespace RestaurantReservation.Db.Models
{
    public interface IJwtTokenGenerator
    {
        string? generateToken(ApplicationUser applicationUser, IList<string> userRoles);
    }
}