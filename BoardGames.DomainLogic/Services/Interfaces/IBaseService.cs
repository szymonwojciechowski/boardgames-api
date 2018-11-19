using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BoardGames.DataAccess.Entities.Base;

namespace BoardGames.DomainLogic.Services.Interfaces
{
    public interface IBaseService<T> where T : Entity
    {
        Task<IEnumerable<T>> GetAsync();

        Task<T> GetById(int id);

        IEnumerable<T> Where(Expression<Func<T, bool>> exp);

        void AddOrUpdate(T entry);

        void Remove(int id);
    }
}
