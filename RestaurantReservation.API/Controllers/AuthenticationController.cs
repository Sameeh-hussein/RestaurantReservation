using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RestaurantReservation.Db.Models;
using RestaurantReservation.API.Authentication.DTO;
using Microsoft.AspNetCore.Identity;
using AutoMapper;

namespace RestaurantReservation.API.Controllers
{
    [Route("api")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserValidation _userValidation;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public AuthenticationController(IUserValidation userValidation, 
                                        IJwtTokenGenerator jwtTokenGenerator,
                                        UserManager<ApplicationUser> userManager,
                                        IMapper mapper)
        {
            _userValidation = userValidation ?? throw new ArgumentNullException(nameof(userValidation));
            _jwtTokenGenerator = jwtTokenGenerator ?? throw new ArgumentNullException(nameof(jwtTokenGenerator));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpPost]
        [Route("login")]
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

        [HttpPost]
        [Route("regester")]
        public async Task<IActionResult> regester([FromBody] RegesterRequestDTO regesterRequestDTO)
        {
            var user = await _userManager.FindByNameAsync(regesterRequestDTO.UserName);
            if (user != null)
            {
                return Conflict("User Already Exist !!");
            }

            var applicationUser = _mapper.Map<ApplicationUser>(regesterRequestDTO);

            var createdUser = await _userManager.CreateAsync(applicationUser, regesterRequestDTO.Password);

            if(!createdUser.Succeeded)
            {
                return BadRequest(createdUser.Errors);
            }

            return Ok(StatusCodes.Status201Created);
        }
    }
}
