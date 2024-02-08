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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReservationDTO>>> GetAllReservations()
        {
            _logger.LogInformation("GetAllReservations Start Getting all reservations");

            var reservations = await _reservationRepository.GetAllAsync();

            var reservationsToReturn = _mapper.Map<IEnumerable<ReservationDTO>>(reservations);

            _logger.LogInformation($"GetAllReservations retrieved {reservations.Count()} reservations successfully.");

            return Ok(reservationsToReturn);
        }

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
