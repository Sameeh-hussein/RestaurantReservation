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
    /// <summary>
    /// API endpoints for managing tables
    /// </summary>>
    
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

        /// <summary>
        /// Retrieves all tables in a restaurant by restaurant ID.
        /// </summary>
        /// <param name="restaurantid">The ID of the restaurant.</param>
        /// <returns>
        /// A collection of table data transfer objects (DTOs) representing the tables in the restaurant.
        /// </returns>
        /// <response code="200">Returns the collection of tables in the restaurant.</response>
        /// <response code="404">If the restaurant with the given ID is not found.</response>
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

        /// <summary>
        /// Retrieves a table by its ID within a specific restaurant.
        /// </summary>
        /// <param name="restaurantid">The ID of the restaurant containing the table.</param>
        /// <param name="tableid">The ID of the table to retrieve.</param>
        /// <returns>
        /// An ActionResult containing the table data transfer object (DTO) representing the table with the specified ID.
        /// </returns>
        /// <response code="200">Returns the table with the specified ID.</response>
        /// <response code="404">If the restaurant or table with the given IDs is not found.</response>
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

        /// <summary>
        /// Creates a new table in a restaurant.
        /// </summary>
        /// <param name="restaurantid">The ID of the restaurant where the table will be created.</param>
        /// <param name="tableForCreationDTO">The data transfer object (DTO) containing information about the table to be created.</param>
        /// <returns>
        /// ActionResult representing the newly created table, along with a location header for accessing it.
        /// </returns>
        /// <response code="201">Returns the newly created table and a location header for accessing it.</response>
        /// <response code="400">If the input data is invalid.</response>
        /// <response code="401">If the request is not authorized (user does not have the required role).</response>
        /// <response code="404">If the restaurant with the given ID is not found.</response>
        /// <response code="500">If an unexpected error occurs during table creation.</response>
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

        /// <summary>
        /// Updates a table within a restaurant.
        /// </summary>
        /// <param name="restaurantid">The ID of the restaurant containing the table to be updated.</param>
        /// <param name="tableid">The ID of the table to be updated.</param>
        /// <param name="tableForUpdateDTO">The data transfer object (DTO) containing the updated information for the table.</param>
        /// <returns>
        /// ActionResult representing the result of the update operation.
        /// </returns>
        /// <response code="204">Indicates that the table was successfully updated.</response>
        /// <response code="400">If the input data is invalid.</response>
        /// <response code="401">If the request is not authorized (user does not have the required role).</response>
        /// <response code="404">If the restaurant or table with the given IDs is not found.</response>
        /// <response code="500">If an unexpected error occurs during table update.</response>
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

        /// <summary>
        /// Deletes a table from a restaurant.
        /// </summary>
        /// <param name="restaurantid">The ID of the restaurant from which the table will be deleted.</param>
        /// <param name="tableid">The ID of the table to be deleted.</param>
        /// <returns>
        /// ActionResult representing the result of the delete operation.
        /// </returns>
        /// <response code="204">Indicates that the table was successfully deleted.</response>
        /// <response code="401">If the request is not authorized (user does not have the required role).</response>
        /// <response code="404">If the restaurant or table with the given IDs is not found.</response>
        /// <response code="500">If an unexpected error occurs during table deletion.</response>
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
