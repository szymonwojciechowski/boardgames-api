using System;
using System.Collections.Generic;
using System.Text;

namespace BoardGames.DomainLogic.Models
{
    public class GameResponseModel : BaseResponseModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
