using Service.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Service
{
    public class GC
    {
        public List<Game> Games { get; set; }

        public string NewGame()
        {
            Game newGame = new Game();
            Games.Add(newGame);
            return newGame.GameName;
        }

        public Game FindGame(string gamename)
        {
            return Games.Where(x => x.GameName == gamename).Single();
        }
    }
}