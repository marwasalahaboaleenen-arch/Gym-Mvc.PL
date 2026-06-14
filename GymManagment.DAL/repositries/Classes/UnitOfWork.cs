using GymManagment.DAL.Models;
using GymManagment.DAL.repositries.Interfaces;
using GymSystem.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagment.DAL.repositries.Classes
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly GymDbcontext _dbcontext;
        private readonly Dictionary<string, object> _repositories = [];
        public UnitOfWork(GymDbcontext dbcontext)
        {
            
            _dbcontext = dbcontext;
        }
        public IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity, new()
        {
           var TypeName = typeof(TEntity).Name;
            
            if(_repositories.TryGetValue(TypeName, out object? value))
              return (IGenericRepository<TEntity>)value;
            else
            {
             var repo = new GenericRepository<TEntity>(_dbcontext);
                _repositories[TypeName] = repo;
                return repo;
            }






        }

        public async Task<int> SaveChangesAsync(CancellationToken ct = default)
        
           => await _dbcontext.SaveChangesAsync(ct);
        
    }
}
