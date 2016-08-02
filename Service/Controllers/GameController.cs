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
        //GET api/Game/Get
        [ActionName("Get")]
        public IEnumerable<List<User>> Get()
        {
            List<User> users = new List<User>();
            users.Add(new User { UserId = "asd", Name = "pera" });
            users.Add(new User { UserId = "asd", Name = "pera" });
            yield return users;
            
        }
        //GET api/Game/GameInfo
        [ActionName("GameInfo")]
        public IEnumerable<Game> GameInfo()
        {
            Game game = new Game();
            User host = new User { Name = "Pera", UserId = Guid.NewGuid().ToString() };
            User p1 = new User { Name = "mika", UserId = Guid.NewGuid().ToString() };
            User p2 = new User { Name = "zika", UserId = Guid.NewGuid().ToString() };
            game.CreateGame();
            game.AddPlayer(host);
            game.AddPlayer(p1);
            game.AddPlayer(p2);
            game.StartGame();

            Card cd = new Card { Text = "aasdasdd", CardId = "3231" };
            Card cc = new Card { Text = "ddddd", CardId = "ssda" };
            Card dd = new Card { Text = "ccccccc", CardId = "ssda" };
            game.PlayerMove(host, cd);
            game.PlayerMove(p1, cc);
            game.PlayerMove(p2, dd);
            yield return game;
        }
    }
}