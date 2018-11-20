using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoardGames.DataAccess.Contexts;
using BoardGames.DataAccess.Entities;
using BoardGames.DomainLogic.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace BoardGames.DomainLogic.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        //private readonly UserManager<User> _userManager;

        //public UserRepository(UserManager<User> userManager)
        //{
        //    _userManager = userManager;
        //}

        //public IQueryable<User> Get() => _userManager.Users;

        //public User GetByEmail(string email) => _userManager.Users.First(u => u.Email == email);

        //public Task<IdentityResult> Create(User user, string password)
        //{
        //    return _userManager.CreateAsync(user, password);
        //}

        //public async Task<IdentityResult> Delete(User user)
        //{
        //    return await _userManager.DeleteAsync(user);
        //}

        //public async Task<IdentityResult> Update(User user)
        //{
        //    return await _userManager.UpdateAsync(user);
        //}

        //public UserManager<User> GetUserManager()
        //{
        //    return _userManager;
        //}
        public UserRepository(DataContext context) : base(context)
        {

        }

        public User FindByUsername(string username)
        {
            return Set.SingleOrDefault(x => x.Username == username);
        }
    }
}
