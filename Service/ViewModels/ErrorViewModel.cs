using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Service.ViewModels
{
    public class ErrorViewModel:Exception,ICAHViewModel
    {
        public string Ojbect = "ErrorViewModel";
        public string ErrorMessage { get; set; }
    }
}