using GymManagment.DAL.Models;
using GymManagment.DAL.repositries.Interfaces;
using GymSystem.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GymManagment.DAL.repositries.Classes
{
    public class SessionRepository : GenericRepository<Session>, ISessionRepository
    {
        private readonly GymDbcontext _dbcontext;
        public SessionRepository(GymDbcontext dbcontext) : base(dbcontext)  
        {
            _dbcontext = dbcontext;
        }

        public async Task<int> CountOfBookedSlotsAsync(int sessionId, CancellationToken ct = default)
        {
            return await _dbcontext.Bookings.AsNoTracking().CountAsync(B => B.SessionId == sessionId);
        }

        public async Task<Session> GetSessionByIdWithTrainerAndCatgory(int sessionId, CancellationToken ct = default)
        {
            return await _dbcontext.Sessions.AsNoTracking().Include(S => S.Trainer).Include(S => S.Catgory).FirstOrDefaultAsync(S => S.Id == sessionId);
        }

        public Task<IEnumerable<Session>> GetSessionsWithTrainerAndCatgoryAsync(Expression<Func<Session, bool>>? Predicate = null, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Session>> GetSessionWithTrainerAndCategory(CancellationToken ct = default)
        {
           var query = _dbcontext.Sessions.AsNoTracking().Include(S=> S.Trainer).Include(S => S.Catgory );
            return await query.ToListAsync();
        }
    }
}
