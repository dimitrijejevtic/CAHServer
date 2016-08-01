using Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Service.Controllers
{
    public class GameController:ApiController
    {
        public IEnumerable<List<User>> Get()
        {
            List<User> users = new List<User>();
            users.Add(new User { UserId = "asd", Name = "pera" });
            users.Add(new User { UserId = "asd", Name = "pera" });
            yield return users;
            
        }
    }
}