using System;
using System.Collections.Generic;
using System.Text;
using BoardGames.DataAccess.Entities.Base;

namespace BoardGames.DataAccess.Entities
{
    public class Game : Entity
    {
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
