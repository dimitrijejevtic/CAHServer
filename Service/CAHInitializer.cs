using Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Service
{
    public class CAHInitializer:System.Data.Entity.DropCreateDatabaseIfModelChanges<CAHDB>
    {
        protected override void Seed(CAHDB context)
        {
            var cards = new List<Card>
            {
                new Card {CardID="asd",Text="SDD SD SD D" },
                new Card {CardID="dsa",Text="aads a as " },
                new Card {CardID="sss",Text="da a ss a a " }
            };
            cards.ForEach(c => context.Cards.Add(c));
            context.SaveChanges();
            var bcards = new List<BCard>
            {
                new BCard {BCardID="adsdas",Text="dxxddxdx xd dx " },
                new BCard {BCardID=" ads s",Text="fsd hgdf " },
                new BCard {BCardID="ddddd ",Text="ss a sa sa" }
            };
            bcards.ForEach(b => context.BCards.Add(b));
            context.SaveChanges();
        }
    }
}