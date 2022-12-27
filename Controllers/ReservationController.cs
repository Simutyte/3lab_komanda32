﻿using _3lab_komanda32.Models;
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
        public async Task<ActionResult> Get(int customerId)
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

        //TODO: cia kazkoks bsk gaidys pas juos - nes jei duodi CustomerId, tai gauni list'a reservations,
        //tai kaip zinot, kuri reikia pakeist?
        //As dabar padariau, kad paduodi reservationId, o ne CustomerId
        // Patch api/<ReservationController>/5
        [HttpPatch("{id}")]
        public async Task<ActionResult<Reservation>> Patch(int id, [FromBody] Reservation reservation)
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
        public async Task<ActionResult> Delete(int id)
        {
            var res = await reservationRepository.RemoveById(id);

            if (res == null)
            {
                return NotFound(Resource.ReservationIdNotFound + id);
            }

            if (res == EntityState.Deleted)
            {
                return Ok();
            }

            return StatusCode(StatusCodes.Status500InternalServerError, Resource.ErrDataDelete);
        }
    }
}