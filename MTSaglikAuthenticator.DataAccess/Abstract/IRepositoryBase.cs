using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MTSaglikAuthenticator.DataAccess.Abstract
{
    public interface IRepositoryBase<T> where T:class,new()
    {
        T Get(Expression<Func<T, bool>> filter);
        IList<T> GetList(Expression<Func<T, bool>> filter = null);

        T GetFirstOfDefault(Expression<Func<T, bool>> filter = null, string includeProperties = null);
        IQueryable<T> GetAll(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = null);
        T GetById(Guid id);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
