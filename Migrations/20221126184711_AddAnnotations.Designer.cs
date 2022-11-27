﻿// <auto-generated />
using System;
using MVDB.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MVDB.Migrations
{
    [DbContext(typeof(MoviesContext))]
    [Migration("20221126184711_AddAnnotations")]
    partial class AddAnnotations
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.0");

            modelBuilder.Entity("Director", b =>
                {
                    b.Property<long>("MovieId")
                        .HasColumnType("INTEGER");

                    b.Property<long>("PersonId")
                        .HasColumnType("INTEGER");

                    b.HasKey("MovieId", "PersonId");

                    b.HasIndex("PersonId");

                    b.ToTable("directors", (string)null);
                });

            modelBuilder.Entity("MVDB.Models.Movie", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnType("INTEGER")
                        .HasColumnName("id");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("title");

                    b.Property<decimal?>("Year")
                        .HasColumnType("NUMERIC")
                        .HasColumnName("year");

                    b.HasKey("Id");

                    b.ToTable("movies", (string)null);
                });

            modelBuilder.Entity("MVDB.Models.Person", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnType("INTEGER")
                        .HasColumnName("id");

                    b.Property<decimal?>("Birth")
                        .HasColumnType("NUMERIC")
                        .HasColumnName("birth");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("people", (string)null);
                });

            modelBuilder.Entity("MVDB.Models.Rating", b =>
                {
                    b.Property<long>("MovieId")
                        .HasColumnType("INTEGER")
                        .HasColumnName("movie_id");

                    b.Property<double>("IMDBRating")
                        .HasColumnType("REAL")
                        .HasColumnName("rating");

                    b.Property<long>("Votes")
                        .HasColumnType("INTEGER")
                        .HasColumnName("votes");

                    b.HasKey("MovieId");

                    b.ToTable("ratings", (string)null);
                });

            modelBuilder.Entity("Star", b =>
                {
                    b.Property<long>("MovieId")
                        .HasColumnType("INTEGER");

                    b.Property<long>("PersonId")
                        .HasColumnType("INTEGER");

                    b.HasKey("MovieId", "PersonId");

                    b.HasIndex("PersonId");

                    b.ToTable("stars", (string)null);
                });

            modelBuilder.Entity("Director", b =>
                {
                    b.HasOne("MVDB.Models.Movie", null)
                        .WithMany()
                        .HasForeignKey("MovieId")
                        .IsRequired();

                    b.HasOne("MVDB.Models.Person", null)
                        .WithMany()
                        .HasForeignKey("PersonId")
                        .IsRequired();
                });

            modelBuilder.Entity("MVDB.Models.Rating", b =>
                {
                    b.HasOne("MVDB.Models.Movie", "Movie")
                        .WithOne("Rating")
                        .HasForeignKey("MVDB.Models.Rating", "MovieId")
                        .IsRequired();

                    b.Navigation("Movie");
                });

            modelBuilder.Entity("Star", b =>
                {
                    b.HasOne("MVDB.Models.Movie", null)
                        .WithMany()
                        .HasForeignKey("MovieId")
                        .IsRequired();

                    b.HasOne("MVDB.Models.Person", null)
                        .WithMany()
                        .HasForeignKey("PersonId")
                        .IsRequired();
                });

            modelBuilder.Entity("MVDB.Models.Movie", b =>
                {
                    b.Navigation("Rating");
                });
#pragma warning restore 612, 618
        }
    }
}
