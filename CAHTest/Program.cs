using Service.Controllers;
using Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAHTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();
            User host = new User { Name="Pera",UserId=Guid.NewGuid().ToString()};
            User p1 = new User { Name = "mika", UserId = Guid.NewGuid().ToString() };
            User p2 = new User { Name = "zika", UserId = Guid.NewGuid().ToString() };
            game.CreateGame();
            game.AddPlayer(host);
            game.AddPlayer(p1);
            game.AddPlayer(p2);
            game.StartGame();

            Card cd = new Card { Text = "aasdasdd", CardID="3231" };
            Card cc = new Card { Text = "ddddd", CardID = "ssda" };
            Card dd = new Card { Text = "ccccccc", CardID = "ssda" };
            game.PlayerMove(host, cd);
            game.PlayerMove(p1, cc);
            game.PlayerMove(p2, dd);
            Console.ReadKey();
        }
    }
}
