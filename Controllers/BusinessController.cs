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
                return StatusCode(StatusCodes.Status500InternalServerError, Resource.ErrRetrieveFromDB);
            }
        }

        // GET api/<BusinessController>/5/Address
        [HttpGet("{id}/Address")]
        public async Task<ActionResult<Address>> GetAddress(int id)
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
        public async Task<ActionResult<Address>> Put(int id, [FromBody] Address address)
        {
            try
            {
                if (id != address.Id)
                    return BadRequest(Resource.AddressIdMismatch);

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

        // POST api/<BusinessController>/5/ManagePrivileges
        [HttpPost("{id}/ManagePrivileges")]
        public async Task<ActionResult<Business>> PostPrivilege(int id, [FromBody] ManagePrivilege managePrivilege)
        {
            try
            {
                if (managePrivilege == null)
                    return BadRequest();

                var result = await businessRepository.GetById(id);

                if (result == null) 
                    return BadRequest();

                var created = await businessRepository.CreatePrivilege(managePrivilege);

                return CreatedAtAction(nameof(managePrivilege),
                    new { id = created.BusinessId }, created);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Resource.ErrCreatingPrivilege);
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

                return CreatedAtAction(nameof(business),
                    new { id = created.Id }, created);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Resource.ErrCreatingBusiness);
            }
        }

        // PUT api/<BusinessController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Business>> Put(int id, [FromBody] Business business)
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
        public async Task<ActionResult> Delete(int id)
        {
            var res = await businessRepository.RemoveById(id);

            if (res == null)
            {
                return NotFound(Resource.BusinessIdNotFound + id);
            }

            if (res == EntityState.Deleted)
            {
                return Ok();
            }

            return StatusCode(StatusCodes.Status500InternalServerError, Resource.ErrDataDelete);
        }

        // DELETE api/<BusinessController>/5/ManagePrivileges
        [HttpDelete("{id}/ManagePrivileges/{id2}")]
        public async Task<ActionResult> DeletePrivileges(long id, long id2)
        {
            var res = await businessRepository.RemovePrivilegeByIds(id, id2);

            if (res == null)
            {
                return NotFound(Resource.BusinessIdNotFound + id + " or " + Resource.EmployeeIdNotFound + id2);
            }

            if (res == EntityState.Deleted)
            {
                return Ok();
            }

            return StatusCode(StatusCodes.Status500InternalServerError, Resource.ErrDataDelete);
        }
    }
}
