using _3lab_komanda32.Models;
using _3lab_komanda32.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace _3lab_komanda32.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        //visur turėtų būt interface greičiausiai
        private readonly OrderRepository orderRepository;
        public OrderController(OrderRepository orderRepository)
        {
            this.orderRepository = orderRepository;
        }

        /// <summary>
        /// Gets all orders
        /// </summary>
        /// <returns> All Orders</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            try
            {
                return Ok(await orderRepository.GetAll());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Resource.ErrRetrieveFromDB);
            }
        }

        /// <summary>
        /// Gets a specific order
        /// </summary>
        /// <param name="id">Id of order</param>
        /// <returns> Order by specified Id</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        // GET api/<OrderController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> Get(long id)
        {
            try
            {
                var result = await orderRepository.GetById(id);

                if (result == null) return NotFound();

                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Resource.ErrRetrieveFromDB);
            }
        }

        /// <summary>
        /// Posts a new order object
        /// </summary>
        /// <returns> Your created order</returns>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public async Task<ActionResult<Order>> Post([FromBody] Order order)
        {
            try
            {
                if (order == null)
                    return BadRequest();

                var created = await orderRepository.Create(order);

                return CreatedAtAction(nameof(Post), new { id = created.Id }, created);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Resource.ErrCreatingOrder);
            }
        }

        /// <summary>
        /// Updates a specified order
        /// </summary>
        /// <param name="id">id of Order you want to update</param>
        /// <param name="order">Order you want to update to</param>
        /// <returns> Your updated order</returns>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPut("{id}")]
        public async Task<ActionResult<Order?>> Put(long id, [FromBody] Order order)
        {
            try
            {
                if (id != order.Id)
                    return BadRequest(Resource.OrderIdMismatch);

                var toUpdate = await orderRepository.GetById(id);

                if (toUpdate == null)
                    return NotFound(Resource.OrderIdNotFound + id);

                return await orderRepository.Update(order);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Resource.ErrDataUpdate);
            }
        }

        /// <summary>
        /// Deletes a specified order
        /// </summary>
        /// <param name="id">Id of order you want to delete</param>
        /// <returns> Your deleted order</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Order?>> Delete(long id)
        {
            try
            {
                var res = await orderRepository.GetById(id);

                if (res == null)
                {
                    return NotFound(Resource.OrderIdNotFound + id);
                }

                return await orderRepository.RemoveById(id);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Resource.ErrDataDelete);
            }
        }

        /// <summary>
        /// Posts a new order confirmation
        /// </summary>
        /// <param name="id">Id of order you want to confirm</param>
        /// <param name="orderConfirmation">Your new order confirmation</param>
        /// <returns> Your confirmed order</returns>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("/api/[controller]/{id}/confirm")]
        [HttpPost]
        public async Task<ActionResult<Order>> PostConfirm(long id, [FromBody] OrderConfirmation orderConfirmation)
        {
            try
            {
                if (orderConfirmation == null)
                    return BadRequest();

                var created = await orderRepository.CreateConfirmation(orderConfirmation);

                return CreatedAtAction(nameof(PostConfirm), new { id = created.Id }, created);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Resource.ErrCreatingOrderConfirm);
            }
        }
    }
}
