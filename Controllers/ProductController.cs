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

        /// <summary>
        /// Gets all Products
        /// </summary>
        /// <returns> All Products</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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

        /// <summary>
        /// Gets a specific product
        /// </summary>
        /// <param name="id">Id of product</param>
        /// <returns> Product by specified Id</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> Get(long id)
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

        /// <summary>
        /// Posts a new product object
        /// </summary>
        /// <returns> Your created product</returns>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public async Task<ActionResult<Product>> Post([FromBody] Product product)
        {
            try
            {
                if (product == null)
                    return BadRequest();

                var created = await productRepository.Create(product);

                return CreatedAtAction(nameof(Post),
                    new { id = created.Id }, created);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Resource.ErrCreatingProduct);
            }
        }

        /// <summary>
        /// Updates a specified product
        /// </summary>
        /// <param name="id">Id of product you want to update</param>
        /// <param name="product">Your new product</param>
        /// <returns> Your updated product</returns>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPatch("{id}")]
        public async Task<ActionResult<Product?>> Patch(long id, [FromBody] Product product)
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

        /// <summary>
        /// Deletes a specified product
        /// </summary>
        /// <param name="id">Id of product you want to delete</param>
        /// <returns> Your deleted product</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Product?>> Delete(long id)
        {
            try
            {
                var res = await productRepository.GetById(id);

                if (res == null)
                {
                    return NotFound(Resource.ProductIdNotFound + id);
                }

                return await productRepository.RemoveById(id);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Resource.ErrDataDelete);
            }
        }

        /// <summary>
        /// Updates discount for a specified product
        /// </summary>
        /// <param name="id">Id of product whose discount you want to update</param>
        /// <param name="discount">Your new discount</param>
        /// <returns> Your updated product</returns>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPatch("{id}/discount")]
        public async Task<ActionResult<Product>> PatchDiscount(long id, [FromBody] ProductDiscount discount)
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

        /// <summary>
        /// Gets list of products by name
        /// </summary>
        /// <param name="name">Name of product</param>
        /// <returns> List of products</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("find/name/{name}")]
        public ActionResult<IEnumerable<Product>> GetByName(string name)
        {
            try
            {
                var result = productRepository.GetByName(name);

                if (result == null) return NotFound();

                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Resource.ErrRetrieveFromDB);
            }
        }

        /// <summary>
        /// Gets list of products by category
        /// </summary>
        /// <param name="category">Category of product</param>
        /// <returns> List of products</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("find/category/{category}")]
        public ActionResult<IEnumerable<Product>> GetByCategory(string category)
        {
            try
            {
                var result = productRepository.GetByCategory(category);

                if (result == null) return NotFound();

                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Resource.ErrRetrieveFromDB);
            }
        }

        /// <summary>
        /// Adds product to order
        /// </summary>
        /// <param name="id">Id of Order</param>
        /// <param name="product">Your new product</param>
        /// <returns> Your created product</returns>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("Order/add/{id}")]
        public async Task<ActionResult<Product>> PostAddProductToOrder(long id, [FromBody] Product product)
        {
            try
            {
                if (product == null)
                    return BadRequest();

                var created = await productRepository.AddProductToOrder(id, product);

                if (created == null)
                    return NotFound();

                return CreatedAtAction(nameof(PostAddProductToOrder),
                    new { id = created.Id }, created);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Resource.ErrAddingProduct);
            }
        }

        /// <summary>
        /// Removes product from order
        /// </summary>
        /// <param name="id">Id of order</param>
        /// <param name="product">Product you want to remove</param>
        /// <returns> Updated order</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("Order/remove/{id}")]
        public async Task<ActionResult<Order>> RemoveProductFromOrder(long id, [FromBody] Product product)
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

        /// <summary>
        /// Updates a product in order
        /// </summary>
        /// <param name="id">Id of order</param>
        /// <param name="product">Your new product</param>
        /// <returns> Updated order</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPut("Order/update/{id}")]
        public async Task<ActionResult<Order>> Put(long id, [FromBody] Product product)
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
