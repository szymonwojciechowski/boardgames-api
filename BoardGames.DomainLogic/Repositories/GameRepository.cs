using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BoardGames.DataAccess.Contexts;
using BoardGames.DataAccess.Entities;
using BoardGames.DomainLogic.Infrastructure.ErrorHandler;
using BoardGames.DomainLogic.Repositories.Interfaces;

namespace BoardGames.DomainLogic.Repositories
{
    public class GameRepository : Repository<Game>, IGameRepository
    {
        public GameRepository(DataContext context) : base(context)
        {
        }
    }
}
