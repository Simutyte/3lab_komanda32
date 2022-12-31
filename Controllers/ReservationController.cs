using _3lab_komanda32.Models;
using _3lab_komanda32.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace _3lab_komanda32.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly ReservationRepository reservationRepository;
        public ReservationController(ReservationRepository reservationRepository)
        {
            this.reservationRepository = reservationRepository;
        }

        /// <summary>
        /// Gets a specific reservation
        /// </summary>
        /// <param name="customerId">Id of customer</param>
        /// <returns> Reservation by specified customer Id</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("{customerId}")]
        public async Task<ActionResult> Get(long customerId)
        {
            try
            {
                var result = await reservationRepository.GetByCustomerId(customerId);

                if (result == null) return NotFound();

                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Resource.ErrRetrieveFromDB);
            }
        }

        /// <summary>
        /// Updates a specified reservation
        /// </summary>
        /// <param name="id">Id of reservation you want to update</param>
        /// <param name="reservation">Your new reservation</param>
        /// <returns> Your updated reservation</returns>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPatch("{id}")]
        public async Task<ActionResult<Reservation?>> Patch(long id, [FromBody] Reservation reservation)
        {
            try
            {
                if (id != reservation.Id)
                    return BadRequest(Resource.ReservationIdMismatch);

                var toUpdate = await reservationRepository.GetById(id);

                if (toUpdate == null)
                    return NotFound(Resource.ReservationIdNotFound + id);

                return await reservationRepository.Update(reservation);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Resource.ErrDataUpdate);
            }
        }

        /// <summary>
        /// Deletes a specified reservation
        /// </summary>
        /// <param name="id">Id of reservation you want to delete</param>
        /// <returns> Your deleted reservation</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Reservation?>> Delete(long id)
        {
            try
            {
                var res = await reservationRepository.GetById(id);

                if (res == null)
                {
                    return NotFound(Resource.ReservationIdNotFound + id);
                }

                return await reservationRepository.RemoveById(id);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Resource.ErrDataDelete);
            }
        }
    }
}
