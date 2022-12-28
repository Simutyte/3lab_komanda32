using _3lab_komanda32.Models;
using _3lab_komanda32.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace _3lab_komanda32.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoyaltyController : ControllerBase
    {

        private readonly LoyaltyRepository loyaltyRepository;
        public LoyaltyController(LoyaltyRepository loyaltyRepository)
        {
            this.loyaltyRepository = loyaltyRepository;
        }

        // GET: api/<BusinessController>
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            try
            {
                return Ok(await loyaltyRepository.GetAll());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Resource.ErrRetrieveFromDB);
            }
        }

        // GET api/<BusinessController>/5
        [HttpGet("{customerId}")]
        public async Task<ActionResult<LoyaltyProgram>> Get(long customerId)
        {
            try
            {
                var result = await loyaltyRepository.GetById(customerId);

                if (result == null) return NotFound();

                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Resource.ErrRetrieveFromDB);
            }
        }

        // POST api/<BusinessController>
        [HttpPost("{customerId}")]
        public async Task<ActionResult<LoyaltyProgram>> Post(long customerId)
        {
            try
            {
                LoyaltyProgram loyalty = new LoyaltyProgram(customerId);

                var created = await loyaltyRepository.Create(new LoyaltyProgram(customerId));

                return CreatedAtAction(nameof(Post), new { id = created.CustomerId }, created);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Resource.ErrCreatingLoyalty);
            }
        }

        // DELETE api/<BusinessController>/5
        [HttpDelete("{customerId}")]
        public async Task<ActionResult<LoyaltyProgram?>> Delete(int customerId)
        {

            try
            {
                var res = await loyaltyRepository.GetById(customerId);

                if (res == null)
                {
                    return NotFound(Resource.LoyaltyIdNotFound + customerId);
                }

                return await loyaltyRepository.RemoveById(customerId);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Resource.ErrDataDelete);
            }

        }
    }
}
