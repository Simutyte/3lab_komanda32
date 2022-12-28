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

        [HttpPatch("{id}")]
        public async Task<ActionResult<Product>> Patch(int id, [FromBody] Product product)
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

        // PUT api/<BusinessController>/5
        [HttpPatch("{id}/discount")]
        public async Task<ActionResult<Product>> PatchDiscount(int id, [FromBody] ProductDiscount discount)
        {
            try
            {
                if (id != discount.Id)
                    return BadRequest(Resource.ProductidMismatch);

                if (discount.Discount < 0 || discount.Discount > 1)
                    return BadRequest(Resource.DiscountIntervalErr);

                var toUpdate = await productRepository.GetById(id);

                if (toUpdate == null)
                    return NotFound(Resource.ProductIdNotFound + id);

                return await productRepository.UpdateDiscount(discount, toUpdate);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Resource.ErrDataUpdate);
            }
        }

        [HttpGet("find/name/{name}")]
        public async Task<ActionResult<Product>> GetByName(string name)
        {
            try
            {
                var result = await productRepository.GetByName(name);

                if (result == null) return NotFound();

                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Resource.ErrRetrieveFromDB);
            }
        }

        [HttpGet("find/category/{category}")]
        public async Task<ActionResult<Product>> GetByCategory(string category)
        {
            try
            {
                var result = await productRepository.GetByCategory(category);

                if (result == null) return NotFound();

                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Resource.ErrRetrieveFromDB);
            }
        }

        [HttpPost("Order/add/{id}")]
        public async Task<ActionResult<Product>> PostAddProductToOrder(int id, [FromBody] Product product)
        {
            try
            {
                if (product == null)
                    return BadRequest();

                var created = await productRepository.AddProductToOrder(id, product);

                if (created == null)
                    return NotFound();

                return CreatedAtAction(nameof(product),
                    new { id = created.Id }, created);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Resource.ErrAddingProduct);
            }
        }

        //cia sjp reikia pagal api grazint product, bet labai nelogiska
        [HttpDelete("Order/remove/{id}")]
        public async Task<ActionResult<Order>> RemoveProductFromOrder(int id, [FromBody] Product product)
        {
            try
            {
                var res = await productRepository.RemoveProductFromOrder(id, product);

                if (res == null)
                    return NotFound(Resource.ProductOrderIdNotFound);

                return Ok(res);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Resource.ErrDataDelete);
            }
        }

        [HttpPut("Order/update/{id}")]
        public async Task<ActionResult<Order>> Put(int id, [FromBody] Product product)
        {
            try
            {
                var order = await productRepository.UpdateOrderProduct(id, product);

                if (order == null)
                    return NotFound(Resource.ProductOrderIdNotFound);

                return Ok(order);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Resource.ErrDataUpdate);
            }
        }
    }
}
