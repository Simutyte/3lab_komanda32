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

        // GET api/<PaymentController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Payment>> Get(int id)
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

        // POST api/<PaymentController>
        [HttpPost]
        public async Task<ActionResult<Payment>> Post([FromBody] Payment payment)
        {
            try
            {
                if (payment == null)
                    return BadRequest();

                var created = await paymentRepository.Create(payment);

                return CreatedAtAction(nameof(payment), new { id = created.Id }, created);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Resource.ErrCreatingPayment);
            }
        }

        // PUT api/<PaymentController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Payment>> Put(int id, [FromBody] Payment payment)
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

        // DELETE api/<PaymentController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
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
