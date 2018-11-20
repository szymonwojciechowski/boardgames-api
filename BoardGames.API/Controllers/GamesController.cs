using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BoardGames.DataAccess.Entities;
using BoardGames.DomainLogic.Infrastructure.ErrorHandler;
using BoardGames.DomainLogic.Models;
using BoardGames.DomainLogic.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BoardGames.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class GamesController : Controller
    {
        private readonly IGameService _gameService;
        private readonly IErrorHandler _errorHandler;
        private readonly IMapper _mapper;

        public GamesController(IGameService gameService, IErrorHandler errorHandler, IMapper mapper)
        {
            _gameService = gameService;
            _errorHandler = errorHandler;
            _mapper = mapper;
        }


        // GET: api/games
        [HttpGet]
        [AllowAnonymous]
        public async Task<IEnumerable<GameResponseModel>> Get(string title)
        {
            //if (!string.IsNullOrEmpty(title))
            //{
            //   return _gameService.Where(g => g.Title.Contains(title));
            //}
            var results = await _gameService.GetAsync();
            return results.Select(t => _mapper.Map<Game, GameResponseModel>(t));
        }

        // GET: api/games/5
        [HttpGet("{id:int}")]
        public async Task<GameResponseModel> Get(int id)
        {
            return _mapper.Map<Game, GameResponseModel>(await _gameService.GetById(id));
        }
    }
}