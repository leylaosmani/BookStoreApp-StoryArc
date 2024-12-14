using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;


namespace BookStoreApp.Models
{
    public class BookStoreContext : DbContext

    {
        public BookStoreContext() : base("name=BookStoreContext")
        {
            
                this.Database.Log = Console.WriteLine; 
            
        }
        public DbSet<Booktbl> Books { get; set; }
        public DbSet<Genretbl> Genretbl { get; set; }
        public DbSet<Usertbl> Usertbl { get; set; }

    


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Booktbl>().ToTable("Booktbl");
            modelBuilder.Entity<Genretbl>().ToTable("Genretbl");
            modelBuilder.Entity<Usertbl>().ToTable("Usertbl");


        }

        public System.Data.Entity.DbSet<BookStoreApp.Models.CartItem> CartItems { get; set; }
    }
}