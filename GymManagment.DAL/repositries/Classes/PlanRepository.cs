using GYM_MVC01.Contexts;
using GYM_MVC01.Models;
using GymManagment.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagment.DAL.Repositories.Classes
{
   public class PlanRepository : IPlanRepository
    { // [1] Database Connection
        private readonly GymDbContext dbContext;
        public PlanRepository()
        {

            dbContext = new GymDbContext();
        }

        public async Task<int> AddAsync(Plan plan, CancellationToken ct = default)
        {
            dbContext.Plans.Add(plan);
           return await dbContext.SaveChangesAsync(ct);
        }

        public async Task<int> DeleteAsync(Plan plan, CancellationToken ct = default)
        {
            dbContext.Plans.Remove(plan);
            return await dbContext.SaveChangesAsync(ct);
        }

        public async Task<IEnumerable<Plan>> GetAllAsync(bool tracking = false, CancellationToken ct = default)
        {
            if (tracking) // True: Enable tracking for updates

                return await dbContext.Plans.ToListAsync(ct);

            else

                return await dbContext.Plans.AsNoTracking().ToListAsync(ct);
        }

        public async Task<Plan?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            return await dbContext.Plans.FindAsync(id, ct);
        }

        public Task<int> UpdateAsync(Plan plan, CancellationToken ct = default)
        {
            dbContext.Plans.Update(plan);
            return dbContext.SaveChangesAsync(ct);  
        }
    }
}
