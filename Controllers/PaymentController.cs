using _3lab_komanda32.Models;
using _3lab_komanda32.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace _3lab_komanda32.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly PaymentRepository paymentRepository;
        public PaymentController(PaymentRepository paymentRepository)
        {
            this.paymentRepository = paymentRepository;
        }

        /// <summary>
        /// Gets a specific payment
        /// </summary>
        /// <param name="id">Id of payment</param>
        /// <returns> Payment by specified Id</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("{id}")]
        public async Task<ActionResult<Payment>> Get(long id)
        {
            try
            {
                var result = await paymentRepository.GetById(id);

                if (result == null) return NotFound();

                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Resource.ErrRetrieveFromDB);
            }
        }

        /// <summary>
        /// Posts a new payment object
        /// </summary>
        /// <returns> Your created payment</returns>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public async Task<ActionResult<Payment>> Post([FromBody] Payment payment)
        {
            try
            {
                if (payment == null)
                    return BadRequest();

                var created = await paymentRepository.Create(payment);

                return CreatedAtAction(nameof(Post), new { id = created.Id }, created);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Resource.ErrCreatingPayment);
            }
        }

        /// <summary>
        /// Updates a specified payment
        /// </summary>
        /// <param name="id">id of payment you want to update</param>
        /// <param name="payment">Your new payment</param>
        /// <returns> Your updated payment</returns>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPut("{id}")]
        public async Task<ActionResult<Payment>> Put(long id, [FromBody] Payment payment)
        {
            try
            {
                if (id != payment.Id)
                    return BadRequest(Resource.ServiceIdMismatch);

                var toUpdate = await paymentRepository.GetById(id);

                if (toUpdate == null)
                    return NotFound(Resource.PaymentIdNotFound + id);

                return await paymentRepository.Update(payment);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Resource.ErrDataUpdate);
            }
        }

        /// <summary>
        /// Deletes a specified payment
        /// </summary>
        /// <param name="id">Id of payment you want to delete</param>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(long id)
        {
            var res = await paymentRepository.RemoveById(id);

            if (res == null)
            {
                return NotFound(Resource.PaymentIdNotFound + id);
            }

            if (res == EntityState.Deleted)
            {
                return Ok();
            }

            return StatusCode(StatusCodes.Status500InternalServerError, Resource.ErrDataDelete);
        }
    }
}
