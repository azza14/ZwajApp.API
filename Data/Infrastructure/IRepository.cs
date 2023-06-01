using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.InteropServices;

namespace ZwajApp.API.Data.Infrastructure
{
    public interface IRepository<T> where T : class
    {
        List<T> GetAll();
        IEnumerable<T> GetAll(params Expression<Func<T, object>>[] including);
        T GetById(int id);
        void Add(T model);
        T InsertWithReturn(T model);
        List<T> AddRange(List<T> models);
        void Update(T model);
        void Delete(int id);
        bool IsExists(Expression<Func<T, bool>> expression);
        T GetOne(Expression<Func<T, bool>> expression);
        IList<T> GetList(Expression<Func<T, bool>> expression);
        IList<T> GetByCondition(Expression<Func<T, bool>> expression);
    }
}
