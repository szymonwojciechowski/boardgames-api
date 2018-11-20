using AutoMapper;
using BoardGames.DataAccess.Entities;
using BoardGames.DomainLogic.Repositories;
using BoardGames.DomainLogic.Repositories.Interfaces;
using BoardGames.DomainLogic.Services.Interfaces;

namespace BoardGames.DomainLogic.Services
{
    public class GameService : Service<Game>, IGameService
    {
        private readonly IMapper _mapper;

        public GameService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork)
        {
            _mapper = mapper;
        }

        protected override IRepository<Game> Repository => UnitOfWork.GameRepository;
    }
}
