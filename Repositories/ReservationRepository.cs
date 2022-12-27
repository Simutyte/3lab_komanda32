using _3lab_komanda32.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.OpenApi.Models;

namespace _3lab_komanda32.Repositories
{
    public class ReservationRepository
    {
        private readonly ApiDbContext dbContext;

        public ReservationRepository(ApiDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<Reservation>?> GetByCustomerId(long id)
        {
            return await dbContext.Reservations.Where(el => el.CustomerId == id).ToListAsync();
        }

        public async Task<Reservation>? GetById(long id)
        {
            return await dbContext.Reservations.FirstOrDefaultAsync(el => el.Id == id);
        }

        public async Task<Reservation> Update(Reservation reservation)
        {
            dbContext.Entry(reservation).State = EntityState.Modified;
            await dbContext.SaveChangesAsync();
            return reservation;
        }

        public async Task<EntityState?> RemoveById(long id)
        {
            var obj = await dbContext.Reservations.FirstOrDefaultAsync(el => el.Id == id);

            if (obj == null)
            {
                return null;
            }

            var res = dbContext.Reservations.Remove(obj);
            await dbContext.SaveChangesAsync();

            return res.State;
        }
    }
}
