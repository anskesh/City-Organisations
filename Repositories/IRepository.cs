using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using CityOrganisations.Models;

namespace CityOrganisations.Repositories
{
    public interface IRepository<T> where T:IEntity
    {
        List<T> Get();
        IQueryable<T> Get(Expression<Func<T, bool>> predicate);
        void Add(T item);
        void Remove(int id);

        void Update(T item);
        void Clear();
    }
}