﻿using _3lab_komanda32.Models;
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

        public async Task<Business> Create(Business business)
        {
            var result = await dbContext.Businesses.AddAsync(business);
            await dbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Business> Update(Business business)
        {
            dbContext.Entry(business).State = EntityState.Modified;
            await dbContext.SaveChangesAsync();
            return business;

        }
        
    }
}