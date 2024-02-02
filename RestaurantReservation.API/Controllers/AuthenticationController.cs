using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RestaurantReservation.Db.Models;
using RestaurantReservation.API.Authentication.DTO;

namespace RestaurantReservation.API.Controllers
{
    [Route("api")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserValidation _userValidation;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public AuthenticationController(IUserValidation userValidation, IJwtTokenGenerator jwtTokenGenerator)
        {
            _userValidation = userValidation ?? throw new ArgumentNullException(nameof(userValidation));
            _jwtTokenGenerator = jwtTokenGenerator ?? throw new ArgumentNullException(nameof(jwtTokenGenerator));
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginRequestDTO)
        {
            var user = await _userValidation.Validate(loginRequestDTO.UserName, loginRequestDTO.Password);
            if (user == null)
            {
                return Unauthorized();
            }
            var applicationUser = user.Value.Item1;
            var rolesList = user.Value.Item2;

            var token = _jwtTokenGenerator.generateToken(applicationUser, rolesList);

            if(token.IsNullOrEmpty())
            {
                return Unauthorized();
            }

            return Ok(token);
        }
    }
}
