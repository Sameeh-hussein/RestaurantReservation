using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RestaurantReservation.Db.Models
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly IConfiguration _configuration;

        public JwtTokenGenerator(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public string? generateToken(ApplicationUser applicationUser, IList<string> userRoles)
        {
            // 1- Generate SecurityKey
            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["Jet:Key"]));

            // 2- Generate ClaimsArrey
            var authClaims = new List<Claim>()
            {
                new Claim("sub", applicationUser.Id),
                new Claim(ClaimTypes.Name, applicationUser.UserName)
            };

            foreach (var role in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            // 3- Generate new instance from jwtSecurityToken 
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                expires: DateTime.Now.AddDays(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256)
            );

            // 4- Generate Token string
            var tokenToReturn = new JwtSecurityTokenHandler().WriteToken(token);

            // 5- return token string

            return tokenToReturn;
        }
    }
}
