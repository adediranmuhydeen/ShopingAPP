﻿using Microsoft.EntityFrameworkCore;
using ShopOnline.Entities.Data;
using ShopOnline.Entities.IRepositories;
using System.Linq.Expressions;

namespace ShopOnline.Entities.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> _dbSet;
        public IQueryable<T> queryable;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        /// <summary>
        /// Generic method to add item
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<T> CreateAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            return entity;
        }

        public async Task<T> DeleteAsync(Expression<Func<T, bool>> exp, List<string> include = null)
        {
            IQueryable<T> query = _dbSet;
            if (include != null)
            {
                foreach (var included in include)
                {
                    query = query.Include(included);
                }
            }
            var entity = await _dbSet.FirstOrDefaultAsync(exp);
            if (entity == null)
            {
                return null;
            }
            _dbSet.Remove(entity);
            return entity;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var entities = await _dbSet.ToListAsync();
            return entities;
        }

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, List<string> include = null)
        {
            IQueryable<T> query = _dbSet;
            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            if (include != null)
            {
                foreach (var included in include)
                {
                    query = query.Include(included);
                }
            }
            if (orderBy != null)
            {
                query = orderBy(query);
            }
            return await query.AsNoTracking().ToListAsync();

        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> exp, List<string> include = null)
        {
            IQueryable<T> query = _dbSet;
            if (include != null)
            {
                foreach (var included in include)
                {
                    query = query.Include(included);
                }
            }
            return await query.AsNoTracking().FirstOrDefaultAsync(exp);

        }

        public async Task<T> UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            return entity;
        }
    }
}
