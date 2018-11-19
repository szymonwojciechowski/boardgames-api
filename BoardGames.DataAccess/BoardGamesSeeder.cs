using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoardGames.DataAccess.Contexts;
using BoardGames.DataAccess.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Internal;

namespace BoardGames.DataAccess
{
    public class BoardGamesSeeder
    {
        private readonly DataContext _context;
        private readonly UserManager<User> _userManager;

        public BoardGamesSeeder(DataContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task Seed()
        {
            _context.Database.EnsureCreated();
            var user1 = await _userManager.FindByEmailAsync("szymon.wojciechowski27@gmail.com");
            if (user1 == null)
            {
                user1 = new User
                {
                    FirstName = "Szymon",
                    LastName = "Wojciechowski",
                    Email = "szymon.wojciechowski27@gmail.com",
                    UserName = "szymek"
                };
                var result = await _userManager.CreateAsync(user1, "Admin123!");
                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Failed to create default user1");
                }
            }
            var user2 = await _userManager.FindByEmailAsync("justyna.wojciechowska04@gmail.com");
            if (user2 == null)
            {
                user2 = new User
                {
                    FirstName = "Justyna    ",
                    LastName = "Wojciechowska",
                    Email = "justyna.wojciechowska04@gmail.com",
                    UserName = "justynka"
                };
                var result = await _userManager.CreateAsync(user2, "Admin123!");
                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Failed to create default user2");
                }
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
    }
}
