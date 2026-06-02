using Domain.Repository;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Service
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly MainContext _context;
        private readonly DbSet<T> _dbSet;
        public GenericRepository(MainContext contxt)
        {
            _context = contxt;
            _dbSet = _context.Set<T>();
        }
        public async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.ToListAsync();

        public async Task<T?> GetByIdAsync(object id) => await _dbSet.FindAsync(id);

        public async Task<T?> GetAsync(Expression<Func<T, bool>> predicate) =>
            await _dbSet.FirstOrDefaultAsync(predicate);

        public async Task AddAsync(T entity) => await _dbSet.AddAsync(entity);

        public void Update(T entity) => _dbSet.Update(entity);

        public void Delete(T entity) => _dbSet.Remove(entity);

        public void Delete(object id)
        {
            var entity = GetByIdAsync(id);
            if (entity != null)
                Delete(entity);
        }
        public async Task AddRangeAsync(IEnumerable<T> entities) => await _dbSet.AddRangeAsync(entities);

        public void RemoveRange(IEnumerable<T> entities) => _dbSet.RemoveRange(entities);

        public async Task<int> CountAsync() => await _dbSet.CountAsync();

        public async Task<int> CountAsync(Expression<Func<T, bool>> predicate) =>
         await _dbSet.CountAsync(predicate);

        public IQueryable<T> GetQueryable()
        {
            return _dbSet.AsQueryable();
        }
    }
}
