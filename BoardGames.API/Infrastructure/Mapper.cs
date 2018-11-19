using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BoardGames.DataAccess.Entities;
using BoardGames.DomainLogic.Models;
using Microsoft.AspNetCore.Identity;

namespace BoardGames.Infrastructure
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Game, GameResponseModel>();
            CreateMap<GameResponseModel, Game>();

            CreateMap<UserModel, User>();
            CreateMap<User, UserModel>();

            CreateMap<UserManager<UserModel>, UserManager<User>>();
            CreateMap<UserManager<User>, UserManager<UserModel>>();
        }
    }
}
