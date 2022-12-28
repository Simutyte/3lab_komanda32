using _3lab_komanda32.Models;
using Microsoft.EntityFrameworkCore;

namespace _3lab_komanda32.Repositories
{
    public class OrderRepository
    {
        private readonly ApiDbContext dbContext;

        public OrderRepository(ApiDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        //Basic tasks

        public async Task<IEnumerable<Order>> GetAll()
        {
            return await dbContext.Orders.ToListAsync();
        }

        public async Task<Order?> GetById(long id)
        {
            return await dbContext.Orders.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<Order> Create(Order order)
        {
            var result = await dbContext.Orders.AddAsync(order);
            await dbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Order?> Update(Order order)
        {
            var toChange = await dbContext.Orders.FirstOrDefaultAsync(e => e.Id == order.Id);

            if (toChange != null)
            {
                dbContext.Entry<Order>(toChange).CurrentValues.SetValues(order);
                await dbContext.SaveChangesAsync();
                return toChange;
            }
            return null;

        }

        public async Task<Order?> RemoveById(long id)
        {
            var obj = await dbContext.Orders.FirstOrDefaultAsync(el => el.Id == id);

            if (obj != null)
            {
                dbContext.Orders.Remove(obj);
                await dbContext.SaveChangesAsync();
                return obj;
            }

            return null;
        }

        public async Task<OrderConfirmation> CreateConfirmation(OrderConfirmation order)
        {
            var result = await dbContext.OrdersConfirmations.AddAsync(order);
            await dbContext.SaveChangesAsync();
            return result.Entity;
        }
    }
}
