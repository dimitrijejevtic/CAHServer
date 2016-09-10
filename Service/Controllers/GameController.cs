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
        public IEnumerable<GameInfoViewModel> GetDefault()
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

            Card cd = new Card { Text = "aasdasdd", CardID = "3231" };
            Card cc = new Card { Text = "ddddd", CardID = "ssda" };
            Card dd = new Card { Text = "ccccccc", CardID = "ssda" };
            game.PlayerMove(host, cd);
            game.PlayerMove(p1, cc);
            game.PlayerMove(p2, dd);
            GameInfoViewModel gmv = game.GetGameInfo();
            yield return gmv;

        }
        #region Statuses

        //GET api/Game/GameInfo
        [HttpGet]
        [Route("GameInfoViewModel")]
        public IEnumerable<ICAHViewModel> GameInfo(string gamename)
        {
            Task<ICAHViewModel> givmtask = Task.Run(() => GC.GetFullGameInfo(gamename, null));
            Task.WaitAll(givmtask);
            yield return givmtask.Result;
        }
        [HttpGet]
        [Route("GetPreGameInfo")]
        public IEnumerable<PreGameViewModel> GetPreGameInfo(string gamename)
        {
            Task<PreGameViewModel> pgvmtask = Task.Run(() => GC.GetPreGameInfo(gamename));
            Task.WaitAll(pgvmtask);
            yield return pgvmtask.Result;
        }
        [HttpGet]
        [Route("GetLobby")]
        public IEnumerable<ICAHViewModel> GetLobby()
        {
            Task<ICAHViewModel> lgvmtask = Task.Run(GC.GetLobby);
            Task.WaitAll(lgvmtask);
            yield return lgvmtask.Result;
        }
        #endregion
        #region Pregame
        [HttpGet]
        [Route("CreateGame")]
        public IEnumerable<ICAHViewModel> CreateGame(string playername)
        {
            Task<ICAHViewModel> pgvmtask = Task.Run(()=>GC.CreateGame(playername));
            Task.WaitAll(pgvmtask);
            var vm = pgvmtask.Result;
            yield return vm;
        }
        [HttpGet]
        [Route("AddPlayer")]
        public IEnumerable<ICAHViewModel> AddPlayer(string gamename,string playername)
        {
            Task<ICAHViewModel> pgvmtask = Task.Run(() => GC.AddPlayerToGame(gamename,playername));
            Task.WaitAll(pgvmtask);
            var vm = pgvmtask.Result;
            yield return vm;
        }
        [HttpGet]
        [Route("RemovePlayer")]
        public IEnumerable<ICAHViewModel> RemovePlayer(string gamename, string playername, string userid)
        {
            Task<ICAHViewModel> pgvmtask = Task.Run(() => GC.RemovePlayerFromGame(gamename, playername, userid));
            Task.WaitAll(pgvmtask);
            yield return pgvmtask.Result;
        }
        [HttpGet]
        [Route("StartGame")]
        public IEnumerable<ICAHViewModel> StartGame(string gamename)
        {
            Task<ICAHViewModel> gvmtask = Task.Run(()=>GC.StartGame(gamename));
            Task.WaitAll(gvmtask);
            yield return gvmtask.Result;
        }
        #endregion
        #region GameTime

        [HttpGet]
        [Route("PlayerMove")]
        public IEnumerable<ICAHViewModel> PlayerMove(string gamename, string playerid, string cardid)
        {
            Task<ICAHViewModel> gvmtask = Task.Run(() => GC.PlayerPickedCard(gamename, playerid, cardid));
            Task.WaitAll(gvmtask);
            yield return gvmtask.Result;
        }

        [HttpGet]
        [Route("PickWinner")]
        public IEnumerable<ICAHViewModel> PickWinner(string gamename,string winnerid)
        {
            Task<ICAHViewModel> gvmtask = Task.Run(() => GC.PickWinner(gamename, winnerid));
            Task.WaitAll(gvmtask);
            yield return gvmtask.Result;
        }
        [HttpGet]
        [Route("GetJVM")]
        public IEnumerable<ICAHViewModel> GetJVM(string gamename,string playerid)
        {
            Task<ICAHViewModel> jgvmtask = Task.Run(() => GC.JudgeList(gamename, playerid));
            Task.WaitAll(jgvmtask);
            yield return jgvmtask.Result;
        }
        #endregion
        #region Postgame
        [HttpGet]
        [Route("EndGame")]
        public IEnumerable<ICAHViewModel> EndGame(string gamename, string playerid)
        {
            Task<ICAHViewModel> gvmtask = Task.Run(() => GC.EndGame(gamename,playerid));
            Task.WaitAll(gvmtask);
            yield return gvmtask.Result;
        }
        #endregion
        #region Testregion
        [HttpGet]
        [Route("TestError")]
        public IEnumerable<ICAHViewModel> TestError(string number)
        {
            Task<ICAHViewModel> gvmtask = Task.Run(() => GC.Test(number));
            Task.WaitAll(gvmtask);
            yield return gvmtask.Result;
        }
        #endregion
    }
}