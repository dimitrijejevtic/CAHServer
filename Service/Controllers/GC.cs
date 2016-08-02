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
        public List<Game> Games { get; set; }

        public string NewGame()
        {
            Game newGame = new Game();
            Games.Add(newGame);
            return newGame.GameName;
        }

        public Game FindGame(string gamename)
        {
            return Games.Where(x => x.GameName == gamename).Single();
        }
        /// <summary>
        /// Returns full information about the requested game, matched by gamename
        /// </summary>
        /// <param name="gamename">Gamename whose information is requested</param>
        /// <param name="requester">User that is requesting Gamename information</param>
        /// <returns></returns>
        public async Task<GameInfoViewModel> GetFullGameInfo(string gamename, User requester)
        {
            throw new NotImplementedException();
        }
        #region GameState=Pregame
        public async Task<GameInfoViewModel> CreateGame()
        {
            throw new NotImplementedException();
        }

        public async Task<GameInfoViewModel> AddPlayerToGame()
        {
            throw new NotImplementedException();
        }
        public async Task<GameInfoViewModel> RemovePlayerFromGame()
        {
            throw new NotImplementedException();
        }

        public async Task<GameInfoViewModel> StartGame()
        {
            throw new NotImplementedException(); 
        }
        #endregion
        #region GameState=GameTime
        public async Task<GameInfoViewModel> PlayerPickedCard()
        {
            throw new NotImplementedException();
        }
        public async Task<GameInfoViewModel> PickWinner()
        {
            throw new NotImplementedException();
        }
        #endregion

    }
}