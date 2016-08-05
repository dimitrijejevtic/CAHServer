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
        public static async Task<GameInfoViewModel> GetFullGameInfo(string gamename, User requester)
        {
            return FindGame(gamename).GetGameInfo();
        }
        public static async Task<PreGameViewModel> GetPreGameInfo(string gamename)
        {
            return FindGame(gamename).GetPreGameInfo();
        }
        #region GameState=Pregame
        public static async Task<PreGameViewModel> CreateGame(string playername)
        {
            Game game = new Game();
            game.CreateGame();
            User creator = new User() { Name = playername };
            game.AddPlayer(creator);
            Games.Add(game);
            return game.GetPreGameInfo();
        }

        public static async Task<PreGameViewModel> AddPlayerToGame(string gamename,string playername)
        {
            User user = new User() { Name = playername };
            Game game = FindGame(gamename);
            game.AddPlayer(user);
            return game.GetPreGameInfo();

        }
        public static async Task<PreGameViewModel> RemovePlayerFromGame(string gamename, string playername,string userid)
        {
            User user = new User()
            {   UserId = userid, Name = playername};
            var game = FindGame(gamename);
            game.RemovePlayer(user);
            return game.GetPreGameInfo();
        }

        public static async Task<GameInfoViewModel> StartGame(string gamename)
        {
            var game = FindGame(gamename);
            game.StartGame();
            return game.GetGameInfo();
        }
        #endregion
        #region GameState=GameTime
        public static async Task<GameInfoViewModel> PlayerPickedCard(string gamename, string playerid,string cardid)
        {
            var game = FindGame(gamename);
            var user = game.FindUser(playerid);
            //Implementation for cardbank needed
            //var card=bank.FindCard(cardid);
            var card = new Card() { CardId = cardid };
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