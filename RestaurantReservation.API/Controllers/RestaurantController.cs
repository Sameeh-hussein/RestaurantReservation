using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantReservation.API.Authentication;
using RestaurantReservation.API.DTOEntity;
using RestaurantReservation.Db.Models;
using RestaurantReservation.Db.Services;
using RestaurantReservation.Db.Services.Implementaion;

namespace RestaurantReservation.API.Controllers
{
    [Authorize]
    [Route("api/restaurants")]
    [ApiController]
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantService _restaurantService;
        private readonly IMapper _mapper;

        public RestaurantController(IRestaurantService restaurantService, IMapper mapper)
        {
            _restaurantService = restaurantService ?? throw new ArgumentNullException(nameof(restaurantService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RestaurantDTO>>> GetAllRestaurants()
        {
            var restaurants = await _restaurantService.GetAllAsync();
            var result = _mapper.Map<IEnumerable<RestaurantDTO>>(restaurants);
            return Ok(result);
        }

        [HttpGet("{restaurantid}", Name = "GetRestaurantById")]
        public async Task<ActionResult<CustomerDTO>> GetRestaurantById(int restaurantid)
        {
            var restaurant = await _restaurantService.GetByIdAsync(restaurantid);
            if (restaurant == null)
            {
                return NotFound();
            }

            var result = _mapper.Map<RestaurantDTO>(restaurant);
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult> AddRestaurant(RestaurantForCreationDTO restaurantForCreationDTO)
        {
            var restaurant = _mapper.Map<Restaurant>(restaurantForCreationDTO);

            await _restaurantService.CreateAsync(restaurant);

            var restaurantToReturn = _mapper.Map<RestaurantDTO>(restaurant);

            return CreatedAtRoute("GetRestaurantById",
                    new
                    {
                        restaurantid = restaurantToReturn.RestaurantId
                    },
                    restaurantToReturn
                );
        }

        [HttpPut("{restaurantid}")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult> UpdateRestaurant(
            int restaurantid,
            RestaurantForUpdateDTO restaurantForUpdateDTO
            )
        {
            var restaurant = await _restaurantService.GetByIdAsync(restaurantid);
            if (restaurant == null)
            {
                return NotFound();
            }

            _mapper.Map(restaurantForUpdateDTO, restaurant);

            await _restaurantService.UpdateAsync(restaurant);

            return NoContent();
        }

        [HttpDelete("{restaurantid}")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult> DeleteCustomer(int restaurantid)
        {
            var resturant = await _restaurantService.GetByIdAsync(restaurantid);
            if (resturant == null)
            {
                return NotFound();
            }

            await _restaurantService.DeleteAsync(resturant);

            return NoContent();
        }
    }
}
