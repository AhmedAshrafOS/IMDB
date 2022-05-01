namespace IMDB_Final.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedatabase : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.MovieActor", "MovieId", "dbo.Movies");
            DropForeignKey("dbo.MovieActor", "ActorId", "dbo.Actors");
            DropForeignKey("dbo.UserActor", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserActor", "ActorId", "dbo.Actors");
            DropForeignKey("dbo.UserDirector", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserDirector", "DirectorId", "dbo.Directors");
            DropForeignKey("dbo.UserMovie", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserMovie", "MovieId", "dbo.Movies");
            DropIndex("dbo.MovieActor", new[] { "MovieId" });
            DropIndex("dbo.MovieActor", new[] { "ActorId" });
            DropIndex("dbo.UserActor", new[] { "UserId" });
            DropIndex("dbo.UserActor", new[] { "ActorId" });
            DropIndex("dbo.UserDirector", new[] { "UserId" });
            DropIndex("dbo.UserDirector", new[] { "DirectorId" });
            DropIndex("dbo.UserMovie", new[] { "UserId" });
            DropIndex("dbo.UserMovie", new[] { "MovieId" });
            CreateTable(
                "dbo.MovesActors",
                c => new
                    {
                        MovieId = c.Int(nullable: false),
                        ActorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.MovieId, t.ActorId })
                .ForeignKey("dbo.Actors", t => t.ActorId, cascadeDelete: true)
                .ForeignKey("dbo.Movies", t => t.MovieId, cascadeDelete: true)
                .Index(t => t.MovieId)
                .Index(t => t.ActorId);
            
            AddColumn("dbo.Users", "Director_DirectorId", c => c.Int());
            CreateIndex("dbo.Users", "Director_DirectorId");
            AddForeignKey("dbo.Users", "Director_DirectorId", "dbo.Directors", "DirectorId");
            DropTable("dbo.MovieActor");
            DropTable("dbo.UserActor");
            DropTable("dbo.UserDirector");
            DropTable("dbo.UserMovie");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.UserMovie",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        MovieId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.MovieId });
            
            CreateTable(
                "dbo.UserDirector",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        DirectorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.DirectorId });
            
            CreateTable(
                "dbo.UserActor",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        ActorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.ActorId });
            
            CreateTable(
                "dbo.MovieActor",
                c => new
                    {
                        MovieId = c.Int(nullable: false),
                        ActorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.MovieId, t.ActorId });
            
            DropForeignKey("dbo.MovesActors", "MovieId", "dbo.Movies");
            DropForeignKey("dbo.Users", "Director_DirectorId", "dbo.Directors");
            DropForeignKey("dbo.MovesActors", "ActorId", "dbo.Actors");
            DropIndex("dbo.Users", new[] { "Director_DirectorId" });
            DropIndex("dbo.MovesActors", new[] { "ActorId" });
            DropIndex("dbo.MovesActors", new[] { "MovieId" });
            DropColumn("dbo.Users", "Director_DirectorId");
            DropTable("dbo.MovesActors");
            CreateIndex("dbo.UserMovie", "MovieId");
            CreateIndex("dbo.UserMovie", "UserId");
            CreateIndex("dbo.UserDirector", "DirectorId");
            CreateIndex("dbo.UserDirector", "UserId");
            CreateIndex("dbo.UserActor", "ActorId");
            CreateIndex("dbo.UserActor", "UserId");
            CreateIndex("dbo.MovieActor", "ActorId");
            CreateIndex("dbo.MovieActor", "MovieId");
            AddForeignKey("dbo.UserMovie", "MovieId", "dbo.Movies", "MovieId", cascadeDelete: true);
            AddForeignKey("dbo.UserMovie", "UserId", "dbo.Users", "UserId", cascadeDelete: true);
            AddForeignKey("dbo.UserDirector", "DirectorId", "dbo.Directors", "DirectorId", cascadeDelete: true);
            AddForeignKey("dbo.UserDirector", "UserId", "dbo.Users", "UserId", cascadeDelete: true);
            AddForeignKey("dbo.UserActor", "ActorId", "dbo.Actors", "ActorId", cascadeDelete: true);
            AddForeignKey("dbo.UserActor", "UserId", "dbo.Users", "UserId", cascadeDelete: true);
            AddForeignKey("dbo.MovieActor", "ActorId", "dbo.Actors", "ActorId", cascadeDelete: true);
            AddForeignKey("dbo.MovieActor", "MovieId", "dbo.Movies", "MovieId", cascadeDelete: true);
        }
    }
}
