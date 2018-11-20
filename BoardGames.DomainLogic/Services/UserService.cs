using BoardGames.DataAccess.Entities;
using BoardGames.DomainLogic.Repositories;
using BoardGames.DomainLogic.Repositories.Interfaces;
using BoardGames.DomainLogic.Services.Interfaces;
using BoardGames.Helpers;
using System;
using System.Text;

namespace BoardGames.DomainLogic.Services
{
    public class UserService : Service<User>, IUserService
    {
        public UserService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        protected override IRepository<User> Repository => UnitOfWork.UserRepository;

        public User Authenticate(string username, string password)
        {
            var user = UnitOfWork.UserRepository.FindByUsername(username);
            if (user == null)
                throw new AppException("The username or password is not correct");

            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                throw new AppException("The username or password is not correct");

            return user;
        }

        public User Register(User user, string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new AppException("Password is required");

            if (UnitOfWork.UserRepository.FindByUsername(user.Username) != null)
                throw new AppException($"Username {user.Username} is already taken");

            CreatePasswordHash(password, out var passwordHash, out var passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            AddOrUpdate(user);

            return user;
        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException(nameof(password));
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", nameof(password));

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", nameof(password));
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }
    }
}
