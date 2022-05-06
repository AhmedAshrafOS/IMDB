namespace IMDB_Final.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CommentSection : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserMovies", "comment", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserMovies", "comment");
        }
    }
}
