using GymManagment.DAL.Models;
using GymManagment.DAL.repositries.Interfaces;
using GymSystem.Context;

namespace GymManagment.DAL.repositries.Classes
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly GymDbcontext _dbcontext;
        private readonly Dictionary<string, object> _repositories;

        public UnitOfWork(
            GymDbcontext dbcontext,
            ISessionRepository sessionRepository)
        {
            _dbcontext = dbcontext;
            SessionRepository = sessionRepository;
            _repositories = new Dictionary<string, object>();
        }

        public ISessionRepository SessionRepository { get; }

        public IGenericRepository<TEntity> GetRepository<TEntity>()
            where TEntity : BaseEntity, new()
        {
            var typeName = typeof(TEntity).Name;

            if (_repositories.TryGetValue(typeName, out var repository))
            {
                return (IGenericRepository<TEntity>)repository;
            }

            var repo = new GenericRepository<TEntity>(_dbcontext);

            _repositories[typeName] = repo;

            return repo;
        }

        public async Task<int> SaveChangesAsync(
            CancellationToken ct = default)
        {
            return await _dbcontext.SaveChangesAsync(ct);
        }
    }
}