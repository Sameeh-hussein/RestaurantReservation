using AutoMapper;
using Castle.Core.Resource;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantReservation.API.Authentication;
using RestaurantReservation.API.DTOEntity;
using RestaurantReservation.Db.Models;
using RestaurantReservation.Db.Repositories;

namespace RestaurantReservation.API.Controllers
{
    /// <summary>
    /// API endpoints for managing reservations
    /// </summary>>
    
    [Authorize]
    [Route("api/reservation")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<ReservationController> _logger;

        public ReservationController(
            IReservationRepository reservationRepository,
            IMapper mapper,
            ILogger<ReservationController> logger)
        {
            _reservationRepository = reservationRepository ??
                throw new ArgumentNullException(nameof(reservationRepository));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
            _logger = logger ??
                throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Retrieves all reservations.
        /// </summary>
        /// <returns>
        /// ActionResult representing a collection of reservation data transfer objects (DTOs).
        /// </returns>
        /// <response code="200">Returns the collection of reservations.</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReservationDTO>>> GetAllReservations()
        {
            _logger.LogInformation("GetAllReservations Start Getting all reservations");

            var reservations = await _reservationRepository.GetAllAsync();

            var reservationsToReturn = _mapper.Map<IEnumerable<ReservationDTO>>(reservations);

            _logger.LogInformation($"GetAllReservations retrieved {reservations.Count()} reservations successfully.");

            return Ok(reservationsToReturn);
        }

        /// <summary>
        /// Retrieves a reservation by its ID.
        /// </summary>
        /// <param name="reservationid">The ID of the reservation to retrieve.</param>
        /// <returns>
        /// ActionResult representing the reservation data transfer object (DTO) with the specified ID.
        /// </returns>
        /// <response code="200">Returns the reservation with the specified ID.</response>
        /// <response code="404">If the reservation with the given ID is not found.</response>
        [HttpGet("{reservationid}", Name = "GetReservationById")]
        public async Task<ActionResult<ReservationDTO>> GetReservationById(int reservationid)
        {
            _logger.LogInformation($"GetReservationById started for reservation with ID: {reservationid}.");

            var reservation = await _reservationRepository.GetByIdAsync(reservationid);
            if (reservation == null)
            {
                _logger.LogWarning($"Reservation with ID {reservationid} not found.");
                return NotFound();
            }

            var reservationToReturn = _mapper.Map<ReservationDTO>(reservation);

            _logger.LogInformation($"Retrieved reservation with ID: {reservationid} successfully.");

            return Ok(reservationToReturn);
        }

        /// <summary>
        /// Adds a new reservation.
        /// </summary>
        /// <param name="reservationForCreationDTO">The data transfer object (DTO) containing information about the reservation to be added.</param>
        /// <returns>
        /// ActionResult representing the newly added reservation, along with a location header for accessing it.
        /// </returns>
        /// <response code="201">Returns the newly added reservation and a location header for accessing it.</response>
        /// <response code="400">If the input data is invalid.</response>
        /// <response code="401">If the request is not authorized (user does not have the required role).</response>
        /// <response code="500">If an unexpected error occurs during reservation addition.</response>
        [HttpPost]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult> AddReservation(ReservationForCreationDTO reservationForCreationDTO)
        {
            _logger.LogInformation("AddReservation attempting to add a new reservation.");

            try
            {
                var reservation = _mapper.Map<Reservation>(reservationForCreationDTO);

                await _reservationRepository.CreateAsync(reservation);

                var reservationToReturn = _mapper.Map<ReservationDTO>(reservation);

                _logger.LogInformation($"New Reservation with ID: {reservationToReturn.customerId} added successfully.");

                return CreatedAtRoute("GetReservationById",
                        new
                        {
                            reservationid = reservationToReturn.reservationId
                        },
                        reservationToReturn
                    );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding a new reservation.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        /// <summary>
        /// Updates a reservation.
        /// </summary>
        /// <param name="reservationid">The ID of the reservation to be updated.</param>
        /// <param name="reservationForUpdateDTO">The data transfer object (DTO) containing updated information for the reservation.</param>
        /// <returns>
        /// ActionResult representing the result of the update operation.
        /// </returns>
        /// <response code="204">Indicates that the reservation was successfully updated.</response>
        /// <response code="400">If the input data is invalid.</response>
        /// <response code="401">If the request is not authorized (user does not have the required role).</response>
        /// <response code="404">If the reservation with the given ID is not found.</response>
        /// <response code="500">If an unexpected error occurs during reservation update.</response>
        [HttpPut("{reservationid}")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult> UpdateReservation(
                int reservationid,
                ReservationForUpdateDTO reservationForUpdateDTO
            )
        {
            _logger.LogInformation($"UpdateReservation attempting to update reservation with ID: {reservationid}.");

            try
            {
                var reservation = await _reservationRepository.GetByIdAsync(reservationid);
                if (reservation == null)
                {
                    _logger.LogWarning($"Reservation with ID {reservationid} not found.");
                    return NotFound();
                }

                _mapper.Map(reservationForUpdateDTO, reservation);

                await _reservationRepository.UpdateAsync(reservation);

                _logger.LogInformation($"Reservation with ID {reservationid} updated successfully.");

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while updating reservation with ID {reservationid}.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        /// <summary>
        /// Deletes a reservation.
        /// </summary>
        /// <param name="reservationid">The ID of the reservation to be deleted.</param>
        /// <returns>
        /// ActionResult representing the result of the delete operation.
        /// </returns>
        /// <response code="204">Indicates that the reservation was successfully deleted.</response>
        /// <response code="401">If the request is not authorized (user does not have the required role).</response>
        /// <response code="404">If the reservation with the given ID is not found.</response>
        /// <response code="500">If an unexpected error occurs during reservation deletion.</response>
        [HttpDelete("{reservationid}")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult> DeleteReservation(int reservationid)
        {
            _logger.LogInformation($"DeleteReservation attempting to delete reservation with ID {reservationid}.");

            try
            {
                var reservation = await _reservationRepository.GetByIdAsync(reservationid);
                if (reservation == null)
                {
                    _logger.LogWarning($"Reservation with ID {reservationid} not found. Unable to delete.");
                    return NotFound();
                }

                await _reservationRepository.DeleteAsync(reservation);

                _logger.LogInformation($"Reservation with ID {reservationid} deleted successfully.");

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while deleting reservation with ID {reservationid}.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
    }
}
