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

        //business
        public async Task<IEnumerable<Business>> GetAll()
        {
            return await dbContext.Businesses.ToListAsync();
        }

        public async Task<Business?> GetById(long id)
        {
            return await dbContext.Businesses.FirstOrDefaultAsync(e => e.Id == id);
        }


        public async Task<Business> Create(Business business)
        {
            var result = await dbContext.Businesses.AddAsync(business);
            await dbContext.SaveChangesAsync();
            return result.Entity;
        } 

        public async Task<Business?> Update(Business business)
        {
            //dbContext.Entry(business).State = EntityState.Modified;
            var toChange = await dbContext.Businesses.FirstOrDefaultAsync(e => e.Id == business.Id);

            if(toChange == null)
                return null;

            dbContext.Entry<Business>(toChange).CurrentValues.SetValues(business);
            await dbContext.SaveChangesAsync();
            return toChange;
        }

        public async Task<Business?> RemoveById(long id)
        {
            var obj = await dbContext.Businesses.FirstOrDefaultAsync(el => el.Id == id);

            if (obj == null)
                return null;

            dbContext.Businesses.Remove(obj);
            await dbContext.SaveChangesAsync();
            return obj;
        }

        //addresses
        public async Task<Address?> UpdateAddress(Address address)
        {
            var toChange = await dbContext.Addresses.FirstOrDefaultAsync(e => e.Id == address.Id);

            if (toChange == null)
                 return null;

            dbContext.Entry<Address>(toChange).CurrentValues.SetValues(address);
            await dbContext.SaveChangesAsync();
            return toChange;
        }

        public async Task<Address?> GetAddress(long id)
        {
            return await dbContext.Addresses.FirstOrDefaultAsync(e => e.Id == id);
        }


        //privileges
        public async Task<ManagePrivilege> CreatePrivilege(ManagePrivilege managePrivilege)
        {
            var result = await dbContext.Privileges.AddAsync(managePrivilege);
            await dbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<ManagePrivilege?> RemovePrivilegeByIds(long id, long id2)
        {
            var obj = await dbContext.Privileges.FirstOrDefaultAsync(el => el.BusinessId == id && el.EmployeeId == id2);

            if (obj == null)
                return null;

            dbContext.Privileges.Remove(obj);
            await dbContext.SaveChangesAsync();
            return obj;
        }

        public async Task<ManagePrivilege?> FindPrivilegesByIds(long id, long id2)
        {
            return await dbContext.Privileges.FirstOrDefaultAsync(el => el.BusinessId == id && el.EmployeeId == id2);
        }
    }
}
