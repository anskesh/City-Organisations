using System.Collections.Generic;
using System.Linq;
using CityOrganisations.DataBase.Context;
using CityOrganisations.Models;
using Microsoft.EntityFrameworkCore;

namespace CityOrganisations.Repositories
{
    public class DbRepository<T> : IRepository<T> where T:class, IEntity
    {
        public IQueryable<T> Items => _set;
        
        private readonly OrganizationContext _dbContext;
        private readonly DbSet<T> _set;

        public DbRepository(OrganizationContext dbContext)
        {
            _dbContext = dbContext;
            _set = _dbContext.Set<T>();
        }
        
        public List<T> Get()
        {
            return _set.ToList();
        }

        public IQueryable<T> Get(int id)
        {
            return _set.Where(x => x.Id == id);
        }

        public void Add(T item)
        {
            _set.Add(item);
            _dbContext.SaveChanges();
        }

        public void Remove(int id)
        {
            T item = _set.First(x => x.Id == id);
            
            if (item != null)
            {
                _set.Remove(item);
                _dbContext.SaveChanges();
            }
        }

        public void Update(T item)
        {
            _dbContext.Entry(item).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void Clear()
        {
            _dbContext.RemoveRange(_set.ToList());
            _dbContext.SaveChanges();
        }
    }
}