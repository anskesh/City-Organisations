using System;
using System.Collections.ObjectModel;
using Core.Models;
using Core.Repositories;
using Core.DataBase.Models;

namespace Core.DataBase.Services
{
    public abstract class DatabaseService<TEntity, TModel> : IDatabaseService<TModel> where TEntity: IEntity where TModel: Model
    {
        public Action<TModel> AddEvent;
        public Action<TModel, TModel> UpdateEvent;
        public Action<TModel> RemoveEvent;

        public ObservableCollection<TModel> Items { get; }

        protected IRepository<TEntity> Repository;

        protected DatabaseService(IRepository<TEntity> repository)
        {
            Repository = repository;
            Items = new ObservableCollection<TModel>();
        }

        public virtual void Add(TModel model) {}
        public virtual void Update(TModel model, TModel newModel) {}
        public virtual void Remove(TModel model) {}
    }

    public interface IDatabaseService<T> where T : Model
    {
        ObservableCollection<T> Items { get; }

        void Add(T model);
        void Update(T model, T newModel);
        void Remove(T model);
    }
}