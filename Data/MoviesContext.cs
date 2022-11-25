using System;
using System.Collections.Generic;
using MVDB.Models;
using Microsoft.EntityFrameworkCore;

namespace MVDB.Data;

public partial class MoviesContext : DbContext
{
    public MoviesContext()
    {
    }

    public MoviesContext(DbContextOptions<MoviesContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Movie> Movies { get; set; }

    public virtual DbSet<Person> People { get; set; }

    public virtual DbSet<Rating> Ratings { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlite("Name=MoviesDB");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Movie>(entity =>
        {
            entity.ToTable("movies");
             
            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Title).HasColumnName("title");
            entity.Property(e => e.Year)
                .HasColumnType("NUMERIC")
                .HasColumnName("year");

            entity.HasMany(d => d.Directors).WithMany(p => p.DirectedMovies)
                .UsingEntity<Dictionary<string, object>>(
                    "Director",
                    r => r.HasOne<Person>().WithMany()
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.ClientSetNull),
                    l => l.HasOne<Movie>().WithMany()
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.ClientSetNull),
                    j =>
                    {
                        j.HasKey("MovieId", "PersonId");
                        j.ToTable("directors");
                    });

            entity.HasMany(d => d.Stars).WithMany(p => p.StarredMovies)
                .UsingEntity<Dictionary<string, object>>(
                    "Star",
                    r => r.HasOne<Person>().WithMany()
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.ClientSetNull),
                    l => l.HasOne<Movie>().WithMany()
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.ClientSetNull),
                    j =>
                    {
                        j.HasKey("MovieId", "PersonId");
                        j.ToTable("stars");
                    });
        });

        modelBuilder.Entity<Person>(entity =>
        {
            entity.ToTable("people");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Birth)
                .HasColumnType("NUMERIC")
                .HasColumnName("birth");
            entity.Property(e => e.Name).HasColumnName("name");
        });

        modelBuilder.Entity<Rating>(entity =>
        {
            entity.HasKey(e => e.MovieId);

            entity.ToTable("ratings");

            entity.Property(e => e.MovieId)
                .ValueGeneratedNever()
                .HasColumnName("movie_id");
            entity.Property(e => e.IMDBRating).HasColumnName("rating");
            entity.Property(e => e.Votes).HasColumnName("votes");

            entity.HasOne(d => d.Movie).WithOne(p => p.Rating)
                .HasForeignKey<Rating>(d => d.MovieId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
