using AutoMapper;
using Castle.Core.Resource;
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
    /// <summary>
    /// API endpoints for managing restaurants
    /// </summary>>
     
    [Authorize]
    [Route("api/restaurants")]
    [ApiController]
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantService _restaurantService;
        private readonly IMapper _mapper;
        private readonly ILogger<RestaurantController> _logger;

        public RestaurantController(
            IRestaurantService restaurantService, 
            IMapper mapper,
            ILogger<RestaurantController> logger)
        {
            _restaurantService = restaurantService ?? 
                throw new ArgumentNullException(nameof(restaurantService));
            _mapper = mapper ?? 
                throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? 
                throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Retrieves all restaurants.
        /// </summary>
        /// <returns>
        /// ActionResult representing a collection of restaurant data transfer objects (DTOs).
        /// </returns>
        /// <response code="200">Returns the collection of restaurants.</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RestaurantDTO>>> GetAllRestaurants()
        {
            _logger.LogInformation("GetAllRestaurants Start Getting all restaurants");

            var restaurants = await _restaurantService.GetAllAsync();
            var result = _mapper.Map<IEnumerable<RestaurantDTO>>(restaurants);

            _logger.LogInformation($"GetAllRestaurants retrieved {restaurants.Count()} restaurants successfully.");

            return Ok(result);
        }

        /// <summary>
        /// Retrieves a restaurant by its ID.
        /// </summary>
        /// <param name="restaurantid">The ID of the restaurant to retrieve.</param>
        /// <returns>
        /// ActionResult representing the restaurant data transfer object (DTO) with the specified ID.
        /// </returns>
        /// <response code="200">Returns the restaurant with the specified ID.</response>
        /// <response code="404">If the restaurant with the given ID is not found.</response>
        [HttpGet("{restaurantid}", Name = "GetRestaurantById")]
        public async Task<ActionResult<CustomerDTO>> GetRestaurantById(int restaurantid)
        {
            _logger.LogInformation($"GetRestaurantById started for restaurant with ID: {restaurantid}.");

            var restaurant = await _restaurantService.GetByIdAsync(restaurantid);
            if (restaurant == null)
            {
                _logger.LogWarning($"Restaurant with ID {restaurantid} not found.");
                return NotFound();
            }

            var result = _mapper.Map<RestaurantDTO>(restaurant);

            _logger.LogInformation($"Retrieved restaurant with ID: {restaurantid} successfully.");

            return Ok(result);
        }

        /// <summary>
        /// Adds a new restaurant.
        /// </summary>
        /// <param name="restaurantForCreationDTO">The data transfer object (DTO) containing information about the restaurant to be added.</param>
        /// <returns>
        /// ActionResult representing the newly added restaurant, along with a location header for accessing it.
        /// </returns>
        /// <response code="201">Returns the newly added restaurant and a location header for accessing it.</response>
        /// <response code="400">If the input data is invalid.</response>
        /// <response code="401">If the request is not authorized (user does not have the required role).</response>
        /// <response code="500">If an unexpected error occurs during restaurant addition.</response>
        [HttpPost]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult> AddRestaurant(RestaurantForCreationDTO restaurantForCreationDTO)
        {
            _logger.LogInformation("AddRestaurant attempting to add a new restaurant.");

            try
            {
                var restaurant = _mapper.Map<Restaurant>(restaurantForCreationDTO);

                await _restaurantService.CreateAsync(restaurant);

                var restaurantToReturn = _mapper.Map<RestaurantDTO>(restaurant);

                _logger.LogInformation($"New Restaurant with ID: {restaurantToReturn.RestaurantId} added successfully.");

                return CreatedAtRoute("GetRestaurantById",
                        new
                        {
                            restaurantid = restaurantToReturn.RestaurantId
                        },
                        restaurantToReturn
                    );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding a new restaurant.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        /// <summary>
        /// Updates a restaurant.
        /// </summary>
        /// <param name="restaurantid">The ID of the restaurant to be updated.</param>
        /// <param name="restaurantForUpdateDTO">The data transfer object (DTO) containing updated information for the restaurant.</param>
        /// <returns>
        /// ActionResult representing the result of the update operation.
        /// </returns>
        /// <response code="204">Indicates that the restaurant was successfully updated.</response>
        /// <response code="400">If the input data is invalid.</response>
        /// <response code="401">If the request is not authorized (user does not have the required role).</response>
        /// <response code="404">If the restaurant with the given ID is not found.</response>
        /// <response code="500">If an unexpected error occurs during restaurant update.</response>
        [HttpPut("{restaurantid}")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult> UpdateRestaurant(
            int restaurantid,
            RestaurantForUpdateDTO restaurantForUpdateDTO
            )
        {
            _logger.LogInformation($"UpdateRestaurant attempting to update restaurant with ID {restaurantid}.");

            try
            {
                var restaurant = await _restaurantService.GetByIdAsync(restaurantid);
                if (restaurant == null)
                {
                    _logger.LogWarning($"Restaurant with ID {restaurantid} not found.");
                    return NotFound();
                }

                _mapper.Map(restaurantForUpdateDTO, restaurant);
                await _restaurantService.UpdateAsync(restaurant);

                _logger.LogInformation($"Restaurant with ID {restaurantid} updated successfully.");

                return NoContent();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while updating restaurant with ID {restaurantid}.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        /// <summary>
        /// Deletes a restaurant.
        /// </summary>
        /// <param name="restaurantid">The ID of the restaurant to be deleted.</param>
        /// <returns>
        /// ActionResult representing the result of the delete operation.
        /// </returns>
        /// <response code="204">Indicates that the restaurant was successfully deleted.</response>
        /// <response code="401">If the request is not authorized (user does not have the required role).</response>
        /// <response code="404">If the restaurant with the given ID is not found.</response>
        /// <response code="500">If an unexpected error occurs during restaurant deletion.</response>
        [HttpDelete("{restaurantid}")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult> DeleteRestaurant(int restaurantid)
        {
            _logger.LogInformation($"DeleteRestaurant attempting to delete restaurant with ID {restaurantid}.");

            try
            {
                var resturant = await _restaurantService.GetByIdAsync(restaurantid);
                if (resturant == null)
                {
                    _logger.LogWarning($"Restaurant with ID {restaurantid} not found. Unable to delete.");
                    return NotFound();
                }

                await _restaurantService.DeleteAsync(resturant);

                _logger.LogInformation($"Restaurant with ID {restaurantid} deleted successfully.");

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while deleting restaurant with ID {restaurantid}.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
    }
}
