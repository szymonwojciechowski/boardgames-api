using System;
using System.Collections.Generic;
using System.Text;
using BoardGames.DataAccess.Entities.Base;

namespace BoardGames.DataAccess.Entities
{
    public class Meeting : Entity
    {
        public Game Game { get; set; }
        public DateTime Date { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string SpotName { get; set; }

    }
}
