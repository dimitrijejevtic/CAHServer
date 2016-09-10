using Service.Controllers;
using Service.Models;
using Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Service
{
    public class GC
    {
        public static List<Game> Games { get; set; }
        private CAHDB db = new CAHDB();
        public GC()
        {
            Games = new List<Game>();
        }
        public string NewGame()
        {
            Game newGame = new Game();
            Games.Add(newGame);
            return newGame.GameName;
        }

        public static Game FindGame(string gamename)
        {
            return Games.Where(x => x.GameName == gamename).Single();
        }
        /// <summary>
        /// Returns full information about the requested game, matched by gamename
        /// </summary>
        /// <param name="gamename">Gamename whose information is requested</param>
        /// <param name="requester">User that is requesting Gamename information</param>
        /// <returns></returns>
        public static async Task<ICAHViewModel> GetFullGameInfo(string gamename, User requester)
        {
            return FindGame(gamename).GetGameInfo();
        }
        public static async Task<PreGameViewModel> GetPreGameInfo(string gamename)
        {
            return FindGame(gamename).GetPreGameInfo();
        }
        public static async Task<ICAHViewModel> GetLobby()
        {
            var games = new ListGamesViewModel();
            foreach(var game in Games)
            {
                games.Games.Add(game.GameName);
            }
            return games;
        }
        #region GameState=Pregame
        public static async Task<ICAHViewModel> CreateGame(string playername)
        {
            try
            {
                Game game = new Game();
                game.CreateGame();
                User creator = new User() { Name = playername };
                game.AddPlayer(creator);
                Games.Add(game);
                return game.GetPreGameInfo();
            }catch(ErrorViewModel erv)
            {
                return erv;
            }
        }

        public static async Task<ICAHViewModel> AddPlayerToGame(string gamename,string playername)
        {
            try
            {
                User user = new User() { Name = playername };
                Game game = FindGame(gamename);
                game.AddPlayer(user);
                return game.GetPreGameInfo();
            }catch(ErrorViewModel erv)
            {
                return erv;
            }

        }
        public static async Task<ICAHViewModel> RemovePlayerFromGame(string gamename, string playername,string userid)
        {
            try
            {
                User user = new User()
                { UserId = userid, Name = playername };
                var game = FindGame(gamename);
                game.RemovePlayer(user);
                return game.GetPreGameInfo();
            }catch(ErrorViewModel erv)
            {
                return erv;
            }
        }

        public static async Task<ICAHViewModel> StartGame(string gamename)
        {
            var game = FindGame(gamename);
            game.StartGame();
            return game.GetGameInfo();
        }
        #endregion
        #region GameState=GameTime
        public static async Task<ICAHViewModel> PlayerPickedCard(string gamename, string playerid,string cardid)
        {
            var game = FindGame(gamename);
            var user = game.FindUser(playerid);
            //Implementation for cardbank needed
            //var card=bank.FindCard(cardid);
            var card = new Card() { CardID = cardid };
            game.PlayerMove(user, card);
            return game.GetGameInfo();
        }
        public static async Task<ICAHViewModel> PickWinner(string gamename,string winnerid)
        {
            var game = FindGame(gamename);
            if (!game.IsJudge(winnerid))
            {
                var winner = game.FindUser(winnerid);
                game.NextRound(winner);
                return game.GetGameInfo();
            }
            else return new ErrorViewModel() { ErrorMessage = "Judge cant be winner" };
        }

        public static async Task<ICAHViewModel> EndGame(string gamename, string playerid)
        {
            var game = FindGame(gamename);
            game.EndGame(playerid);
            return game.GetEndGameInfo();
            
        }
        public static async Task<ICAHViewModel> JudgeList(string gamename,string playerid)
        {
            var game = FindGame(gamename);
            if (game.IsJudge(playerid))
            {
                JudgeViewModel jvm = game.GetJudgeViewModel();
                return jvm;
            }
            else return new ErrorViewModel() { ErrorMessage = "You are not judge" };
        }
        #endregion
        public static async Task<ICAHViewModel> Test(string param)
        {
            if (param == "1")
            {
                return Games.First().GetPreGameInfo();
            }
            else return new ErrorViewModel() { ErrorMessage = "asdasda" };
        }
    }
}