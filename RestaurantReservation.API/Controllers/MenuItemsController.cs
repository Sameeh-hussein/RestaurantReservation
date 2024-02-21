using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantReservation.API.Authentication;
using RestaurantReservation.API.DTOEntity;
using RestaurantReservation.Db.Models;
using RestaurantReservation.Db.Services;
using System.ComponentModel.Design;

namespace RestaurantReservation.API.Controllers
{
    /// <summary>
    /// API endpoints for managing menu items.
    /// </summary>

    [Authorize]
    [Route("api/restaurants/{restaurantid}/menuitems")]
    [ApiController]
    public class MenuItemsController : ControllerBase
    {
        private readonly IMenuItemsService _menuItemsService;
        private readonly IRestaurantService _restaurantService;
        private readonly IMapper _mapper;
        private readonly ILogger<MenuItemsController> _logger;

        public MenuItemsController(
            IMenuItemsService menuItemsService,
            IRestaurantService restaurantService,
            IMapper mapper,
            ILogger<MenuItemsController> logger)
        {
            _menuItemsService = menuItemsService ??
                throw new ArgumentNullException(nameof(menuItemsService));
            _restaurantService = restaurantService ??
                throw new ArgumentNullException(nameof(restaurantService));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
            _logger = logger ??
                throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Retrieves all menu items in a restaurant by restaurant ID.
        /// </summary>
        /// <param name="restaurantid">The ID of the restaurant.</param>
        /// <returns>
        /// ActionResult representing a collection of menu item data transfer objects (DTOs).
        /// </returns>
        /// <response code="200">Returns the collection of menu items in the restaurant.</response>
        /// <response code="404">If the restaurant with the given ID is not found.</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MenuItemsDTO>>> GetAllMenuItems(int restaurantid)
        {
            _logger.LogInformation($"Starting to get all menu items for restaurant ID: {restaurantid}");

            if (!await _restaurantService.RestaurantExist(restaurantid))
            {
                _logger.LogWarning($"Restaurant with ID: {restaurantid} not found.");
                return NotFound();
            }

            var menuItems = await _menuItemsService.GetMenuItemsInRestaurantAsync(restaurantid);
            if (menuItems == null)
            {
                _logger.LogWarning($"Menu items for restaurant ID: {restaurantid} not found.");
                return NotFound();
            }

            var menuItemsToReturn = _mapper.Map<IEnumerable<MenuItemsDTO>>(menuItems);

            _logger.LogInformation($"Successfully retrieved {menuItems.Count()} menu items for restaurant ID: {restaurantid}.");
            return Ok(menuItemsToReturn);
        }

        /// <summary>
        /// Retrieves a specific menu item within a restaurant by menu item ID.
        /// </summary>
        /// <param name="restaurantid">The ID of the restaurant.</param>
        /// <param name="menuitemsid">The ID of the menu item to retrieve.</param>
        /// <returns>
        /// ActionResult representing the menu item data transfer object (DTO) with the specified ID.
        /// </returns>
        /// <response code="200">Returns the menu item with the specified ID.</response>
        /// <response code="404">If the restaurant or menu item with the given IDs are not found.</response>
        [HttpGet("{menuitemsid}", Name = "GetMenuItemById")]
        public async Task<ActionResult<MenuItemsDTO>> GetMenuItemById(int restaurantid, int menuitemsid)
        {
            _logger.LogInformation($"Starting to get menu item with ID: {menuitemsid} for restaurant ID: {restaurantid}");

            if (!await _restaurantService.RestaurantExist(restaurantid))
            {
                _logger.LogWarning($"Restaurant with ID: {restaurantid} not found.");
                return NotFound();
            }

            var menuItem = await _menuItemsService.GetMenuItemInRestaurantAsync(restaurantid, menuitemsid);
            if (menuItem == null)
            {
                _logger.LogWarning($"Menu item with ID: {menuitemsid} for restaurant ID: {restaurantid} not found.");
                return NotFound();
            }

            var menuItemToReturn = _mapper.Map<MenuItemsDTO>(menuItem);

            _logger.LogInformation($"Successfully retrieved menu item with ID: {menuitemsid} for restaurant ID: {restaurantid}.");
            
            return Ok(menuItemToReturn);
        }

        /// <summary>
        /// Adds a new menu item to a restaurant.
        /// </summary>
        /// <param name="restaurantid">The ID of the restaurant.</param>
        /// <param name="menuItemsForCreationDTO">The data transfer object (DTO) containing information about the menu item to be added.</param>
        /// <returns>
        /// ActionResult representing the result of the add operation.
        /// </returns>
        /// <response code="201">Indicates that the menu item was successfully added to the restaurant.</response>
        /// <response code="400">If the input data is invalid.</response>
        /// <response code="401">If the request is not authorized (user does not have the required role).</response>
        /// <response code="404">If the restaurant with the given ID is not found.</response>
        /// <response code="500">If an unexpected error occurs during menu item addition.</response>
        [HttpPost]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult> CreatMenuItemInRestaurant(
                int restaurantid, 
                MenuItemsForCreationDTO menuItemsForCreationDTO
            )
        {
            _logger.LogInformation($"Attempting to add a new menu item in restaurant ID: {restaurantid}");

            try
            {
                if (!await _restaurantService.RestaurantExist(restaurantid))
                {
                    _logger.LogWarning($"Restaurant with ID: {restaurantid} not found.");
                    return NotFound();
                }

                var menuItem = _mapper.Map<MenuItems>(menuItemsForCreationDTO);

                await _menuItemsService.CreateAsync(restaurantid, menuItem);

                var menuItemToReturn = _mapper.Map<MenuItemsDTO>(menuItem);

                _logger.LogInformation($"Successfully created menu item with ID: {menuItem.menuItemId} in restaurant ID: {restaurantid}.");
                
                return CreatedAtRoute("GetMenuItemById",
                        new { restaurantid, menuitemsid = menuItemToReturn.menuItemId },
                        menuItemToReturn);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding a new menu item.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        /// <summary>
        /// Updates a menu item within a restaurant.
        /// </summary>
        /// <param name="restaurantid">The ID of the restaurant.</param>
        /// <param name="menuitemid">The ID of the menu item to be updated.</param>
        /// <param name="menuItemsForUpdateDTO">The data transfer object (DTO) containing updated information for the menu item.</param>
        /// <returns>
        /// ActionResult representing the result of the update operation.
        /// </returns>
        /// <response code="204">Indicates that the menu item was successfully updated.</response>
        /// <response code="400">If the input data is invalid.</response>
        /// <response code="401">If the request is not authorized (user does not have the required role).</response>
        /// <response code="404">If the restaurant or menu item with the given IDs are not found.</response>
        /// <response code="500">If an unexpected error occurs during menu item update.</response>
        [HttpPut("{menuitemid}")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult> UpdateMenuItemInRestaurant(
                int restaurantid,
                int menuitemid,
                MenuItemsForUpdateDTO menuItemsForUpdateDTO
            )
        {
            _logger.LogInformation($"Attempting to update menu item with ID: {menuitemid} in restaurant ID: {restaurantid}");

            try
            {
                if (!await _restaurantService.RestaurantExist(restaurantid))
                {
                    _logger.LogWarning($"Restaurant with ID: {restaurantid} not found.");
                    return NotFound();
                }

                var menuItem = await _menuItemsService.GetMenuItemInRestaurantAsync(restaurantid, menuitemid);
                if (menuItem == null)
                {
                    _logger.LogWarning($"Menu item with ID: {menuitemid} in restaurant ID: {restaurantid} not found.");
                    return NotFound();
                }

                _mapper.Map(menuItemsForUpdateDTO, menuItem);

                await _menuItemsService.UpdateAsync(menuItem);

                _logger.LogInformation($"Successfully updated menu item with ID: {menuitemid} in restaurant ID: {restaurantid}.");
                
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while updating menu item with ID {menuitemid}.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        /// <summary>
        /// Deletes a menu item from a restaurant.
        /// </summary>
        /// <param name="restaurantid">The ID of the restaurant.</param>
        /// <param name="menuitemid">The ID of the menu item to be deleted.</param>
        /// <returns>
        /// ActionResult representing the result of the delete operation.
        /// </returns>
        /// <response code="204">Indicates that the menu item was successfully deleted from the restaurant.</response>
        /// <response code="401">If the request is not authorized (user does not have the required role).</response>
        /// <response code="404">If the restaurant or menu item with the given IDs are not found.</response>
        /// <response code="500">If an unexpected error occurs during menu item deletion.</response>
        [HttpDelete("{menuitemid}")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult> DeleteMenuItemInResturant(int restaurantid, int menuitemid)
        {
            _logger.LogInformation($"Attempting to delete menu item with ID: {menuitemid} from restaurant ID: {restaurantid}");

            try
            {
                if (!await _restaurantService.RestaurantExist(restaurantid))
                {
                    _logger.LogWarning($"Restaurant with ID: {restaurantid} not found.");
                    return NotFound();
                }

                var menuItem = await _menuItemsService.GetMenuItemInRestaurantAsync(restaurantid, menuitemid);
                if (menuItem == null)
                {
                    _logger.LogWarning($"Menu item with ID: {menuitemid} in restaurant ID: {restaurantid} not found.");
                    return NotFound();
                }

                await _menuItemsService.DeleteAsync(menuItem);

                _logger.LogInformation($"Successfully deleted menu item with ID: {menuitemid} from restaurant ID: {restaurantid}.");
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while deleting menu item with ID {menuitemid}.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
    }
}
