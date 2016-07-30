using Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Service.Controllers
{
    public class Game
    {
        private bool nameSet = false;
        private BCard BCard { get; set; }
        private List<User> Users { get; set; }
        private List<BCard> OldBcards { get; set; }
        private IDictionary<User, Card> PlayedCards { get; set; }

        public GameState GameState { get; set; }
        public string GameName
        {
            get { return this.GameName; }
            set
            {
                if (!nameSet)
                {
                    throw new Exception("GameName already set");
                }
                else { this.GameName = value; }
            }
        }        
        public int Round { get; set; }       
        public IDictionary<User,int> UserPoints { get; set; }       

        public Game()
        {
            this.GameState = GameState.Pregame;
            this.Users = new List<User>();
        }
        public string CreateGame()
        {
            nameSet = true;
            return GameName = Guid.NewGuid().ToString();
        }
        public void AddPlayer(User user)
        {
            if (GameState == GameState.Pregame)
            {
                Users.Add(user);
            }
        }
        public void RemovePlayer(User user)
        {
            if (GameState == GameState.Pregame)
            {
                if (Users.Contains(user))
                {
                    Users.Remove(user);
                }
            }
        }
        public void StartGame()
        {
            Round = 1;
            foreach(var user in Users)
            {
                UserPoints.Add(user, 0);
            }
            GameState = GameState.Game;
        }
        public void NextRound(User winner)
        {
            if (GameState==GameState.Game)
            {
                Round++;
                OldBcards.Add(BCard);
                UserPoints[winner] += 1;
                //BCard = DB.NewBcard(); get new black card from bd
            }

        }
        public bool PlayerMove(User user,Card card)
        {
            return true;
        }
    }
}