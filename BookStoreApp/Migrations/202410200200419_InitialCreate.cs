namespace BookStoreApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            //CreateTable(
            //    "dbo.Booktbl",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            Title = c.String(),
            //            Author = c.String(),
            //            Price = c.Decimal(nullable: false, precision: 18, scale: 2),
            //            ISBN = c.String(),
            //            GenreId = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("dbo.Genretbl", t => t.GenreId, cascadeDelete: true)
            //    .Index(t => t.GenreId);
            
            //CreateTable(
            //    "dbo.Genretbl",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            Name = c.String(),
            //        })
            //    .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Booktbl", "GenreId", "dbo.Genretbl");
            DropIndex("dbo.Booktbl", new[] { "GenreId" });
            DropTable("dbo.Genretbl");
            DropTable("dbo.Booktbl");
        }
    }
}
