using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BoardGames.DataAccess.Entities.Base;
using BoardGames.DomainLogic.Repositories.Interfaces;
using BoardGames.DomainLogic.Services.Interfaces;

namespace BoardGames.DomainLogic.Services
{
    public class BaseService<T> : IBaseService<T> where T : Entity
    {
        private readonly IBaseRepository<T> _repository;

        public BaseService(IBaseRepository<T> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<T>> GetAsync()
        {
            return await _repository.GetAll();
        }

        public async Task<T> GetById(int id)
        {
            return await _repository.GetById(id);
        }

        public IEnumerable<T> Where(Expression<Func<T, bool>> exp)
        {
            return _repository.Where(exp);
        }

        public void AddOrUpdate(T entry)
        {
            var targetRecord = _repository.GetById(entry.Id).Result;
            var exists = targetRecord != null;

            if (exists)
            {
                entry.DateModified = DateTime.UtcNow;
                _repository.Update(entry);
                return;
            }

            entry.DateCreated = DateTime.UtcNow;
            _repository.Insert(entry);
        }

        public void Remove(int id)
        {
            var label = _repository.GetById(id).Result;
            _repository.Delete(label);
        }
    }
}
