using Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Service.ViewModels
{
    public class JudgeViewModel:ICAHViewModel
    {
        public Dictionary<User,string> PickedCards{ get; set; }
    }
}