using System;
using System.Collections.Generic;
using System.Text;
using BoardGames.DataAccess.Entities.Base;
using Microsoft.AspNetCore.Identity;

namespace BoardGames.DataAccess.Entities
{
    public class User : Entity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte [] PasswordSalt { get; set; }
    }
}
