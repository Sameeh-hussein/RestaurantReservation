using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using RestaurantReservation.API.Authentication;
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

        [HttpGet("{tableid}", Name = "GetTableById")]
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


        [HttpPost]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult> CreatTableInRestaurant(
                int restaurantid,
                TableForCreationDTO tableForCreationDTO
            )
        {
            _logger.LogInformation($"CreatTableInRestaurant attempting to add a new table in restaurant ID: {restaurantid}");

            try
            {
                if (!await _restaurantService.RestaurantExist(restaurantid))
                {
                    _logger.LogWarning($"Restaurant with ID: {restaurantid} not found.");
                    return NotFound();
                }

                var table = _mapper.Map<Table>(tableForCreationDTO);

                await _tableService.CreateAsync(restaurantid, table);

                var tableToReturn = _mapper.Map<TableDTO>(table);

                _logger.LogInformation($"Successfully created table with ID: {table.tableId} in restaurant ID: {restaurantid}.");

                return CreatedAtRoute("GetTableById",
                        new { restaurantid = restaurantid,
                              tableid = tableToReturn.tableId 
                            },
                        tableToReturn);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding a new table.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpPut("{tableid}")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult> UpdateTableInRestaurant(
        int restaurantid,
        int tableid,
        TableForUpdateDTO tableForUpdateDTO
    )
        {
            _logger.LogInformation($"UpdateTableInRestaurant attempting to update table with ID: {tableid} in restaurant ID: {restaurantid}");

            try
            {
                if (!await _restaurantService.RestaurantExist(restaurantid))
                {
                    _logger.LogWarning($"Restaurant with ID: {restaurantid} not found.");
                    return NotFound();
                }

                var table = await _tableService.GetTableByIdInRestaurantAsync(restaurantid, tableid);
                if (table == null)
                {
                    _logger.LogWarning($"Table with ID: {tableid} in restaurant ID: {restaurantid} not found.");
                    return NotFound();
                }

                _mapper.Map(tableForUpdateDTO, table);

                await _tableService.UpdateAsync(table);

                _logger.LogInformation($"Successfully updated table with ID: {tableid} in restaurant ID: {restaurantid}.");

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while updating table with ID {tableid}.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpDelete("{tableid}")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult> DeleteTableInResturant(int restaurantid, int tableid)
        {
            _logger.LogInformation($"DeleteTableInResturant attempting to delete table with ID: {tableid} from restaurant ID: {restaurantid}");

            try
            {
                if (!await _restaurantService.RestaurantExist(restaurantid))
                {
                    _logger.LogWarning($"Restaurant with ID: {restaurantid} not found.");
                    return NotFound();
                }

                var table = await _tableService.GetTableByIdInRestaurantAsync(restaurantid, tableid);
                if (table == null)
                {
                    _logger.LogWarning($"Table with ID: {table} in restaurant ID: {restaurantid} not found.");
                    return NotFound();
                }

                await _tableService.DeleteAsync(table);

                _logger.LogInformation($"Successfully deleted table with ID: {tableid} from restaurant ID: {restaurantid}.");
                
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while deleting table with ID {tableid}.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
    }
}
