using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Core.DataBase.Models;
using Core.Models;

namespace Core.Repositories
{
    public interface IRepository<T> where T:IEntity
    {
        List<T> Get();
        IQueryable<T> Get(Expression<Func<T, bool>> predicate);
        int Add(T item);
        void Remove(int id);

        void Update(T item);
        void Clear();
    }
}