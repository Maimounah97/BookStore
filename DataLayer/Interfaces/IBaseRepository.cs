using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        T GetById(int id);
        Task<T> GetByIdAsync(int id);
        List<T> GetAll();
        T Find(Expression<Func<T, bool>> criteria, string[] include = null);
        List<T> FindAll(Expression<Func<T, bool>> criteria, string[] include = null);
        T Add(T entity);
        IEnumerable<T> AddRange(IEnumerable<T> entities);
        T Update(T entity);
        void Delete(T entity);
        void Attach(T entity);
        int Count();
        int Count(Expression<Func<T, bool>> criteria);
    }
}
