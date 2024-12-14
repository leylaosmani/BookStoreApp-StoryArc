namespace BookStoreApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserTable : DbMigration
    {
        public override void Up()
        {
            //CreateTable(
            //    "dbo.Usertbl",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            Username = c.String(),
            //            Password = c.String(),
            //            Email = c.String(),
            //        })
            //    .PrimaryKey(t => t.Id);
            
            //AddColumn("dbo.Genretbl", "User_Id", c => c.Int());
            //CreateIndex("dbo.Genretbl", "User_Id");
            //AddForeignKey("dbo.Genretbl", "User_Id", "dbo.Usertbl", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Genretbl", "User_Id", "dbo.Usertbl");
            DropIndex("dbo.Genretbl", new[] { "User_Id" });
            DropColumn("dbo.Genretbl", "User_Id");
            DropTable("dbo.Usertbl");
        }
    }
}
