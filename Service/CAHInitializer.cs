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
                
            };
            cards.ForEach(c => context.Cards.Add(c));
            context.SaveChanges();
            var bcards = new List<BCard>
            {
                
            };
            bcards.ForEach(b => context.BCards.Add(b));
            context.SaveChanges();
        }
    }
}