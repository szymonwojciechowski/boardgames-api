using System;
using System.Collections.Generic;
using System.Text;
using BoardGames.DataAccess.Entities;

namespace BoardGames.DomainLogic.Services.Interfaces
{
    public interface IUserService : IService<User>
    {
        User Authenticate(string username, string password);
        User Register(User user, string password);
    }
}
