using System;
using BACKEND.Entities;
using Microsoft.EntityFrameworkCore;

namespace BACKEND
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        { 
            //Llave primaria compuesta
            modelBuilder.Entity<MovieActor>()
                .HasKey(x => new { x.MovieId, x.ActorId});

            modelBuilder.Entity<MovieCinema>()
                .HasKey(x => new { x.MovieId, x.CinemaId });

            modelBuilder.Entity<MovieGender>()
                .HasKey(x => new { x.MovieId, x.GenderId });

            base.OnModelCreating(modelBuilder); 
        }

        public DbSet<Gender> Gender { get; set; }
        public DbSet<Actor> Actor { get; set; }
        public DbSet<Cinema> Cinema { get; set; }

        public DbSet<MovieActor> MovieActor { get; set; }
        public DbSet<MovieCinema> MovieCinema { get; set; }
        public DbSet<MovieGender> MovieGender { get; set; }

    }
}
