using _3lab_komanda32.Models;
using _3lab_komanda32.Repositories;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace _3lab_komanda32.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusinessController : ControllerBase
    {
        //visur turėtų būt interface greičiausiai
        private readonly BusinessRepository businessRepository;
        public BusinessController(BusinessRepository businessRepository)
        {
            this.businessRepository = businessRepository;
        }

        // GET: api/<BusinessController>
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            try
            {
                return Ok(await businessRepository.GetAll());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        // GET api/<BusinessController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Business>> Get(int id)
        {
            try
            {
                var result = await businessRepository.GetById(id);

                if (result == null) return NotFound();

                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        // POST api/<BusinessController>
        [HttpPost]
        public async Task<ActionResult<Employee>> Post([FromBody] Business business)
        {
            try
            {
                if (business == null)
                    return BadRequest();

                var created = await businessRepository.Create(business);

                return CreatedAtAction(nameof(business),
                    new { id = created.Id }, created);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating new employee record");
            }
        }

        // PUT api/<BusinessController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Business>> Put(int id, [FromBody] Business business)
        {
            try
            {
                if (id != business.Id)
                    return BadRequest("Employee ID mismatch");

                var toUpdate = await businessRepository.GetById(id);

                if (toUpdate == null)
                    return NotFound($"Employee with Id = {id} not found");

                return await businessRepository.Update(business);             
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating data");
            }
        }

        // DELETE api/<BusinessController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
