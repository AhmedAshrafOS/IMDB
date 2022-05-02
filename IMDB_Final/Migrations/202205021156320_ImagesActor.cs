namespace IMDB_Final.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ImagesActor : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Actors", "Image", c => c.String());
            AlterColumn("dbo.Actors", "FirstName", c => c.String(nullable: false));
            AlterColumn("dbo.Actors", "LastName", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Actors", "LastName", c => c.String());
            AlterColumn("dbo.Actors", "FirstName", c => c.String());
            DropColumn("dbo.Actors", "Image");
        }
    }
}
