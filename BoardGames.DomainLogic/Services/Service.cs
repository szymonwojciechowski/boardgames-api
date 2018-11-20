using BoardGames.DataAccess.Entities.Base;
using BoardGames.DomainLogic.Repositories;
using BoardGames.DomainLogic.Repositories.Interfaces;
using BoardGames.DomainLogic.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BoardGames.DomainLogic.Services
{
    public abstract class Service<TEntity> : IService<TEntity> where TEntity : Entity
    {
        protected readonly IUnitOfWork UnitOfWork;

        protected Service(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        protected abstract IRepository<TEntity> Repository { get; }

        public async Task<IEnumerable<TEntity>> GetAsync()
        {
            return await Repository.GetAll();
        }

        public async Task<TEntity> GetById(int id)
        {
            return await Repository.GetById(id);
        }

        public IEnumerable<TEntity> Where(Expression<Func<TEntity, bool>> exp)
        {
            return Repository.Where(exp);
        }

        public void AddOrUpdate(TEntity entry)
        {
            var targetRecord = Repository.GetById(entry.Id).Result;
            var exists = targetRecord != null;

            if (exists)
            {
                entry.DateModified = DateTime.UtcNow;
                Repository.Update(entry);
                UnitOfWork.SaveChanges();
            }

            entry.DateCreated = DateTime.UtcNow;
            Repository.Insert(entry);
            UnitOfWork.SaveChanges();
        }

        public void Remove(int id)
        {
            var label = Repository.GetById(id).Result;
            Repository.Delete(label);
            UnitOfWork.SaveChanges();
        }
    }
}
