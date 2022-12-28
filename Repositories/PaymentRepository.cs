using _3lab_komanda32.Models;
using Microsoft.EntityFrameworkCore;

namespace _3lab_komanda32.Repositories
{
    public class PaymentRepository
    {
        private readonly ApiDbContext dbContext;

        public PaymentRepository(ApiDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Payment?> GetById(long id)
        {
            return await dbContext.Payments.FirstOrDefaultAsync(el => el.Id == id);
        }

        public async Task<Payment> Create(Payment payment)
        {
            var res = await dbContext.Payments.AddAsync(payment);
            await dbContext.SaveChangesAsync();
            return res.Entity;
        }

        public async Task<Payment> Update(Payment payment)
        {
            dbContext.Entry(payment).State = EntityState.Modified;
            await dbContext.SaveChangesAsync();
            return payment;
        }

        public async Task<EntityState?> RemoveById(long id)
        {
            var obj = await dbContext.Payments.FirstOrDefaultAsync(el => el.Id == id);

            if (obj == null)
            {
                return null;
            }

            var res = dbContext.Payments.Remove(obj);
            await dbContext.SaveChangesAsync();

            return res.State;
        }
    }
}
