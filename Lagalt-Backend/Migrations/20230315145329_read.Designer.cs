﻿// <auto-generated />
using Lagalt_Backend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Lagalt_Backend.Migrations
{
    [DbContext(typeof(LagaltDbContext))]
    [Migration("20230315145329_read")]
    partial class read
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Lagalt_Backend.Models.Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ProjectCategoryId")
                        .HasColumnType("int");

                    b.Property<string>("ProjectCategoryName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProjectDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("ProjectIsAvailable")
                        .HasColumnType("bit");

                    b.Property<string>("ProjectName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Projects");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ProjectCategoryId = 1,
                            ProjectCategoryName = "Games",
                            ProjectDescription = "World-Class MMORPG",
                            ProjectIsAvailable = true,
                            ProjectName = "Final Fantasy XIV"
                        },
                        new
                        {
                            Id = 2,
                            ProjectCategoryId = 2,
                            ProjectCategoryName = "Music",
                            ProjectDescription = "Rock song doubling as a boss theme",
                            ProjectIsAvailable = false,
                            ProjectName = "Scream"
                        });
                });

            modelBuilder.Entity("Lagalt_Backend.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Hidden")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Hidden = true,
                            UserName = "Maddie"
                        },
                        new
                        {
                            Id = 2,
                            Hidden = false,
                            UserName = "Alice"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
