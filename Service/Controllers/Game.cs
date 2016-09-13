using Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Service.ViewModels;

namespace Service.Controllers
{
    public class Game
    {
        private CAHDB db = new CAHDB();
        private bool nameSet = false; 
        private BCard BCard { get; set; } // current round Black card
        private List<BCard> OldBcards { get; set; } // used Black cards
        private Dictionary<User, string> PlayedCards { get; set; } // played cards in current round
        private List<User> Users { get; set; } // users in current game
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
            OldBcards = new List<BCard>();
        }
        internal JudgeViewModel GetJudgeViewModel()
        {
            JudgeViewModel jvm = new JudgeViewModel();
            Dictionary<User, string> judgecards = new Dictionary<User, string>();
            foreach(var user in Users)
            {
                if (!IsJudge(user.UserId))
                {
                    judgecards.Add(user, PlayedCards[user]);
                }
            }
            jvm.Users = judgecards.Keys.ToList();
            jvm.Cards = judgecards.Values.ToList();          
            return jvm;
        }

        internal GameInfoViewModel GetGameInfo()
        {
            GameInfoViewModel gvm = new GameInfoViewModel();
            gvm.Round = this.Round;
            gvm.Judge = this.Judge;
            gvm.Users = this.Users;
            gvm.State = this.GameState;
            gvm.GameName = this.GameName;
            gvm.BCard = this.BCard;
            return gvm;
        }
        internal PreGameViewModel GetPreGameInfo()
        {
            PreGameViewModel pgvm = new PreGameViewModel();
            pgvm.Users = Users;
            pgvm.GameName = GameName;
            pgvm.GameState = this.GameState;
            return pgvm;
        }
        internal EndGameViewModel GetEndGameInfo()
        {
            EndGameViewModel egvm = new EndGameViewModel();
            egvm.Users = UserPoints.Keys.ToList();
            egvm.Points = UserPoints.Values.ToList();
            egvm.Rounds = Round;
            egvm.GameState = GameState;
            return egvm;
        }

        public string CreateGame()
        {
            if (!nameSet)
            {
                _gamename = new Random().Next(0, 9999).ToString();
                return GameName;
            }
            else return GameName;
        }
        public void AddPlayer(User user)
        {
            if (GameState == GameState.Pregame)
            {
                Users.Add(user);
            }
            else throw new ErrorViewModel() { ErrorMessage = "Gamestate is not Pregame" };
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
            GameState = GameState.PlayTime;
            SetJudge();
            BCard = NewBCard();
        }
        public void NextRound(User winner)
        {

                if (GameState == GameState.PlayTime)
                {
                    Round++;
                    ChangeJudge();
                    OldBcards.Add(BCard);
                    BCard = NewBCard();
                    UserPoints[winner] += 1;
                    Dictionary<User, string> newPlayed = new Dictionary<User, string>();
                    foreach (var key in PlayedCards.Keys)
                    {
                        newPlayed.Add(key, null);
                    }
                    PlayedCards = newPlayed;
                    //GameState = GameState.RoundEnd;
                }
                else throw new ErrorViewModel() { ErrorMessage = "Gamestate is not PlayTime" };


        }

        internal bool IsJudge(string playerid)
        {
            var user = FindUser(playerid);
            if (Judge == user)
            {
                return true;
            }else return false;
        }

        public bool PlayerMove(User user,Card card)
        {
            if (GameState == GameState.PlayTime)
            {
                if (Judge != user)
                {
                    PlayedCards[user] = card.CardID;
                    return true;
                }else return false;
            }
            else return false;
        }
        private void ChangeJudge()
        {
            if (GameState == GameState.RoundStart||GameState==GameState.PlayTime)
            {
                Judge = Users.ElementAt((Users.IndexOf(Judge) + Round) % Users.Count);
                GameState = GameState.PlayTime;
            }
            else throw new ErrorViewModel() { ErrorMessage = "Gamestate is not RoundStart" };
        }
        private void SetJudge()
        {
            Judge = Users.First(); 
        }

        public void EndGame(string playerid)
        {
            if (GameState == GameState.RoundEnd || GameState == GameState.PlayTime)
            {
                var caller = FindUser(playerid);
                if (caller != null)
                {
                    GameState = GameState.PostGame;
                }
            }
            else throw new ErrorViewModel() { ErrorMessage = "Gamestate is not RoundEnd or PlayTime" };
        }
        private BCard NewBCard()
        {
            while (true)
            {
                var cards = db.BCards.ToList();
                var card = cards.ElementAt(new Random().Next(0, cards.Count));
                if (!OldBcards.Contains(card))
                {
                    return card;
                }
            }
        }
        public User FindUser(string playerid)
        {
            return Users.Single(x => x.UserId == playerid);
        }
    }
}