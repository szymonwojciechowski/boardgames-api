using BoardGames.DataAccess.Contexts;
using BoardGames.DataAccess.Entities;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardGames.DataAccess
{
    public class BoardGamesSeeder
    {
        private readonly DataContext _context;

        public BoardGamesSeeder(DataContext context)
        {
            _context = context;
        }

        public void Seed()
        {
            _context.Database.EnsureCreated();
            var user1 = _context.Users.SingleOrDefault(u => u.Email == "szymon.wojciechowski27@gmail.com");
            if (user1 == null)
            {
                user1 = new User
                {
                    FirstName = "Szymon",
                    LastName = "Wojciechowski",
                    Email = "szymon.wojciechowski27@gmail.com",
                    Username = "szymek",
                };
                CreatePasswordHash("admin", out var passwordHash, out var passwordSalt);
                user1.PasswordHash = passwordHash;
                user1.PasswordSalt = passwordSalt;
                _context.Add(user1);
            }
            var user2 = _context.Users.SingleOrDefault(u => u.Email == "justyna.wojciechowska04@gmail.com");
            if (user2 == null)
            {
                user2 = new User
                {
                    FirstName = "Justyna    ",
                    LastName = "Wojciechowska",
                    Email = "justyna.wojciechowska04@gmail.com",
                    Username = "justynka"
                };

                CreatePasswordHash("admin", out var passwordHash, out var passwordSalt);
                user2.PasswordHash = passwordHash;
                user2.PasswordSalt = passwordSalt;
                _context.Add(user2);
            }

            if (!EnumerableExtensions.Any(_context.Games))
            {
                var games = new List<Game>
                {
                    new Game
                    {
                        Title = "Catan Test",
                        Description = "description",
                        DateCreated = DateTime.Now
                    },
                    new Game
                    {
                        Title = "7 wonders",
                        Description = "description",
                        DateCreated = DateTime.Now
                    }
                };
                _context.AddRange(games);

            }

            if (!EnumerableExtensions.Any(_context.Meetings))
            {
                var meetigns = new List<Meeting>
                {
                    new Meeting
                    {
                        Game = Queryable.FirstOrDefault(_context.Games),
                        City = "Poznan",
                        DateCreated = DateTime.Now,
                        SpotName = "DRAFT",
                        Street = "Głogowska",
                        Date = DateTime.Now.AddDays(7),
                    }
                };
                _context.AddRange(meetigns);
            }

            _context.SaveChanges();
        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }
    }
}
