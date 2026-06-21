using GymManagment.DAL.Models;
using GymManagment.DAL.repositries.Interfaces;
using GymSystem.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GymManagment.DAL.repositries.Classes
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity>
        where TEntity : BaseEntity, new()
    {
        private readonly GymDbcontext _dbcontext;
        private readonly DbSet<TEntity> _set;

        public GenericRepository(GymDbcontext dbcontext)
        {
            _dbcontext = dbcontext;
            _set = _dbcontext.Set<TEntity>();
        }

        public void AddAsync(TEntity entity)
        {
            _set.Add(entity);
        }

        public async Task<bool> AnyAsync(
            Expression<Func<TEntity, bool>> predicate,
            CancellationToken ct = default)
        {
            return await _set.AsNoTracking().AnyAsync(predicate, ct);
        }

        public void DeleteAsync(TEntity entity)
        {
            _set.Remove(entity);
        }

        public async Task<TEntity?> FristOrDefaultAsync(
            Expression<Func<TEntity, bool>> predicate,
            bool tracking = false,
            CancellationToken ct = default)
        {
            IQueryable<TEntity> query = tracking
                ? _set
                : _set.AsNoTracking();

            return await query.FirstOrDefaultAsync(predicate, ct);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(
            bool tracking = false,
            CancellationToken ct = default)
        {
            IQueryable<TEntity> query = tracking
                ? _set
                : _set.AsNoTracking();

            return await query.ToListAsync(ct);
        }

        public async Task<TEntity?> GetByIdAsync(
            int id,
            CancellationToken ct = default)
        {
            return await _set.FindAsync(new object[] { id }, ct);
        }

        public void UpdateAsync(TEntity entity)
        {
            _set.Update(entity);
        }
    }
}