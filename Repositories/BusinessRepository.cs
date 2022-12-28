using _3lab_komanda32.Models;
using Microsoft.EntityFrameworkCore;

namespace _3lab_komanda32.Repositories
{
    public class BusinessRepository
    {
        private readonly ApiDbContext dbContext;

        public BusinessRepository(ApiDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        //Basic tasks

        public async Task<IEnumerable<Business>> GetAll()
        {
            return await dbContext.Businesses.ToListAsync();
        }

        public async Task<Business?> GetById(long id)
        {
            return await dbContext.Businesses.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<Address?> GetAddress(long id)
        {
            return await dbContext.Addresses.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<Business> Create(Business business)
        {
            var result = await dbContext.Businesses.AddAsync(business);
            await dbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<ManagePrivilege> CreatePrivilege(ManagePrivilege managePrivilege)
        {
            var result = await dbContext.Privileges.AddAsync(managePrivilege);
            await dbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Business> Update(Business business)
        {
            dbContext.Entry(business).State = EntityState.Modified;
            await dbContext.SaveChangesAsync();
            return business;

        }

        public async Task<Address> UpdateAddress(Address address)
        {
            dbContext.Entry(address).State = EntityState.Modified;
            await dbContext.SaveChangesAsync();
            return address;

        }

        public async Task<EntityState?> RemoveById(long id)
        {
            var obj = await dbContext.Businesses.FirstOrDefaultAsync(el => el.Id == id);

            if (obj == null)
            {
                return null;
            }

            var res = dbContext.Businesses.Remove(obj);
            await dbContext.SaveChangesAsync();

            return res.State;
        }

        public async Task<EntityState?> RemovePrivilegeByIds(long id, long id2)
        {
            var obj = await dbContext.Privileges.FirstOrDefaultAsync(el => el.BusinessId == id && el.EmployeeId == id2);

            if (obj == null)
            {
                return null;
            }

            var res = dbContext.Privileges.Remove(obj);
            await dbContext.SaveChangesAsync();

            return res.State;
        }
    }
}
