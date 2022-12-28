using _3lab_komanda32.Models;
using _3lab_komanda32.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace _3lab_komanda32.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        //visur turėtų būt interface greičiausiai
        private readonly ProductRepository productRepository;
        public ProductController(ProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        // GET: api/<BusinessController>
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            try
            {
                return Ok(await productRepository.GetAll());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Resource.ErrRetrieveFromDB);
            }
        }

        // GET api/<BusinessController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> Get(int id)
        {
            try
            {
                var result = await productRepository.GetById(id);

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
        public async Task<ActionResult<Product>> Post([FromBody] Product product)
        {
            try
            {
                if (product == null)
                    return BadRequest();

                var created = await productRepository.Create(product);

                return CreatedAtAction(nameof(product),
                    new { id = created.Id }, created);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Resource.ErrCreatingProduct);
            }
        }

        // PUT api/<BusinessController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Product>> Put(int id, [FromBody] Product product)
        {
            try
            {
                if (id != product.Id)
                    return BadRequest(Resource.EmployeeIDMisMatch);

                var toUpdate = await productRepository.GetById(id);

                if (toUpdate == null)
                    return NotFound(Resource.EmployeeIdNotFound + id);

                return await productRepository.Update(product);
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
            var res = await productRepository.RemoveById(id);

            if (res == null)
            {
                return NotFound(Resource.ProductIdNotFound + id);
            }

            if (res == EntityState.Deleted)
            {
                return Ok();
            }

            return StatusCode(StatusCodes.Status500InternalServerError, Resource.ErrDataDelete);
        }
    }
}
