using AutoMapper;
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

        public ReservationController(IReservationRepository reservationRepository, IMapper mapper)
        {
            _reservationRepository = reservationRepository ?? throw new ArgumentNullException(nameof(reservationRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReservationDTO>>> GetAllReservations()
        {
            var reservations = await _reservationRepository.GetAllAsync();

            var reservationsToReturn = _mapper.Map<IEnumerable<ReservationDTO>>(reservations);

            return Ok(reservationsToReturn);
        }

        [HttpGet("{reservationid}", Name = "GetReservationById")]
        public async Task<ActionResult<ReservationDTO>> GetReservationById(int reservationid)
        {
            var reservation = await _reservationRepository.GetByIdAsync(reservationid);
            if (reservation == null)
            {
                return NotFound();
            }

            var reservationToReturn = _mapper.Map<ReservationDTO>(reservation);

            return Ok(reservationToReturn);
        }

        [HttpPost]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult> AddReservation(ReservationForCreationDTO reservationForCreationDTO)
        {
            var reservation = _mapper.Map<Reservation>(reservationForCreationDTO);

            await _reservationRepository.CreateAsync(reservation);

            var reservationToReturn = _mapper.Map<ReservationDTO>(reservation);

            return CreatedAtRoute("GetReservationById",
                    new
                    {
                        reservationid = reservationToReturn.reservationId
                    },
                    reservationToReturn
                );
        }

        [HttpPut("{reservationid}")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult> UpdateReservation(
                int reservationid,
                ReservationForUpdateDTO reservationForUpdateDTO
            )
        {
            var reservation = await _reservationRepository.GetByIdAsync(reservationid);
            if(reservation == null)
            {
                return NotFound();
            }

            _mapper.Map(reservationForUpdateDTO, reservation);

            await _reservationRepository.UpdateAsync(reservation);

            return NoContent();
        }

        [HttpDelete("{reservationid}")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult> DeleteReservation(int reservationid)
        {
            var reservation = await _reservationRepository.GetByIdAsync(reservationid);
            if(reservation == null )
            {
                return NotFound();
            }

            await _reservationRepository.DeleteAsync(reservation);

            return NoContent();
        }
    }
}
