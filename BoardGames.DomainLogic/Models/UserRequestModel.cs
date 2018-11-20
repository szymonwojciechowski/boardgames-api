using System;
using System.Collections.Generic;
using System.Text;
using BoardGames.DataAccess.Entities;

namespace BoardGames.DomainLogic.Models
{
    public class UserRequestModel : BaseResponseModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
