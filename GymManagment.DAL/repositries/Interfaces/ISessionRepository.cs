using GymManagment.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GymManagment.DAL.repositries.Interfaces
{
  public interface ISessionRepository : IGenericRepository<Session>
    {
       Task<IEnumerable<Session>> GetSessionWithTrainerAndCategory(CancellationToken ct = default);


            
 Task<int>CountOfBookedSlotsAsync(int sessionId , CancellationToken ct = default);

        Task<IEnumerable<Session>>GetSessionsWithTrainerAndCatgoryAsync(
            Expression<Func<Session, bool>>? Predicate = null,
            CancellationToken ct = default);


    Task<Session> GetSessionByIdWithTrainerAndCatgory(int sessionId , CancellationToken ct = default);










    }
}
