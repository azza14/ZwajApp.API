using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using ZwajApp.API.Data.DAL;

namespace ZwajApp.API.Data.Infrastructure
{
    public class RepositoryBase<T> : IRepository<T> where T : class
    {
        private readonly DataContext context;
        private DataContext _context;
        private DbSet<T> table = null;
        public RepositoryBase(DataContext context)
        {
            _context = context;
            table = _context.Set<T>();
        }
        public void Add(T model)
        {
            table.Add(model);
        }
        public void Delete(int id)
        {
            var item = GetById(id);
            table.Remove(item);
        }
        public void Update(T model)
        {
            table.Attach(model);
            _context.Entry(model).State = EntityState.Modified;
        }
        public List<T> GetAll()
        {
            return table.ToList();
        }
        public IEnumerable<T> GetAll(params Expression<Func<T, object>>[] including)
        {
            var query = table.AsQueryable();
            if (including != null)
            {
                foreach (var include in including)
                    query = query.Include(include);
            }
            return query;
        }
        public IList<T> GetByCondition(Expression<Func<T, bool>> expression)
        {
            return this._context.Set<T>().Where(expression).ToList();
        }
        public T GetById(int id)
        {
            return table.Find(id);
        }
        public IList<T> GetList(Expression<Func<T, bool>> expression)
        {
            return this._context.Set<T>().Where(expression).ToList();
        }
        public T GetOne(Expression<Func<T, bool>> expression)
        {
            return this._context.Set<T>().Where(expression).FirstOrDefault();
        }
        public List<T> AddRange(List<T> models)
        {
            table.AddRange(models);
            return models;
        }
        public T InsertWithReturn(T model)
        {
            table.Add(model);
            return model;
        }
        public bool IsExists(Expression<Func<T, bool>> expression)
        {
            return _context.Set<T>().Any(expression);
        }


    }
}