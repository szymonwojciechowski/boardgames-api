using System;
using System.Collections.Generic;
using System.Text;
using BoardGames.DataAccess.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BoardGames.DataAccess.Contexts
{
    public class DataContext : IdentityDbContext<User>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Game> Games { get; set; }
        public DbSet<Meeting> Meetings { get; set; }

    }
}
