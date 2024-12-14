namespace BookStoreApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveUserGenres : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserGenres", "UserId", "dbo.Usertbl");
            DropForeignKey("dbo.UserGenres", "GenreId", "dbo.Genretbl");
            DropIndex("dbo.UserGenres", new[] { "UserId" });
            DropIndex("dbo.UserGenres", new[] { "GenreId" });
            DropTable("dbo.UserGenres");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.UserGenres",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        GenreId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.GenreId });
            
            CreateIndex("dbo.UserGenres", "GenreId");
            CreateIndex("dbo.UserGenres", "UserId");
            AddForeignKey("dbo.UserGenres", "GenreId", "dbo.Genretbl", "Id", cascadeDelete: true);
            AddForeignKey("dbo.UserGenres", "UserId", "dbo.Usertbl", "Id", cascadeDelete: true);
        }
    }
}
