using Service.Models;
using Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Service.Controllers
{
    [RoutePrefix("api/Game")]
    public class GameController : ApiController
    {
        //GET api/Game/Get
        [Route("")]
        public IEnumerable<GameInfoViewModel> Get()
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
            GameInfoViewModel gmv = game.GetGameInfo();
            yield return gmv;

        }
        //GET api/Game/GameInfo
        [Route("GameInfo")]
        public IEnumerable<GameInfoViewModel> GameInfo()
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
            GameInfoViewModel gmv = game.GetGameInfo();
            yield return gmv;
        }
        [Route("CreateGame")]
        public IEnumerable<GameInfoViewModel> CreateGame()
        {
            Task<GameInfoViewModel> gvmtask = Task.Run(GC.CreateGame);
            Task.WaitAll(gvmtask);
            var gvm = gvmtask.Result;
            yield return gvm;
        }
    }
}