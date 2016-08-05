using Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Service.ViewModels
{
    public class GameInfoViewModel:ICAHViewModel
    {
        public string Object = "GameInfoViewModel";
        public string GameName { get; set; }
        public List<User> Users{ get; set; }
        public BCard BCard { get; set; }
        public User Judge { get; set; }
        public GameState State { get; set; }
        public int Round { get; set; }

    }
}