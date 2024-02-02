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
    [ApiController]
    [Authorize]
    [Route("api/customers")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly IMapper _mapper;

        public CustomerController(ICustomerService customerService, IMapper mapper)
        {
            _customerService = customerService ?? throw new ArgumentNullException(nameof(customerService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerDTO>>> GetAllCustomers()
        {
            var customers = await _customerService.GetAllAsync();
            var result = _mapper.Map<IEnumerable<CustomerDTO>>(customers);
            return Ok(result);
        }

        [HttpGet("{customerid}", Name ="GetCustomerById")]
        public async Task<ActionResult<CustomerDTO>> GetCustomerById(int customerid)
        {
            var customer = await _customerService.GetByIdAsync(customerid);
            if(customer == null)
            {
                return NotFound();
            }

            var result = _mapper.Map<CustomerDTO>(customer);
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult> AddCustomer(CustomerForCreationDTO customerForCreationDTO)
        {
            var customer = _mapper.Map<Customer>(customerForCreationDTO);

            await _customerService.CreateAsync(customer);

            var customerToReturn = _mapper.Map<CustomerDTO>(customer);

            return CreatedAtRoute("GetCustomerById",
                    new
                    {
                        customerid = customerToReturn.customerId
                    },
                    customerToReturn
                );
        }

        [HttpPut("{customerid}")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult> UpdateCustomer(
            int customerid,
            CustomerForUpdateDTO customerForUpdateDTO
            )
        {
            var customer = await _customerService.GetByIdAsync(customerid);
            if(customer == null )
            {
                return NotFound();
            }

            _mapper.Map(customerForUpdateDTO, customer);

            await _customerService.UpdateAsync(customer);

            return NoContent();
        }

        [HttpDelete("{customerid}")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult> DeleteCustomer(int customerid)
        {
            var customer = await _customerService.GetByIdAsync(customerid);
            if(customer == null )
            {
                return NotFound();
            }

            await _customerService.DeleteAsync(customer);

            return NoContent();
        }
    }
}
