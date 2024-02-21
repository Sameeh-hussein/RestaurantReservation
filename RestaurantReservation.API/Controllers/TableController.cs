using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using RestaurantReservation.API.DTOEntity;
using RestaurantReservation.Db.Models;
using RestaurantReservation.Db.Services;
using RestaurantReservation.Db.Services.Implementaion;

namespace RestaurantReservation.API.Controllers
{
    [Route("api/restaurants/{restaurantid}/tables")]
    [ApiController]
    public class TableController : ControllerBase
    {
        private readonly ITableService _tableService;
        private readonly IRestaurantService _restaurantService;
        private readonly IMapper _mapper;
        private readonly ILogger<TableController> _logger;

        public TableController(
            ITableService tableService,
            IRestaurantService restaurantService,
            IMapper mapper,
            ILogger<TableController> logger
            )
        {
            _tableService = tableService ??
                throw new ArgumentNullException(nameof(tableService));
            _restaurantService = restaurantService ??
                throw new ArgumentNullException(nameof(restaurantService));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
            _logger = logger ??
                throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TableDTO>>> GetAllTablesInRestaurant(int restaurantid)
        {
            _logger.LogInformation($"GetAllTabelsInRestaurant attempting to retrieve all tables for restaurant ID: {restaurantid}.");

            if (!await _restaurantService.RestaurantExist(restaurantid))
            {
                _logger.LogWarning($"Restaurant with ID: {restaurantid} not found.");
                return NotFound();
            }

            var tables = await _tableService.GetAllTablesInRestaurantAsync(restaurantid);

            var tablesToReturn = _mapper.Map<IEnumerable<TableDTO>>(tables);

            _logger.LogInformation($"Successfully retrieved {tables.Count()} tables for restaurant ID: {restaurantid}.");

            return Ok(tablesToReturn);
        }

        [HttpGet("{tableid}")]
        public async Task<ActionResult<TableDTO>> GetTableById(int restaurantid, int tableid)
        {
            _logger.LogInformation($"GetTableById starting to get table with ID: {tableid} for restaurant ID: {restaurantid}");

            if (!await _restaurantService.RestaurantExist(restaurantid))
            {
                _logger.LogWarning($"Restaurant with ID: {restaurantid} not found.");
                return NotFound();
            }

            var table = await _tableService.GetTableByIdInRestaurantAsync(restaurantid, tableid);
            if (table == null)
            {
                _logger.LogWarning($"Table with ID: {tableid} for restaurant ID: {restaurantid} not found.");
                return NotFound();
            }

            var tableToReturn = _mapper.Map<TableDTO>(table);

            _logger.LogInformation($"Successfully retrieved table with ID: {tableid} for restaurant ID: {restaurantid}.");

            return Ok(tableToReturn);
        }
    }
}
