namespace Service
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using Models;

    public partial class CAHDB : DbContext
    {
        public CAHDB()
            : base("name=CAHModel")
        {
        }

        public DbSet<BCard> BCards { get; set; }
        public DbSet<Card> Cards { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
