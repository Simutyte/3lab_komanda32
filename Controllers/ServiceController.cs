using _3lab_komanda32.Models;
using _3lab_komanda32.Repositories;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace _3lab_komanda32.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private readonly ServiceRepository serviceRepository;
        public ServiceController(ServiceRepository serviceRepository)
        {
            this.serviceRepository = serviceRepository;
        }

        /// <summary>
        /// Gets all services
        /// </summary>
        /// <returns> All services</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            try
            {
                return Ok(await serviceRepository.GetAll());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Resource.ErrRetrieveFromDB);
            }
        }

        /// <summary>
        /// Gets a specific service
        /// </summary>
        /// <param name="id">Id of service</param>
        /// <returns> Service by specified Id</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("{id}")]
        public async Task<ActionResult<Service>> Get(long id)
        {
            try
            {
                var result = await serviceRepository.GetById(id);

                if (result == null) return NotFound();

                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Resource.ErrRetrieveFromDB);
            }
        }

        /// <summary>
        /// Posts a new service object
        /// </summary>
        /// <returns> Your created service</returns>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public async Task<ActionResult<Service>> Post([FromBody] Service service)
        {
            try
            {
                if (service == null)
                    return BadRequest();

                var created = await serviceRepository.Create(service);

                return CreatedAtAction(nameof(Post), new { id = created.Id }, created);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Resource.ErrCreatingService);
            }
        }

        /// <summary>
        /// Updates a specified service
        /// </summary>
        /// <param name="id">Id of service you want to update</param>
        /// <param name="service">Your new service</param>
        /// <returns> Your updated service</returns>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPut("{id}")]
        public async Task<ActionResult<Service?>> Put(long id, [FromBody] Service service)
        {
            try
            {
                if (id != service.Id)
                    return BadRequest(Resource.ServiceIdMismatch);

                var toUpdate = await serviceRepository.GetById(id);

                if (toUpdate == null)
                    return NotFound(Resource.ServiceIdNotFound + id);

                return await serviceRepository.Update(service);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Resource.ErrDataUpdate);
            }
        }

        /// <summary>
        /// Deletes a specified service
        /// </summary>
        /// <param name="id">Id of service you want to delete</param>
        /// <returns> Your deleted service</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Service?>> Delete(long id)
        {
            try
            {
                var res = await serviceRepository.GetById(id);

                if (res == null)
                {
                    return NotFound(Resource.ServiceIdNotFound + id);
                }

                return await serviceRepository.RemoveById(id);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Resource.ErrDataDelete);
            }

        }
    }
}
