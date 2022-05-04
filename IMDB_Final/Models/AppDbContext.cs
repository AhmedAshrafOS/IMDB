using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using IMDB_Final.Models;

namespace IMDB_Final.Models
{
    public class AppDbContext : DbContext
    {


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MovesActors>().HasKey(sc => new { sc.MovieId, sc.ActorId});
            modelBuilder.Entity<UserActors>().HasKey(sc => new { sc.UserId, sc.ActorId });
            modelBuilder.Entity<UserDirector>().HasKey(sc => new { sc.UserId, sc.DirectorId });
            modelBuilder.Entity<UserMovies>().HasKey(sc => new { sc.UserId, sc.MovieId });
            //Movies and Actors
            //modelBuilder.Entity<Movie>()
            //   .HasMany<Actor>(s => s.Actors)
            //   .WithMany(c => c.Movies)
            //   .Map(cs =>
            //   {
            //       cs.MapLeftKey("MovieId");
            //       cs.MapRightKey("ActorId");
            //       cs.ToTable("MovieActor");
            //   });
            //User and Movies
            //modelBuilder.Entity<User>()
            //   .HasMany<Movie>(s => s.Movies)
            //   .WithMany(c => c.Users)
            //   .Map(cs =>
            //   {
            //       cs.MapLeftKey("UserId");
            //       cs.MapRightKey("MovieId");
            //       cs.ToTable("UserMovie");
            //   });
            ////User and Actors
            //modelBuilder.Entity<User>()
            //   .HasMany<Actor>(s => s.Actors)
            //   .WithMany(c => c.Users)
            //   .Map(cs =>
            //   {
            //       cs.MapLeftKey("UserId");
            //       cs.MapRightKey("ActorId");
            //       cs.ToTable("UserActor");
            //   });
            ////User and Directors
            //modelBuilder.Entity<User>()
            //   .HasMany<Director>(s => s.Directors)
            //   .WithMany(c => c.Users)
            //   .Map(cs =>
            //   {
            //       cs.MapLeftKey("UserId");
            //       cs.MapRightKey("DirectorId");
            //       cs.ToTable("UserDirector");
            //   });

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Director> Directors { get; set; }
        public DbSet<MovesActors> MovesActors { get; set; }
        public DbSet<UserActors> UserActors { get; set; }
        public DbSet<UserDirector> UserDirector { get; set; }
        public DbSet<UserMovies> UserMovies { get; set; }
    }
}