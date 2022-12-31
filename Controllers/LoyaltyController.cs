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

        /// <summary>
        /// Gets all loyalties
        /// </summary>
        /// <returns> All loyalties</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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

        /// <summary>
        /// Gets a specific loyalty
        /// </summary>
        /// <param name="customerId">Id of customer</param>
        /// <returns> Business by specified customer Id</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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

        /// <summary>
        /// Posts a new loyalty object
        /// </summary>
        /// <param name="customerId">Id of customer for which you want to create a loyalty</param>
        /// <returns> Your created loyalty</returns>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("{customerId}")]
        public async Task<ActionResult<LoyaltyProgram>> Post(long customerId)
        {
            try
            {
                LoyaltyProgram loyalty = new LoyaltyProgram(customerId);

                //TODO: cia neturejo buti taip?
                //var created = await loyaltyRepository.Create(loyalty);

                var created = await loyaltyRepository.Create(new LoyaltyProgram(customerId));

                return CreatedAtAction(nameof(Post), new { id = created.CustomerId }, created);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Resource.ErrCreatingLoyalty);
            }
        }

        /// <summary>
        /// Deletes a loyalty by customer id
        /// </summary>
        /// <param name="customerId">Id of customer</param>
        /// <returns> Your deleted loyalty</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("{customerId}")]
        public async Task<ActionResult<LoyaltyProgram?>> Delete(long customerId)
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
