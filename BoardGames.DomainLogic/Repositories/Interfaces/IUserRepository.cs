using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoardGames.DataAccess.Entities;
using Microsoft.AspNetCore.Identity;

namespace BoardGames.DomainLogic.Repositories.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        //IQueryable<User> Get();
        //User GetByEmail(string email);
        //Task<IdentityResult> Create(User user, string password);
        //Task<IdentityResult> Delete(User user);
        //Task<IdentityResult> Update(User user);
        //UserManager<User> GetUserManager();
        User FindByUsername(string username);
    }
}
