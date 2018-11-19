﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoardGames.DataAccess.Entities;
using BoardGames.DomainLogic.Infrastructure.ErrorHandler;
using BoardGames.DomainLogic.Repositories;
using BoardGames.DomainLogic.Repositories.Interfaces;
using BoardGames.DomainLogic.Services;
using BoardGames.DomainLogic.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace BoardGames.Infrastructure
{
    internal static class Installer
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IBaseRepository<Game>, BaseRepository<Game>>();
            services.AddTransient<IBaseService<Game>, BaseService<Game>>();
            services.AddTransient<IGameService, GameService>();
            services.AddTransient<IErrorHandler, ErrorHandler>();
        }
    }
}
