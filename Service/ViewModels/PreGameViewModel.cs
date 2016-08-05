using Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Service.ViewModels
{
    public class PreGameViewModel:ICAHViewModel
    {
        public string Object = "PreGameViewModel";
        public string GameName { get; set; }
        public List<User> Users { get; set; }
    }
}