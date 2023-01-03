using Microsoft.EntityFrameworkCore;
using MTSaglikAuthenticator.DataAccess.Abstract;
using MTSaglikAuthenticator.DataAccess.Concrete.EntityFramework.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MTSaglikAuthenticator.DataAccess.Concrete
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class, new()
    {
        private readonly MTSaglikAuthenticatorDemoContext _context;
        internal DbSet<T> _dbSet;



        public RepositoryBase(MTSaglikAuthenticatorDemoContext context)
        {
            _context = context;
            this._dbSet = _context.Set<T>();
        }

        public void Add(T entity)
        {

            var addedEntity = _context.Entry(entity);
            addedEntity.State = EntityState.Added;
            _context.SaveChanges();

        }

        public void Delete(T entity)
        {

            var deletedEntity = _context.Entry(entity);
            deletedEntity.State = EntityState.Deleted;
            _context.SaveChanges();

        }

        public T Get(Expression<Func<T, bool>> filter)
        {

            return _context.Set<T>().SingleOrDefault(filter);

        }

     

        public T GetById(Guid id)
        {

            return _context.Set<T>().Find(id);

        }

        public T GetFirstOfDefault(Expression<Func<T, bool>> filter = null, string includeProperties = null)
        {
            IQueryable<T> query = _dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (includeProperties != null)
            {
                foreach (var property in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(property);
                }
            }
            return query.FirstOrDefault();
        }


        public IQueryable<T> GetAll(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = null)
        {
            IQueryable<T> query = _dbSet;
            if (filter != null)
            {   
                query = query.Where(filter);
            }
            if (includeProperties != null)
            {
                foreach (var property in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(property);
                }
            }
            if (orderBy != null)
            {
                return orderBy(query);
            }
            return query;
        }


        public IList<T> GetList(Expression<Func<T, bool>> filter = null)
        {

            return filter == null
                ? _context.Set<T>().ToList()
                : _context.Set<T>().Where(filter).ToList();

        }

        public void Update(T entity)
        {

            var updatedEntity = _context.Entry(entity);
            updatedEntity.State = EntityState.Modified;
            _context.SaveChanges();

        }
    }
}
