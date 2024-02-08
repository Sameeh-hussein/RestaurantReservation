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
