﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApi.Helpers;

namespace WebApi.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20200527235425_InitialCreate6")]
    partial class InitialCreate6
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("WebApi.Entities.Collective", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(15);

                    b.Property<int>("OwnerId");

                    b.Property<int>("Size");

                    b.HasKey("Id");

                    b.ToTable("Collectives");
                });

            modelBuilder.Entity("WebApi.Entities.CollectiveUser", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<int>("CollectiveId");

                    b.HasKey("UserId", "CollectiveId");

                    b.HasIndex("CollectiveId");

                    b.ToTable("CollectiveUsers");
                });

            modelBuilder.Entity("WebApi.Entities.Post", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CollectiveId");

                    b.Property<string>("Content")
                        .IsRequired();

                    b.Property<int>("OwnerId");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(15);

                    b.HasKey("Id");

                    b.HasIndex("CollectiveId");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("WebApi.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(15);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(15);

                    b.Property<byte[]>("PasswordHash");

                    b.Property<byte[]>("PasswordSalt");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(15);

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("WebApi.Entities.CollectiveUser", b =>
                {
                    b.HasOne("WebApi.Entities.Collective", "Collective")
                        .WithMany("CollectiveUsers")
                        .HasForeignKey("CollectiveId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("WebApi.Entities.User", "User")
                        .WithMany("CollectiveUsers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WebApi.Entities.Post", b =>
                {
                    b.HasOne("WebApi.Entities.Collective", "Collective")
                        .WithMany("Posts")
                        .HasForeignKey("CollectiveId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
