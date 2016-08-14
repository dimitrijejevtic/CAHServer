using Service.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Service.ViewModels
{
    public class ListGamesViewModel:ICAHViewModel
    {
        public string Object = "ListGamesViewModel";
        public List<string> Games { get; set; }
    }
}