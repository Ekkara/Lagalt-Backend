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
    [Migration("20230316125556_test")]
    partial class test
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

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DummyData1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DummyData2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DummyData3")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsAvailable")
                        .HasColumnType("bit");

                    b.Property<int>("OwnerId")
                        .HasColumnType("int");

                    b.Property<string>("ProjectName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Projects");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CategoryName = "Games",
                            Description = "World-Class MMORPG",
                            IsAvailable = true,
                            OwnerId = 0,
                            ProjectName = "Final Fantasy XIV"
                        },
                        new
                        {
                            Id = 2,
                            CategoryName = "Music",
                            Description = "Rock song doubling as a boss theme",
                            IsAvailable = false,
                            OwnerId = 0,
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
