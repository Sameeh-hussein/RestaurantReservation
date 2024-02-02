using AutoMapper;
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

        public EmployeeController(IEmployeeService employeeService, IMapper mapper)
        {
            _employeeService = employeeService ?? throw new ArgumentNullException(nameof(employeeService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDTO>>> GetAllEmployees()
        {
            var employees = await _employeeService.GetAllAsync();

            var employeesToReturn = _mapper.Map<IEnumerable<EmployeeDTO>>(employees);

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
            var employee = await _employeeService.GetByIdAsync(employeeid);
            if(employee == null)
            {
                return NotFound();
            }

            var employeeToReturn = _mapper.Map<EmployeeDTO>(employee);

            return Ok(employeeToReturn);
        }

        [HttpGet("{employeeid}/avareg-order-amount")]
        public async Task<ActionResult<decimal>> GetEmployeeAvaregOrderAmount(int employeeid)
        {
            var employee = await _employeeService.GetByIdAsync(employeeid);
            if (employee == null)
            {
                return NotFound();
            }

            var result = await _employeeService.CalculateAverageOrderAmountAsync(employeeid);

            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult> AddEmployee(EmployeeForCreationDTO employeeForCreationDTO)
        {
            var employee = _mapper.Map<Employee>(employeeForCreationDTO);

            await _employeeService.CreateAsync(employee);

            var employeeToReturn = _mapper.Map<EmployeeDTO>(employee);

            return CreatedAtRoute("GetEmployeeById",
                    new
                    {
                        employeeid = employeeToReturn.employeeId
                    },
                    employeeToReturn
                ) ;
        }

        [HttpPut("{employeeid}")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult> UpdateEmployee(
            int employeeid,
            EmployeeForUpdateDTO employeeForUpdateDTO)
        {
            var employee = await _employeeService.GetByIdAsync(employeeid);
            if(employee == null)
            {
                return NotFound();
            }

            _mapper.Map(employeeForUpdateDTO, employee);

            await _employeeService.UpdateAsync(employee);

            return NoContent();
        }

        [HttpDelete("{employeeid}")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult> DeleteEmployee(int employeeid)
        {
            var employee = await _employeeService.GetByIdAsync(employeeid);
            if(employee == null)
            {
                return NotFound();
            }

            await _employeeService.DeleteAsync(employee);

            return NoContent();
        }
    }
}
