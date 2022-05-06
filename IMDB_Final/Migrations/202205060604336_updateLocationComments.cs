namespace IMDB_Final.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateLocationComments : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MoviesComments",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        MovieId = c.Int(nullable: false),
                        Like = c.Boolean(nullable: false),
                        disLike = c.Boolean(nullable: false),
                        comment = c.String(),
                    })
                .PrimaryKey(t => new { t.UserId, t.MovieId })
                .ForeignKey("dbo.Movies", t => t.MovieId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.MovieId);
            
            DropColumn("dbo.UserMovies", "Like");
            DropColumn("dbo.UserMovies", "disLike");
            DropColumn("dbo.UserMovies", "comment");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserMovies", "comment", c => c.String());
            AddColumn("dbo.UserMovies", "disLike", c => c.Boolean(nullable: false));
            AddColumn("dbo.UserMovies", "Like", c => c.Boolean(nullable: false));
            DropForeignKey("dbo.MoviesComments", "UserId", "dbo.Users");
            DropForeignKey("dbo.MoviesComments", "MovieId", "dbo.Movies");
            DropIndex("dbo.MoviesComments", new[] { "MovieId" });
            DropIndex("dbo.MoviesComments", new[] { "UserId" });
            DropTable("dbo.MoviesComments");
        }
    }
}
