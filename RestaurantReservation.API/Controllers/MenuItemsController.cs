using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantReservation.API.DTOEntity;
using RestaurantReservation.Db.Models;
using RestaurantReservation.Db.Services;
using System.ComponentModel.Design;

namespace RestaurantReservation.API.Controllers
{
    [Route("api/restaurants/{restaurantid}/menuitems")]
    [ApiController]
    public class MenuItemsController : ControllerBase
    {
        private readonly IMenuItemsService _menuItemsService;
        private readonly IRestaurantService _restaurantService;
        private readonly IMapper _mapper;

        public MenuItemsController(IMenuItemsService menuItemsService, IRestaurantService restaurantService ,IMapper mapper)
        {
            _menuItemsService = menuItemsService ?? throw new ArgumentNullException(nameof(menuItemsService));
            _restaurantService = restaurantService ?? throw new ArgumentNullException(nameof(restaurantService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MenuItemsDTO>>> GetAllMenuItems(int restaurantid)
        {
            if(! await _restaurantService.RestaurantExist(restaurantid))
            {
                return NotFound();
            }

            var menuItems = await _menuItemsService.GetMenuItemsInRestaurantAsync(restaurantid);
            if(menuItems == null)
            {
                return NotFound();
            }

            var menuItemsToReturn = _mapper.Map<IEnumerable<MenuItemsDTO>>(menuItems);

            return Ok(menuItemsToReturn);
        }

        [HttpGet("{menuitemsid}", Name = "GetMenuItemById")]
        public async Task<ActionResult<MenuItemsDTO>> GetMenuItemById(int restaurantid, int menuitemsid)
        {
            if (!await _restaurantService.RestaurantExist(restaurantid))
            {
                return NotFound();
            }

            var menuItem = await _menuItemsService.GetMenuItemInRestaurantAsync(restaurantid, menuitemsid);
            if(menuItem == null)
            {
                return NotFound();
            }

            var menuItemToReturn = _mapper.Map<MenuItemsDTO>(menuItem);

            return Ok(menuItemToReturn);
        }

        [HttpPost]
        public async Task<ActionResult> CreatMenuItemInRestaurant(
                int restaurantid, 
                MenuItemsForCreationDTO menuItemsForCreationDTO
            )
        {
            if (!await _restaurantService.RestaurantExist(restaurantid))
            {
                return NotFound();
            }

            var menuItem = _mapper.Map<MenuItems>(menuItemsForCreationDTO);

            await _menuItemsService.CreateAsync(restaurantid, menuItem);

            var menuItemToReturn = _mapper.Map<MenuItemsDTO>(menuItem);

            return CreatedAtRoute("GetMenuItemById",
                    new
                    {
                        restaurantid = restaurantid,
                        menuitemsid = menuItemToReturn.menuItemId
                    },
                    menuItemToReturn
                );
        }

        [HttpPut("{menuitemid}")]
        public async Task<ActionResult> UpdateMenuItemInRestaurant(
                int restaurantid,
                int menuitemid,
                MenuItemsForUpdateDTO menuItemsForUpdateDTO
            )
        {
            if (!await _restaurantService.RestaurantExist(restaurantid))
            {
                return NotFound();
            }

            var menuItem = await _menuItemsService.GetMenuItemInRestaurantAsync(restaurantid, menuitemid);
            if(menuItem == null)
            {
                return NotFound();
            }

            _mapper.Map(menuItemsForUpdateDTO, menuItem);

            await _menuItemsService.UpdateAsync(menuItem);

            return NoContent();
        }

        [HttpDelete("{menuitemid}")]
        public async Task<ActionResult> DeleteMenuItemInResturant(int restaurantid, int menuitemid)
        {
            if (!await _restaurantService.RestaurantExist(restaurantid))
            {
                return NotFound();
            }

            var menuItem = await _menuItemsService.GetMenuItemInRestaurantAsync(restaurantid, menuitemid);
            if(menuItem == null)
            {
                return NotFound();
            }

            await _menuItemsService.DeleteAsync(menuItem);

            return NoContent();
        }
    }
}
