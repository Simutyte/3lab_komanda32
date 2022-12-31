using _3lab_komanda32.Models;
using Microsoft.EntityFrameworkCore;

namespace _3lab_komanda32.Repositories
{
    public class ServiceRepository
    {
        private readonly ApiDbContext dbContext;

        public ServiceRepository(ApiDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<Service>> GetAll()
        {
            return await dbContext.Services.ToListAsync();
        }

        public async Task<Service?> GetById(long id)
        {
            return await dbContext.Services.FirstOrDefaultAsync(el => el.Id == id);
        }

        public async Task<Service> Create(Service service)
        {
            var res = await dbContext.Services.AddAsync(service);
            await dbContext.SaveChangesAsync();
            return res.Entity;
        }

        public async Task<Service?> Update(Service service)
        {
            var toChange = await dbContext.Services.FirstOrDefaultAsync(e => e.Id == service.Id);

            if (toChange == null)
                return null;

            dbContext.Entry<Service>(toChange).CurrentValues.SetValues(service);
            await dbContext.SaveChangesAsync();
            return toChange;
        }

        public async Task<Service?> RemoveById(long id)
        {
            var obj = await dbContext.Services.FirstOrDefaultAsync(el => el.Id == id);

            if (obj == null)
                return null;

            dbContext.Services.Remove(obj);
            await dbContext.SaveChangesAsync();
            return obj;
        }
    }
}
