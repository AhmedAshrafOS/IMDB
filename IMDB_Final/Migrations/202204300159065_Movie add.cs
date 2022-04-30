namespace IMDB_Final.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Movieadd : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Movies", "DirectorId", "dbo.Directors");
            AddColumn("dbo.Movies", "Image", c => c.String());
            AddColumn("dbo.Movies", "Director_DirectorId", c => c.Int());
            AddColumn("dbo.Movies", "Director_DirectorId1", c => c.Int());
            AlterColumn("dbo.Movies", "MovieName", c => c.String(nullable: false));
            CreateIndex("dbo.Movies", "Director_DirectorId");
            CreateIndex("dbo.Movies", "Director_DirectorId1");
            AddForeignKey("dbo.Movies", "Director_DirectorId", "dbo.Directors", "DirectorId");
            AddForeignKey("dbo.Movies", "Director_DirectorId1", "dbo.Directors", "DirectorId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Movies", "Director_DirectorId1", "dbo.Directors");
            DropForeignKey("dbo.Movies", "Director_DirectorId", "dbo.Directors");
            DropIndex("dbo.Movies", new[] { "Director_DirectorId1" });
            DropIndex("dbo.Movies", new[] { "Director_DirectorId" });
            AlterColumn("dbo.Movies", "MovieName", c => c.String());
            DropColumn("dbo.Movies", "Director_DirectorId1");
            DropColumn("dbo.Movies", "Director_DirectorId");
            DropColumn("dbo.Movies", "Image");
            AddForeignKey("dbo.Movies", "DirectorId", "dbo.Directors", "DirectorId", cascadeDelete: true);
        }
    }
}
