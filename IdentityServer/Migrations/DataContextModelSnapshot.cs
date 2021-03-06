﻿// <auto-generated />
using System;
using IdentityServer.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace IdentityServer.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("IdentityServer.Entities.Client", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(50) CHARACTER SET utf8mb4")
                        .HasMaxLength(50);

                    b.Property<string>("AllowedCorsOrigins")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("AllowedScopes")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("FrontChannelLogoutUri")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("PostLogoutRedirectUris")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("RedirectUris")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("IdentityServer.Entities.Scope", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(50) CHARACTER SET utf8mb4")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("Scopes");
                });

            modelBuilder.Entity("IdentityServer.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Email")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("ExternalProvider")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("ExternalProviderId")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Forename")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Password")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Surname")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Username")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
