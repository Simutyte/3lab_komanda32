using _3lab_komanda32.Models;
using Microsoft.EntityFrameworkCore;

namespace _3lab_komanda32.Repositories
{
    public class ProductRepository
    {
        private readonly ApiDbContext dbContext;

        public ProductRepository(ApiDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        //Basic tasks

        public async Task<IEnumerable<Product>> GetAll()
        {
            return await dbContext.Products.ToListAsync();
        }

        public async Task<Product?> GetById(long id)
        {
            return await dbContext.Products.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<Product> Create(Product product)
        {
            var result = await dbContext.Products.AddAsync(product);
            await dbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Product> Update(Product product)
        {
            dbContext.Entry(product).State = EntityState.Modified;
            await dbContext.SaveChangesAsync();
            return product;

        }

        public async Task<EntityState?> RemoveById(long id)
        {
            var obj = await dbContext.Products.FirstOrDefaultAsync(el => el.Id == id);

            if (obj == null)
            {
                return null;
            }

            var res = dbContext.Products.Remove(obj);
            await dbContext.SaveChangesAsync();

            return res.State;
        }
    }
}
