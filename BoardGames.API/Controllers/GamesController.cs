using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public GamesController(IGameService gameService, IErrorHandler errorHandler)
        {
            _gameService = gameService;
            _errorHandler = errorHandler;
        }


        // GET: api/games
        [HttpGet]
        public async Task<IEnumerable<GameResponseModel>> Get(string title)
        {
            if (!string.IsNullOrEmpty(title))
            {
               return _gameService.Where(g => g.Title.Contains(title));
            }
            return await _gameService.GetAsync();
        }

        // GET: api/games/5
        [HttpGet("{id:int}")]
        public async Task<GameResponseModel> Get(int id)
        {
            return await _gameService.GetById(id);
        }

    }
}