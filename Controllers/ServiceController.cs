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

        // GET: api/<ServiceController>
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

        // GET api/<ServiceController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Service>> Get(int id)
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

        // POST api/<ServiceController>
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

        // PUT api/<ServiceController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Service?>> Put(int id, [FromBody] Service service)
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

        // DELETE api/<ServiceController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Service?>> Delete(int id)
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
