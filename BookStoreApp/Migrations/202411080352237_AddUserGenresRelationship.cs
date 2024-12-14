namespace BookStoreApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class AddUserGenresRelationship : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserGenres",
                c => new
                {
                    UserId = c.Int(nullable: false),
                    GenreId = c.Int(nullable: false),
                })
                .PrimaryKey(t => new { t.UserId, t.GenreId })
                .ForeignKey("dbo.Usertbl", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Genretbl", t => t.GenreId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.GenreId);
        }

        public override void Down()
        {
            DropForeignKey("dbo.UserGenres", "GenreId", "dbo.Genretbl");
            DropForeignKey("dbo.UserGenres", "UserId", "dbo.Usertbl");
            DropIndex("dbo.UserGenres", new[] { "GenreId" });
            DropIndex("dbo.UserGenres", new[] { "UserId" });
            DropTable("dbo.UserGenres");
        }
    }
}
