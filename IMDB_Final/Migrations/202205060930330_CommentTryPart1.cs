namespace IMDB_Final.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CommentTryPart1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.MoviesComments", "MovieId", "dbo.Movies");
            DropForeignKey("dbo.MoviesComments", "UserId", "dbo.Users");
            DropIndex("dbo.MoviesComments", new[] { "UserId" });
            DropIndex("dbo.MoviesComments", new[] { "MovieId" });
            DropPrimaryKey("dbo.MoviesComments");
            AddColumn("dbo.MoviesComments", "CommentId", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.MoviesComments", "Name", c => c.String());
            AddColumn("dbo.UserMovies", "Like", c => c.Boolean(nullable: false));
            AddColumn("dbo.UserMovies", "disLike", c => c.Boolean(nullable: false));
            AddPrimaryKey("dbo.MoviesComments", "CommentId");
            DropColumn("dbo.MoviesComments", "Like");
            DropColumn("dbo.MoviesComments", "disLike");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MoviesComments", "disLike", c => c.Boolean(nullable: false));
            AddColumn("dbo.MoviesComments", "Like", c => c.Boolean(nullable: false));
            DropPrimaryKey("dbo.MoviesComments");
            DropColumn("dbo.UserMovies", "disLike");
            DropColumn("dbo.UserMovies", "Like");
            DropColumn("dbo.MoviesComments", "Name");
            DropColumn("dbo.MoviesComments", "CommentId");
            AddPrimaryKey("dbo.MoviesComments", new[] { "UserId", "MovieId" });
            CreateIndex("dbo.MoviesComments", "MovieId");
            CreateIndex("dbo.MoviesComments", "UserId");
            AddForeignKey("dbo.MoviesComments", "UserId", "dbo.Users", "UserId", cascadeDelete: true);
            AddForeignKey("dbo.MoviesComments", "MovieId", "dbo.Movies", "MovieId", cascadeDelete: true);
        }
    }
}
