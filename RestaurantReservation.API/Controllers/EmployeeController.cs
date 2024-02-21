using AutoMapper;
using Castle.Core.Resource;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantReservation.API.Authentication;
using RestaurantReservation.API.DTOEntity;
using RestaurantReservation.Db.Models;
using RestaurantReservation.Db.Services;

namespace RestaurantReservation.API.Controllers
{
    /// <summary>
    /// API endpoints for managing employees.
    /// </summary>

    [Authorize]
    [Route("api/employees")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly IMapper _mapper;
        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(
            IEmployeeService employeeService,
            IMapper mapper,
            ILogger<EmployeeController> logger)
        {
            _employeeService = employeeService ??
                throw new ArgumentNullException(nameof(employeeService));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
            _logger = logger ??
                throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Retrieves all employees.
        /// </summary>
        /// <returns>
        /// ActionResult representing a collection of employee data transfer objects (DTOs).
        /// </returns>
        /// <response code="200">Returns the collection of employees.</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDTO>>> GetAllEmployees()
        {
            _logger.LogInformation("GetAllEmployees Start Getting all employees");

            var employees = await _employeeService.GetAllAsync();

            var employeesToReturn = _mapper.Map<IEnumerable<EmployeeDTO>>(employees);

            _logger.LogInformation($"GetAllEmployees retrieved {employees.Count()} employees successfully.");

            return Ok(employeesToReturn);
        }

        /// <summary>
        /// Retrieves all manager employees.
        /// </summary>
        /// <returns>
        /// ActionResult representing a collection of manager employee data transfer objects (DTOs).
        /// </returns>
        /// <response code="200">Returns the collection of manager employees.</response>
        [HttpGet("manager")]
        public async Task<ActionResult<IEnumerable<EmployeeDTO>>> GetAllManagerEmployees()
        {
            try
            {
                _logger.LogInformation("Attempting to retrieve all manager employees.");

                var employees = await _employeeService.ListManagersAsync();

                var employeesToReturn = _mapper.Map<IEnumerable<EmployeeDTO>>(employees);

                _logger.LogInformation($"Retrieved {employees.Count()} manager employees successfully.");

                return Ok(employeesToReturn);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving manager employees.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        /// <summary>
        /// Retrieves a specific employee by ID.
        /// </summary>
        /// <param name="employeeid">The ID of the employee to retrieve.</param>
        /// <returns>
        /// ActionResult representing the employee data transfer object (DTO) with the specified ID.
        /// </returns>
        /// <response code="200">Returns the employee with the specified ID.</response>
        /// <response code="404">If the employee with the given ID is not found.</response>
        [HttpGet("{employeeid}", Name ="GetEmployeeById")]
        public async Task<ActionResult<EmployeeDTO>> GetEmployeeById(int employeeid)
        {
            _logger.LogInformation($"GetEmployeeById started for employee with ID: {employeeid}.");

            var employee = await _employeeService.GetByIdAsync(employeeid);
            if(employee == null)
            {
                _logger.LogWarning($"Employee with ID {employeeid} not found.");
                return NotFound();
            }

            var employeeToReturn = _mapper.Map<EmployeeDTO>(employee);

            _logger.LogInformation($"Retrieved employee with ID: {employeeid} successfully.");

            return Ok(employeeToReturn);
        }

        /// <summary>
        /// Retrieves the average order amount for a specific employee.
        /// </summary>
        /// <param name="employeeid">The ID of the employee.</param>
        /// <returns>
        /// ActionResult representing the average order amount for the employee.
        /// </returns>
        /// <response code="200">Returns the average order amount for the employee.</response>
        /// <response code="404">If the employee with the given ID is not found.</response>
        [HttpGet("{employeeid}/avareg-order-amount")]
        public async Task<ActionResult<decimal>> GetEmployeeAvaregOrderAmount(int employeeid)
        {
            _logger.LogInformation($"GetEmployeeAverageOrderAmount started for employee with ID: {employeeid}.");

            var employee = await _employeeService.GetByIdAsync(employeeid);
            if (employee == null)
            {
                _logger.LogWarning($"Employee with ID {employeeid} not found.");
                return NotFound();
            }

            var result = await _employeeService.CalculateAverageOrderAmountAsync(employeeid);

            _logger.LogInformation($"Calculated average order amount for employee with ID: {employeeid} as {result}.");

            return Ok(result);
        }

        /// <summary>
        /// Adds a new employee.
        /// </summary>
        /// <param name="employeeForCreationDTO">The data transfer object (DTO) containing information about the employee to be added.</param>
        /// <returns>
        /// ActionResult representing the result of the add operation.
        /// </returns>
        /// <response code="201">Indicates that the employee was successfully added.</response>
        /// <response code="400">If the input data is invalid.</response>
        /// <response code="401">If the request is not authorized (user does not have the required role).</response>
        /// <response code="500">If an unexpected error occurs during employee addition.</response>
        [HttpPost]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult> AddEmployee(EmployeeForCreationDTO employeeForCreationDTO)
        {
            _logger.LogInformation("AddEmployee attempting to add a new employee.");

            try
            {
                var employee = _mapper.Map<Employee>(employeeForCreationDTO);

                await _employeeService.CreateAsync(employee);

                var employeeToReturn = _mapper.Map<EmployeeDTO>(employee);

                _logger.LogInformation($"New Employee with ID: {employeeToReturn.employeeId} added successfully.");

                return CreatedAtRoute("GetEmployeeById",
                        new
                        {
                            employeeid = employeeToReturn.employeeId
                        },
                        employeeToReturn
                    );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding a new employee.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        /// <summary>
        /// Updates an employee.
        /// </summary>
        /// <param name="employeeid">The ID of the employee to be updated.</param>
        /// <param name="employeeForUpdateDTO">The data transfer object (DTO) containing updated information for the employee.</param>
        /// <returns>
        /// ActionResult representing the result of the update operation.
        /// </returns>
        /// <response code="204">Indicates that the employee was successfully updated.</response>
        /// <response code="400">If the input data is invalid.</response>
        /// <response code="401">If the request is not authorized (user does not have the required role).</response>
        /// <response code="404">If the employee with the given ID is not found.</response>
        /// <response code="500">If an unexpected error occurs during employee update.</response>
        [HttpPut("{employeeid}")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult> UpdateEmployee(
            int employeeid,
            EmployeeForUpdateDTO employeeForUpdateDTO)
        {
            _logger.LogInformation($"UpdateEmployee attempting to update employee with ID {employeeid}.");

            var employee = await _employeeService.GetByIdAsync(employeeid);
            if(employee == null)
            {
                _logger.LogWarning($"Employee with ID {employeeid} not found.");
                return NotFound();
            }

            try
            {
                _mapper.Map(employeeForUpdateDTO, employee);

                await _employeeService.UpdateAsync(employee);

                _logger.LogInformation($"Employee with ID {employeeid} updated successfully.");

                return NoContent();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while updating employee with ID {employeeid}.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        /// <summary>
        /// Deletes an employee.
        /// </summary>
        /// <param name="employeeid">The ID of the employee to be deleted.</param>
        /// <returns>
        /// ActionResult representing the result of the delete operation.
        /// </returns>
        /// <response code="204">Indicates that the employee was successfully deleted.</response>
        /// <response code="401">If the request is not authorized (user does not have the required role).</response>
        /// <response code="404">If the employee with the given ID is not found.</response>
        /// <response code="500">If an unexpected error occurs during employee deletion.</response>
        [HttpDelete("{employeeid}")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult> DeleteEmployee(int employeeid)
        {
            _logger.LogInformation($"DeleteEmployee attempting to delete employee with ID {employeeid}.");

            var employee = await _employeeService.GetByIdAsync(employeeid);
            if(employee == null)
            {
                _logger.LogWarning($"Employee with ID {employeeid} not found. Unable to delete.");
                return NotFound();
            }

            try
            {
                await _employeeService.DeleteAsync(employee);

                _logger.LogInformation($"employee with ID {employeeid} deleted successfully.");

                return NoContent();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while deleting employee with ID {employeeid}.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
    }
}
