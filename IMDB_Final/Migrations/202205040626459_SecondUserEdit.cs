namespace IMDB_Final.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SecondUserEdit : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "PhoneNumber", c => c.String(nullable: false));
            AddColumn("dbo.Users", "Address", c => c.String(nullable: false));
            AddColumn("dbo.Users", "ConfirmPassword", c => c.String(nullable: false));
            AlterColumn("dbo.Users", "Password", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "Password", c => c.String(nullable: false));
            DropColumn("dbo.Users", "ConfirmPassword");
            DropColumn("dbo.Users", "Address");
            DropColumn("dbo.Users", "PhoneNumber");
        }
    }
}
