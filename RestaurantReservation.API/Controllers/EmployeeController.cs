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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDTO>>> GetAllEmployees()
        {
            _logger.LogInformation("GetAllEmployees Start Getting all employees");

            var employees = await _employeeService.GetAllAsync();

            var employeesToReturn = _mapper.Map<IEnumerable<EmployeeDTO>>(employees);

            _logger.LogInformation($"GetAllEmployees retrieved {employees.Count()} employees successfully.");

            return Ok(employeesToReturn);
        }

        [HttpGet("manager")]
        public async Task<ActionResult<IEnumerable<EmployeeDTO>>> GetAllManagerEmployees()
        {
            var employees = await _employeeService.ListManagersAsync();

            var employeesToReturn = _mapper.Map<IEnumerable<EmployeeDTO>>(employees);

            return Ok(employeesToReturn);
        }

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
