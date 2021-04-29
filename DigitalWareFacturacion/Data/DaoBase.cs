using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
using DigitalWareFacturacion.Utils;

namespace DigitalWareFacturacion.Data
{
    public class DaoBase<TEntity, TContext> where TEntity : class where TContext : DbContext
    {
        protected readonly TContext _context;
        public DaoBase(TContext context)
        {
            _context = context;
        }
        public DaoBase<TEntity, TContext> Create(TEntity obj)
        {
            _context.Add(obj);
            _context.SaveChanges();
            return this;
        }
        public TEntity Get(Expression<Func<TEntity, bool>> callback)
        {
            return _context.Set<TEntity>().Where(callback).FirstOrDefault();
        }

        public void Update<T>(TEntity obj, Expression<Func<TEntity, bool>> search, Func<TEntity, bool> updateExpression = null)
        {
            var stored = Get(search);
            if (stored == null)
            {
                Create(obj);
            }
            else
            {
                UpdateIf(obj, search, updateExpression, stored);
                PropertyCopy.Copy(stored, obj);
            }
        }

        private void UpdateIf(TEntity obj, Expression<Func<TEntity, bool>> search, Func<TEntity, bool> updateExpression, TEntity stored)
        {

            if (updateExpression == null)
            {
                updateExpression = search.Compile();
            }

            if (!updateExpression(stored))
            {
                _context.Entry(obj).State = EntityState.Modified;
                _context.SaveChanges();
            }
        }
    }
}
