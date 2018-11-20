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

        /// <summary>
        /// Get all games.
        /// </summary>
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IEnumerable<GameResponseModel>> GetAll()
        {
            //if (!string.IsNullOrEmpty(title))
            //{
            //   return _gameService.Where(g => g.Title.Contains(title));
            //}
            var results = await _gameService.GetAsync();
            return results.Select(t => _mapper.Map<Game, GameResponseModel>(t));
        }

        /// <summary>
        /// Get game with specific id.
        /// </summary>
        [HttpGet("{id:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<GameResponseModel> Get(int id)
        {
            return _mapper.Map<Game, GameResponseModel>(await _gameService.GetById(id));
        }
    }
}