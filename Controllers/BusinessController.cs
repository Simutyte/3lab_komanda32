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

        /// <summary>
        /// Gets all businesses
        /// </summary>
        /// <returns> All businesses</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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

        /// <summary>
        /// Gets a specific businesses
        /// </summary>
        /// <param name="id">Id of business</param>
        /// <returns> Business by specified Id</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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

        /// <summary>
        /// Posts a new business object
        /// </summary>
        /// <returns> Your created business</returns>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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

        /// <summary>
        /// Updates a specified business
        /// </summary>
        /// <param name="id">Id of business you want to update</param>
        /// <param name="business">Business object you want to update to</param>
        /// <returns> Your updated business</returns>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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

        /// <summary>
        /// Deletes a specified business
        /// </summary>
        /// <param name="id">Id of business you want to delete</param>
        /// <returns> Your deleted business</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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

        /// <summary>
        /// Gets an address by Id
        /// </summary>
        /// <param name="id">Id of address</param>
        /// <returns> Address by Id</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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

        /// <summary>
        /// Updates an address by Id
        /// </summary>
        /// <param name="id">Id of address</param>
        /// <param name="address">Address object you want to update to</param>
        /// <returns> Updated address</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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

        /// <summary>
        /// Posts a privilege
        /// </summary>
        /// <param name="id">Id of business</param>
        /// <param name="managePrivilege">Your new privilege</param>
        /// <returns> Updated business</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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

        /// <summary>
        /// Deletes a privilege by business and privilege Ids
        /// </summary>
        /// <param name="id">Id of business</param>
        /// <param name="id2">Id of privilege</param>
        /// <returns> Deleted privilege</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
