using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BoardGames.DataAccess.Entities;
using BoardGames.DomainLogic.Models;
using BoardGames.DomainLogic.Services.Interfaces;

namespace BoardGames.DomainLogic.Services
{
    public class GameService : IGameService
    {
        private readonly IBaseService<Game> _service;
        private readonly IMapper _mapper;

        public GameService(IBaseService<Game> service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GameResponseModel>> GetAsync()
        {
            var results = await _service.GetAsync();
            return results.Select(t => _mapper.Map<Game, GameResponseModel>(t));
        }

        public async Task<GameResponseModel> GetById(int id)
        {
            var result = await _service.GetById(id);
            return _mapper.Map<Game, GameResponseModel>(result);
        }
        public void AddOrUpdate(GameResponseModel entry)
        {
            _service.AddOrUpdate(_mapper.Map<GameResponseModel, Game>(entry));
        }

        public void Remove(int id)
        {
          _service.Remove(id);
        }

        public IEnumerable<GameResponseModel> Where(Expression<Func<Game, bool>> exp)
        {
            var whereResult = _service.Where(exp).ToList();
            return _mapper.Map<List<Game>, List<GameResponseModel>>(whereResult).AsEnumerable();
        }
    }
}
