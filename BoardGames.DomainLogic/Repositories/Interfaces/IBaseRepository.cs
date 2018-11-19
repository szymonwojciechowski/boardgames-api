using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BoardGames.DataAccess.Entities.Base;

namespace BoardGames.DomainLogic.Repositories.Interfaces
{
    public interface IBaseRepository<T> where T : Entity
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(int id);
        IEnumerable<T> Where(Expression<Func<T, bool>> exp);
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
