using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Transofast.Domain.Entities.Abstract.Interface;
using Transofast.Domain.Enum;
using Transofast.Domain.IRepositories;
using Transofast.Infrastructure.DataAccess;

namespace Transofast.Infrastructure.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class, IBaseEntity
    {
        private readonly TransofastDbContext _dbContext;
        protected DbSet<T> _table;

        public BaseRepository(TransofastDbContext DbContext)
        {
            _dbContext = DbContext;
            _table = _dbContext.Set<T>();
        }


        public async Task Add(T item)
        {
            await _table.AddAsync(item);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> Any(Expression<Func<T, bool>> expression)
        {
            return await _table.AnyAsync(expression);
        }

        public async Task Delete(T item)
        {
            item.Status = Status.Deleted; //Change status to deleted
            item.ModifiedDate = DateTime.Now;
            await Update(item);
        }

        public async Task<List<T>> GetAll()
        {
            return await _table.ToListAsync();
        }

        public async Task<T> GetById(Expression<Func<T, bool>> expression)
        {
            return await _table.Where(expression).AsNoTracking().FirstAsync();
        }

        public async Task<List<T>> GetDefaults(Expression<Func<T, bool>> expression)
        {
            return await _table.Where(expression).ToListAsync();
        }

        public async Task Update(T item)
        {
            _dbContext.Entry<T>(item).State = EntityState.Modified;//Update status
            await _dbContext.SaveChangesAsync();
        }
    }
}
