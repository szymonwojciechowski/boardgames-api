using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BoardGames.DataAccess.Entities;
using BoardGames.DomainLogic.Models;

namespace BoardGames.DomainLogic.Services.Interfaces
{
    public interface IGameService : IService<Game>
    {
        //void AddOrUpdate(GameResponseModel entry);
        //Task<IEnumerable<GameResponseModel>> GetAsync();
        //Task<GameResponseModel> GetById(int id);
        //void Remove(int id);
        //IEnumerable<GameResponseModel> Where(Expression<Func<Game, bool>> exp);
    }
}
