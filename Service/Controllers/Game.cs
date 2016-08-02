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
        private BCard BCard { get; set; } // current round Black card
        private List<User> Users { get; set; } // users in current game
        private List<BCard> OldBcards { get; set; } // used Black cards
        private Dictionary<User, string> PlayedCards { get; set; } // played cards in current round
        private User Judge { get; set; }

        public GameState GameState { get; set; } // current gamestate (Pregame >| RoundStart > PlayTime > RoundEnd >| PostGame)
        private string _gamename;
        public string GameName
        {
            get { return this._gamename; }        
        }        
        public int Round { get; set; }       
        public Dictionary<User,int> UserPoints { get; set; }    //total points of all rounds   

        public Game()
        {
            this.GameState = GameState.Pregame;
            this.Users = new List<User>();
            UserPoints = new Dictionary<User, int>();
            PlayedCards = new Dictionary<User, string>();
        }
        public string CreateGame()
        {
            _gamename = new Random().Next(0, 9999).ToString();
            return GameName;      
        }
        public void AddPlayer(User user)
        {
            if (GameState == GameState.Pregame)
            {
                Users.Add(user);
            }
        }
        /// <summary>
        ///Removes user from game. User can be removed from game at any GameState 
        /// </summary>
        /// <param name="user">User to be removed</param>
        public void RemovePlayer(User user)
        {            
                if (Users.Contains(user))
                {
                    Users.Remove(user);
                    PlayedCards.Remove(user);
                    UserPoints.Remove(user);
                }            
        }
        public void StartGame()
        {
            Round = 1;
            foreach(var user in Users)
            {
                UserPoints.Add(user, 0);
                PlayedCards.Add(user, null);               
            }
            
            GameState = GameState.RoundStart;
            ChangeJudge();
        }
        public void NextRound(User winner)
        {
            if (GameState==GameState.PlayTime)
            {
                Round++;
                ChangeJudge();
                OldBcards.Add(BCard);
                UserPoints[winner] += 1;
                //BCard = NewBCard(); get new black card from bd
                foreach(var key in PlayedCards.Keys)
                {
                    PlayedCards[key] = null;
                }
            }

        }
        public bool PlayerMove(User user,Card card)
        {
            if (GameState == GameState.PlayTime)
            {
                if (Judge != user)
                {
                    PlayedCards[user] = card.CardId;
                    return true;
                }else return false;
            }
            else return false;
        }
        private void ChangeJudge()
        {
            if (GameState == GameState.RoundStart)
            {
                Judge = Users.ElementAt((Users.IndexOf(Judge) + Round) % Users.Count);
                GameState = GameState.PlayTime;
            }
        }

        public void EndGame()
        {
            if (GameState == GameState.RoundEnd)
            {
                GameState = GameState.PostGame;
            }
        }

        private BCard NewBCard()
        {
            throw new NotImplementedException();
        }
    }
}