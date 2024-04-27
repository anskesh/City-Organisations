using System.Collections.Generic;
using System.Linq;
using CityOrganisations.Models;

namespace CityOrganisations.Repositories
{
    public interface IRepository<T> where T:IEntity
    {
        public IQueryable<T> Items { get; }
        
        List<T> Get();
        IQueryable<T> Get(int id);
        void Add(T item);
        void Remove(int id);

        void Update(T item);
        void Clear();
    }
}