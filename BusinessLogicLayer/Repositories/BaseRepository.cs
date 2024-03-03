using Microsoft.EntityFrameworkCore;
using System;
using DataLayer.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer._ٌRepositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected ApplicationDbContext _context;

        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public T GetById(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }
        public T Find(Expression<Func<T, bool>> criteria, string[] include = null)
        {
            IQueryable<T> query = _context.Set<T>();
            if (include != null)
            {
                foreach (var includeItem in include)
                {
                    query = query.Include(includeItem);
                }
            }
            return query.SingleOrDefault(criteria);
        }
        public List<T> FindAll(Expression<Func<T, bool>> criteria, string[] include = null)
        {
            IQueryable<T> query = _context.Set<T>();
            if (include != null)
            {
                foreach (var includeItem in include)
                {
                    query = query.Include(includeItem);
                }
            }
            return query.Where(criteria).ToList();
        }
        public T Add(T entity)
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();

            return entity;
        }
        public IEnumerable<T> AddRange(IEnumerable<T> entities)
        {
            _context.Set<T>().AddRange(entities);
            _context.SaveChanges();

            return entities;
        }
        public T Update(T entity)
        {
            _context.Update(entity);
            _context.SaveChanges();
            return entity;
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            _context.SaveChanges();
        }
        public void Attach(T entity)
        {
            _context.Set<T>().Attach(entity);
        }
        public int Count()
        {
            return _context.Set<T>().Count();
        }

        public int Count(Expression<Func<T, bool>> criteria)
        {
            return _context.Set<T>().Count(criteria);
        }
    }

   
}
