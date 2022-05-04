namespace IMDB_Final.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstUserEdit : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Users", "Director_DirectorId", "dbo.Directors");
            DropIndex("dbo.Users", new[] { "Director_DirectorId" });
            CreateTable(
                "dbo.UserDirectors",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        DirectorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.DirectorId })
                .ForeignKey("dbo.Directors", t => t.DirectorId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.DirectorId);
            
            CreateTable(
                "dbo.UserActors",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        ActorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.ActorId })
                .ForeignKey("dbo.Actors", t => t.ActorId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.ActorId);
            
            CreateTable(
                "dbo.UserMovies",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        MovieId = c.Int(nullable: false),
                        Like = c.Boolean(nullable: false),
                        disLike = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.MovieId })
                .ForeignKey("dbo.Movies", t => t.MovieId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.MovieId);
            
            AddColumn("dbo.Users", "Image", c => c.String());
            DropColumn("dbo.Users", "Director_DirectorId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "Director_DirectorId", c => c.Int());
            DropForeignKey("dbo.UserMovies", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserMovies", "MovieId", "dbo.Movies");
            DropForeignKey("dbo.UserDirectors", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserActors", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserActors", "ActorId", "dbo.Actors");
            DropForeignKey("dbo.UserDirectors", "DirectorId", "dbo.Directors");
            DropIndex("dbo.UserMovies", new[] { "MovieId" });
            DropIndex("dbo.UserMovies", new[] { "UserId" });
            DropIndex("dbo.UserActors", new[] { "ActorId" });
            DropIndex("dbo.UserActors", new[] { "UserId" });
            DropIndex("dbo.UserDirectors", new[] { "DirectorId" });
            DropIndex("dbo.UserDirectors", new[] { "UserId" });
            DropColumn("dbo.Users", "Image");
            DropTable("dbo.UserMovies");
            DropTable("dbo.UserActors");
            DropTable("dbo.UserDirectors");
            CreateIndex("dbo.Users", "Director_DirectorId");
            AddForeignKey("dbo.Users", "Director_DirectorId", "dbo.Directors", "DirectorId");
        }
    }
}
