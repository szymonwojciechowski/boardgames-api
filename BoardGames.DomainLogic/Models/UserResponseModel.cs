using System;
using System.Collections.Generic;
using System.Text;

namespace BoardGames.DomainLogic.Models
{
    public class UserResponseModel : BaseResponseModel
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
