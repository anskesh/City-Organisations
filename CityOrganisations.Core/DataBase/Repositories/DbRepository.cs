using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Core.DataBase;
using Core.DataBase.Models;
using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Core.Repositories
{
    public class DbRepository<T> : IRepository<T> where T:class, IEntity
    {
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

        public IQueryable<T> Get(Expression<Func<T, bool>> predicate)
        {
            return _set.Where(predicate);
        }

        public int Add(T item)
        {
            var info = _set.Add(item);
            _dbContext.SaveChanges();
            
            return info.Entity.Id;
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