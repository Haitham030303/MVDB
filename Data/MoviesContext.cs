using System;
using System.Collections.Generic;
using MVDB.Models;
using Microsoft.EntityFrameworkCore;
using System.Xml;

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
        => optionsBuilder.UseSqlite("Data source=C:\\Users\\DSU\\Downloads\\OOD\\Database\\movies.db");

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
                        .HasForeignKey("person_id")
                        .OnDelete(DeleteBehavior.ClientSetNull),
                    l => l.HasOne<Movie>().WithMany()
                        .HasForeignKey("movie_id")
                        .OnDelete(DeleteBehavior.ClientSetNull),
                    j =>
                    {
                        j.HasKey("movie_id", "person_id");
                        j.ToTable("directors");
                    });

            entity.HasMany(d => d.Stars).WithMany(p => p.StarredMovies)
                .UsingEntity<Dictionary<string, object>>(
                    "Star",
                    r => r.HasOne<Person>().WithMany()
                        .HasForeignKey("person_id")
                        .OnDelete(DeleteBehavior.ClientSetNull),
                    l => l.HasOne<Movie>().WithMany()
                        .HasForeignKey("movie_id")
                        .OnDelete(DeleteBehavior.ClientSetNull),
                    j =>
                    {
                        j.HasKey("movie_id", "person_id");
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
        modelBuilder.Entity<Person>()
            .Property(p => p.Birth)
            .HasConversion<double>();
        modelBuilder.Entity<Movie>()
            .Property(m => m.Year)
            .HasConversion<double>();

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
