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
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity, new()
    {

        private readonly GymDbcontext _dbcontext;
        private readonly DbSet<TEntity> _set;
        public GenericRepository(GymDbcontext dbcontext)
        {
            _dbcontext = dbcontext;
            _set = _dbcontext.Set<TEntity>();
        }

        public async void AddAsync(TEntity entity)
        {
            _set.Add(entity);
         
        }

        public Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken ct = default)
        {
            return _set.AsNoTracking().AnyAsync(predicate, ct);
        }

        public async void DeleteAsync(TEntity entity)
        {
            _set.Remove(entity);
           
        }

        public async Task<TEntity> FristOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, bool tracking = false, CancellationToken ct = default)
        {
            IQueryable<TEntity> Query = tracking ? _set : _set.AsNoTracking();
            return await Query.FirstOrDefaultAsync(predicate);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(bool tracking = false, CancellationToken ct = default)
        {
            IQueryable<TEntity> query = tracking ? _set : _set.AsNoTracking();
            return await query.ToListAsync();
        }

        public async Task<TEntity?> GetByIdAsync(int id, CancellationToken ct = default)
        => await _set.FindAsync(id, ct);

        public async void UpdateAsync(TEntity entity)
        {
         _set.Update(entity);
           
        }
    }
}
