using _3lab_komanda32.Models;
using Microsoft.EntityFrameworkCore;

namespace _3lab_komanda32.Repositories
{
    public class LoyaltyRepository
    {
        private readonly ApiDbContext dbContext;

        public LoyaltyRepository(ApiDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        //get all loyalties
        public async Task<IEnumerable<LoyaltyProgram>> GetAll()
        {
            return await dbContext.Loyalties.ToListAsync();
        }

        //get loyalty by customer id
        public async Task<LoyaltyProgram?> GetById(long id)
        {
            return await dbContext.Loyalties.FirstOrDefaultAsync(e => e.CustomerId == id);
        }


        //create by customer id
        public async Task<LoyaltyProgram> Create(LoyaltyProgram loyalty)
        {
            var result = await dbContext.Loyalties.AddAsync(loyalty);
            await dbContext.SaveChangesAsync();
            return result.Entity;
        }

        //remove by cutomer id
        public async Task<LoyaltyProgram?> RemoveById(long id)
        {
            var obj = await dbContext.Loyalties.FirstOrDefaultAsync(el => el.CustomerId == id);

            if (obj == null)
                 return null;

            dbContext.Loyalties.Remove(obj);
            await dbContext.SaveChangesAsync();
            return obj;
        }
    }
}
