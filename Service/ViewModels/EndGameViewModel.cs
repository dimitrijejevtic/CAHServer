using Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Service.ViewModels
{
    public class EndGameViewModel:ICAHViewModel
    {
        public string Object = "EndGameViewModel";
        public Dictionary<User,int> UserPoints { get; set; }
        public int Rounds { get; set; }
        public GameState GameState  { get; set; }
    }
}