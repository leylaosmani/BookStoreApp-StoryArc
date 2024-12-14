namespace BookStoreApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Updating : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Usertbl", "Username", c => c.String(nullable: false, maxLength: 25));
            AlterColumn("dbo.Usertbl", "Password", c => c.String(nullable: false, maxLength: 15));
            AlterColumn("dbo.Usertbl", "Email", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Usertbl", "Email", c => c.String());
            AlterColumn("dbo.Usertbl", "Password", c => c.String());
            AlterColumn("dbo.Usertbl", "Username", c => c.String());
        }
    }
}
