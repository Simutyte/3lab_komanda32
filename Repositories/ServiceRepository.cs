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

        public async Task<Service> Update(Service service)
        {
            dbContext.Entry(service).State = EntityState.Modified;
            await dbContext.SaveChangesAsync();
            return service;
        }
    }
}
