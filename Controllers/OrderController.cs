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

        // GET: api/<OrderController>
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

        // GET api/<OrderController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> Get(int id)
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

        // POST api/<OrderController>
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

        // PUT api/<OrderController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Order?>> Put(int id, [FromBody] Order order)
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

        // DELETE api/<BusinessController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Order?>> Delete(int id)
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

        //TODO: pas juos parametras Id yra privalomas, bet neaisku, kur ji naudot - request body pats turi OrderId
        //Palikau, nz ka su juo daryt
        // POST api/<OrderController>/{id}/confirm
        [Route("/api/[controller]/{id}/confirm")]
        [HttpPost]
        public async Task<ActionResult<Order>> PostConfirm(int id, [FromBody] OrderConfirmation orderConfirmation)
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
