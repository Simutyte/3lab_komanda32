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

        // GET api/<ReservationController>/5
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

        // Patch api/<ReservationController>/5
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

        // DELETE api/<ReservationController>/5
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
