using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestaurantReservation.API.Authentication;
using RestaurantReservation.API.DTOEntity;
using RestaurantReservation.Db.Models;
using RestaurantReservation.Db.Services;

namespace RestaurantReservation.API.Controllers
{
    /// <summary>
    /// API endpoints for managing customers.
    /// </summary>

    [ApiController]
    [Authorize]
    [Route("api/customers")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public CustomerController(
            ICustomerService customerService,
            IMapper mapper,
            ILogger<CustomerController> logger)
        {
            _customerService = customerService ??
                throw new ArgumentNullException(nameof(customerService));
            _mapper = mapper ?? 
                throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? 
                throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Retrieves all customers.
        /// </summary>
        /// <returns>
        /// ActionResult representing a collection of customer data transfer objects (DTOs).
        /// </returns>
        /// <response code="200">Returns the collection of customers.</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerDTO>>> GetAllCustomers()
        {
            _logger.LogInformation("GetAllCustomers Start Getting all customers"); 

            var customers = await _customerService.GetAllAsync();
            var result = _mapper.Map<IEnumerable<CustomerDTO>>(customers);

            _logger.LogInformation($"GetAllCustomers retrieved {customers.Count()} customers successfully.");

            return Ok(result);
        }

        /// <summary>
        /// Retrieves a specific customer by ID.
        /// </summary>
        /// <param name="customerid">The ID of the customer to retrieve.</param>
        /// <returns>
        /// ActionResult representing the customer data transfer object (DTO) with the specified ID.
        /// </returns>
        /// <response code="200">Returns the customer with the specified ID.</response>
        /// <response code="404">If the customer with the given ID is not found.</response>
        [HttpGet("{customerid}", Name ="GetCustomerById")]
        public async Task<ActionResult<CustomerDTO>> GetCustomerById(int customerid)
        {
            _logger.LogInformation($"GetCustomerById started for customer with ID: {customerid}.");

            var customer = await _customerService.GetByIdAsync(customerid);
            if(customer == null)
            {
                return NotFound();
            }

            var result = _mapper.Map<CustomerDTO>(customer);

            _logger.LogInformation($"Retrieved customer with ID: {customerid} successfully.");

            return Ok(result);
        }

        /// <summary>
        /// Adds a new customer.
        /// </summary>
        /// <param name="customerForCreationDTO">The data transfer object (DTO) containing information about the customer to be added.</param>
        /// <returns>
        /// ActionResult representing the result of the add operation.
        /// </returns>
        /// <response code="201">Indicates that the customer was successfully added.</response>
        /// <response code="400">If the input data is invalid.</response>
        /// <response code="401">If the request is not authorized (user does not have the required role).</response>
        /// <response code="500">If an unexpected error occurs during customer addition.</response>
        [HttpPost]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult> AddCustomer(CustomerForCreationDTO customerForCreationDTO)
        {
            _logger.LogInformation("AddCustomer attempting to add a new customer.");

            try
            {
                var customer = _mapper.Map<Customer>(customerForCreationDTO);
                await _customerService.CreateAsync(customer);
                var customerToReturn = _mapper.Map<CustomerDTO>(customer);

                _logger.LogInformation($"New Customer with ID: {customerToReturn.customerId} added successfully.");

                return CreatedAtRoute("GetCustomerById",
                    new { customerid = customerToReturn.customerId },
                    customerToReturn);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding a new customer.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        /// <summary>
        /// Updates a customer.
        /// </summary>
        /// <param name="customerid">The ID of the customer to be updated.</param>
        /// <param name="customerForUpdateDTO">The data transfer object (DTO) containing updated information for the customer.</param>
        /// <returns>
        /// ActionResult representing the result of the update operation.
        /// </returns>
        /// <response code="204">Indicates that the customer was successfully updated.</response>
        /// <response code="400">If the input data is invalid.</response>
        /// <response code="401">If the request is not authorized (user does not have the required role).</response>
        /// <response code="404">If the customer with the given ID is not found.</response>
        /// <response code="500">If an unexpected error occurs during customer update.</response>
        [HttpPut("{customerid}")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult> UpdateCustomer(
            int customerid,
            CustomerForUpdateDTO customerForUpdateDTO
            )
        {
            _logger.LogInformation($"UpdateCustomer attempting to update customer with ID {customerid}.");

            var customer = await _customerService.GetByIdAsync(customerid);
            if(customer == null )
            {
                _logger.LogWarning($"Customer with ID {customerid} not found.");
                return NotFound();
            }

            try
            {
                _mapper.Map(customerForUpdateDTO, customer);
                await _customerService.UpdateAsync(customer);

                _logger.LogInformation($"Customer with ID {customerid} updated successfully.");

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while updating customer with ID {customerid}.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        /// <summary>
        /// Deletes a customer.
        /// </summary>
        /// <param name="customerid">The ID of the customer to be deleted.</param>
        /// <returns>
        /// ActionResult representing the result of the delete operation.
        /// </returns>
        /// <response code="204">Indicates that the customer was successfully deleted.</response>
        /// <response code="401">If the request is not authorized (user does not have the required role).</response>
        /// <response code="404">If the customer with the given ID is not found.</response>
        /// <response code="500">If an unexpected error occurs during customer deletion.</response>
        [HttpDelete("{customerid}")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult> DeleteCustomer(int customerid)
        {
            _logger.LogInformation($"DeleteCustomer attempting to delete customer with ID {customerid}.");

            var customer = await _customerService.GetByIdAsync(customerid);
            if(customer == null )
            {
                _logger.LogWarning($"Customer with ID {customerid} not found. Unable to delete.");
                return NotFound();
            }

            try
            {
                await _customerService.DeleteAsync(customer);

                _logger.LogInformation($"Customer with ID {customerid} deleted successfully.");

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while deleting customer with ID {customerid}.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
    }
}
