using _3lab_komanda32.Models;
using _3lab_komanda32.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        
        //business

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
                return StatusCode(StatusCodes.Status500InternalServerError, Resource.ErrRetrieveFromDB);
            }
        }

        // GET api/<BusinessController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Business>> Get(long id)
        {
            try
            {
                var result = await businessRepository.GetById(id);

                if (result == null) return NotFound();

                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Resource.ErrRetrieveFromDB);
            }
        }

        // POST api/<BusinessController>
        [HttpPost]
        public async Task<ActionResult<Business>> Post([FromBody] Business business)
        {
            try
            {
                if (business == null)
                    return BadRequest();

                var created = await businessRepository.Create(business);

                return CreatedAtAction(nameof(Post),
                    new { id = created.Id }, created);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Resource.ErrCreatingBusiness);
            }
        }

        // PUT api/<BusinessController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Business?>> Put(long id, [FromBody] Business business)
        {
            try
            {
                if (id != business.Id)
                    return BadRequest(Resource.BusinessIdMismatch);

                var toUpdate = await businessRepository.GetById(id);

                if (toUpdate == null)
                    return NotFound(Resource.BusinessIdNotFound + id);

                return await businessRepository.Update(business);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Resource.ErrDataUpdate);
            }
        }

        // DELETE api/<BusinessController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Business?>> Delete(long id)
        {

            try
            {
                var res = await businessRepository.GetById(id);

                if (res == null)
                {
                    return NotFound(Resource.BusinessIdNotFound + id);
                }

                return await businessRepository.RemoveById(id);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Resource.ErrDataDelete);
            }

        }

        //addresses
        // GET api/<BusinessController>/5/Address
        [HttpGet("{id}/Address")]
        public async Task<ActionResult<Address>> GetAddress(long id)
        {
            try
            {
                var result = await businessRepository.GetAddress(id);

                if (result == null) return NotFound();

                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Resource.ErrRetrieveFromDB);
            }
        }

        // PUT api/<BusinessController>/5/Address
        [HttpPut("{id}/Address")]
        public async Task<ActionResult<Address?>> Put(long id, [FromBody] Address address)
        {
            try
            {
                if (id != address.Id)
                    return BadRequest(error: Resource.AddressIdMismatch);

                var toUpdate = await businessRepository.GetAddress(id);

                if (toUpdate == null)
                    return NotFound(Resource.AddressIdNotFound + id);

                return await businessRepository.UpdateAddress(address);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Resource.ErrDataUpdate);
            }
        }

        //privileges

        // POST api/<BusinessController>/5/ManagePrivileges
        [HttpPost("{id}/ManagePrivileges")]
        public async Task<ActionResult<Business>> PostPrivilege(long id, [FromBody] ManagePrivilege managePrivilege)
        {
            try
            {
                if (managePrivilege == null)
                    return BadRequest();

                var result = await businessRepository.GetById(id);

                if (result == null)
                    return BadRequest();

                var created = await businessRepository.CreatePrivilege(managePrivilege);

                return CreatedAtAction(nameof(PostPrivilege),
                    new { id = created.BusinessId }, created);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Resource.ErrCreatingPrivilege);
            }
        }

        // DELETE api/<BusinessController>/5/ManagePrivileges
        [HttpDelete("{id}/ManagePrivileges/{id2}")]
        public async Task<ActionResult<ManagePrivilege?>> DeletePrivileges(long id, long id2)
        {
            try
            {
                var res = await businessRepository.FindPrivilegesByIds(id, id2);

                if (res == null)
                {
                    return NotFound(Resource.BusinessIdNotFound + id + " or " + Resource.EmployeeIdNotFound + id2);
                }

                return await businessRepository.RemovePrivilegeByIds(id, id2);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Resource.ErrDataDelete);
            }           
        }
    }
}
