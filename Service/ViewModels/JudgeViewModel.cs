using Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Service.ViewModels
{
    public class JudgeViewModel:ICAHViewModel
    {
        public string Object = "JudgeViewModel";
        public List<User> Users { get; set; }
        public List<string> Cards { get; set; }
    }
}