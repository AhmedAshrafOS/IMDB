namespace IMDB_Final.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Actors",
                c => new
                    {
                        ActorId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Age = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ActorId);
            
            CreateTable(
                "dbo.Movies",
                c => new
                    {
                        MovieId = c.Int(nullable: false, identity: true),
                        MovieName = c.String(),
                        Like = c.Int(nullable: false),
                        Dislike = c.Int(nullable: false),
                        DirectorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MovieId)
                .ForeignKey("dbo.Directors", t => t.DirectorId, cascadeDelete: true)
                .Index(t => t.DirectorId);
            
            CreateTable(
                "dbo.Directors",
                c => new
                    {
                        DirectorId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Age = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.DirectorId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        UserName = c.String(nullable: false),
                        Password = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.Admins",
                c => new
                    {
                        AdminId = c.Int(nullable: false, identity: true),
                        AdminName = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.AdminId);
            
            CreateTable(
                "dbo.MovieActor",
                c => new
                    {
                        MovieId = c.Int(nullable: false),
                        ActorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.MovieId, t.ActorId })
                .ForeignKey("dbo.Movies", t => t.MovieId, cascadeDelete: true)
                .ForeignKey("dbo.Actors", t => t.ActorId, cascadeDelete: true)
                .Index(t => t.MovieId)
                .Index(t => t.ActorId);
            
            CreateTable(
                "dbo.UserActor",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        ActorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.ActorId })
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Actors", t => t.ActorId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.ActorId);
            
            CreateTable(
                "dbo.UserDirector",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        DirectorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.DirectorId })
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Directors", t => t.DirectorId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.DirectorId);
            
            CreateTable(
                "dbo.UserMovie",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        MovieId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.MovieId })
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Movies", t => t.MovieId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.MovieId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserMovie", "MovieId", "dbo.Movies");
            DropForeignKey("dbo.UserMovie", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserDirector", "DirectorId", "dbo.Directors");
            DropForeignKey("dbo.UserDirector", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserActor", "ActorId", "dbo.Actors");
            DropForeignKey("dbo.UserActor", "UserId", "dbo.Users");
            DropForeignKey("dbo.Movies", "DirectorId", "dbo.Directors");
            DropForeignKey("dbo.MovieActor", "ActorId", "dbo.Actors");
            DropForeignKey("dbo.MovieActor", "MovieId", "dbo.Movies");
            DropIndex("dbo.UserMovie", new[] { "MovieId" });
            DropIndex("dbo.UserMovie", new[] { "UserId" });
            DropIndex("dbo.UserDirector", new[] { "DirectorId" });
            DropIndex("dbo.UserDirector", new[] { "UserId" });
            DropIndex("dbo.UserActor", new[] { "ActorId" });
            DropIndex("dbo.UserActor", new[] { "UserId" });
            DropIndex("dbo.MovieActor", new[] { "ActorId" });
            DropIndex("dbo.MovieActor", new[] { "MovieId" });
            DropIndex("dbo.Movies", new[] { "DirectorId" });
            DropTable("dbo.UserMovie");
            DropTable("dbo.UserDirector");
            DropTable("dbo.UserActor");
            DropTable("dbo.MovieActor");
            DropTable("dbo.Admins");
            DropTable("dbo.Users");
            DropTable("dbo.Directors");
            DropTable("dbo.Movies");
            DropTable("dbo.Actors");
        }
    }
}
