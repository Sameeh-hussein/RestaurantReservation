using Microsoft.AspNetCore.Identity;

namespace RestaurantReservation.Db.Models
{
    public class UserValidation : IUserValidation
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserValidation(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        public async Task<(ApplicationUser, IList<string>)?> Validate(string userName, string password)
        {
            var user = await _userManager.FindByNameAsync(userName);

            if (user == null)
            {
                return null;
            }

            var passwordMatch = await _userManager.CheckPasswordAsync(user, password);

            if (!passwordMatch)
            {
                return null;
            }

            var userRoles = await _userManager.GetRolesAsync(user);

            return (user, userRoles);
        }
    }
}
