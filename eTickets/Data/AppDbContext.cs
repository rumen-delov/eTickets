using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eTickets.Models;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ActorMovie>().HasKey(am => new {
                am.ActorId,
                am.MovieId
            });

            // Define the one-to-many relationships in the mapping table 
            modelBuilder.Entity<ActorMovie>().HasOne(m => m.Movie).WithMany(am => am.ActorsMovies).HasForeignKey(m => m.MovieId);
            modelBuilder.Entity<ActorMovie>().HasOne(m => m.Actor).WithMany(am => am.ActorsMovies).HasForeignKey(m => m.ActorId);

            base.OnModelCreating(modelBuilder);
        }

        // Define the table names for each model
        public DbSet<Actor> Actors { get; set; }

        public DbSet<Movie> Movies { get; set; }

        public DbSet<ActorMovie> ActorsMovies { get; set; }

        public DbSet<Cinema> Cinemas { get; set; }

        public DbSet<Producer> Producers { get; set; }
    }
}
