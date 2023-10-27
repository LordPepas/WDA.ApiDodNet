﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using WDA.ApiDotNet.Infra.Data.Context;

#nullable disable

namespace WDA.ApiDodNet.Infra.Data.Migrations
{
    [DbContext(typeof(ContextDb))]
    partial class ContextDbModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("WDA.ApiDotNet.Application.Models.Books", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("PublisherId")
                        .HasColumnType("integer");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer");

                    b.Property<int>("Release")
                        .HasColumnType("integer");

                    b.Property<int>("Rented")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("PublisherId");

                    b.ToTable("Books", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Author = "J.R.R. Tolkien",
                            Name = "O Senhor dos Anéis",
                            PublisherId = 1,
                            Quantity = 10,
                            Release = 1954,
                            Rented = 0
                        },
                        new
                        {
                            Id = 2,
                            Author = "J.K. Rowling",
                            Name = "Harry Potter e a Pedra Filosofal",
                            PublisherId = 2,
                            Quantity = 1,
                            Release = 1997,
                            Rented = 0
                        },
                        new
                        {
                            Id = 3,
                            Author = "Miguel de Cervantes",
                            Name = "Dom Quixote",
                            PublisherId = 3,
                            Quantity = 1,
                            Release = 1605,
                            Rented = 0
                        },
                        new
                        {
                            Id = 4,
                            Author = "Gabriel García Márquez",
                            Name = "Cem Anos de Solidão",
                            PublisherId = 4,
                            Quantity = 12,
                            Release = 1954,
                            Rented = 0
                        },
                        new
                        {
                            Id = 5,
                            Author = "George Orwell",
                            Name = "1984",
                            PublisherId = 5,
                            Quantity = 7,
                            Release = 1954,
                            Rented = 0
                        },
                        new
                        {
                            Id = 6,
                            Author = "George Orwell",
                            Name = "A Revolução dos Bichos",
                            PublisherId = 1,
                            Quantity = 5,
                            Release = 1954,
                            Rented = 0
                        },
                        new
                        {
                            Id = 7,
                            Author = "Fiódor Dostoiévski",
                            Name = "Crime e Castigo",
                            PublisherId = 2,
                            Quantity = 3,
                            Release = 1954,
                            Rented = 0
                        },
                        new
                        {
                            Id = 8,
                            Author = "Antoine de Saint-Exupéry",
                            Name = "O Pequeno Príncipe",
                            PublisherId = 3,
                            Quantity = 8,
                            Release = 1954,
                            Rented = 0
                        },
                        new
                        {
                            Id = 9,
                            Author = "Machado de Assis",
                            Name = "Memórias Póstumas de Brás Cubas",
                            PublisherId = 4,
                            Quantity = 6,
                            Release = 1954,
                            Rented = 0
                        },
                        new
                        {
                            Id = 10,
                            Author = "Franz Kafka",
                            Name = "A Metamorfose",
                            PublisherId = 5,
                            Quantity = 4,
                            Release = 1954,
                            Rented = 0
                        });
                });

            modelBuilder.Entity("WDA.ApiDotNet.Application.Models.Publishers", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Publishers", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            City = "São Paulo",
                            Name = "Companhia das Letras"
                        },
                        new
                        {
                            Id = 2,
                            City = "Rio de Janeiro",
                            Name = "Aleph"
                        },
                        new
                        {
                            Id = 3,
                            City = "Rio De Janeiro",
                            Name = "Editora Intrínseca"
                        },
                        new
                        {
                            Id = 4,
                            City = "Rio de Janeiro",
                            Name = "Editora Rocco"
                        },
                        new
                        {
                            Id = 5,
                            City = "Porto Alegre",
                            Name = "Darkside"
                        },
                        new
                        {
                            Id = 6,
                            City = "Nova Iorque",
                            Name = "Harper Collins"
                        },
                        new
                        {
                            Id = 7,
                            City = "Rio de Janeiro",
                            Name = "Editora Arqueiro"
                        },
                        new
                        {
                            Id = 8,
                            City = "Lisboa",
                            Name = "Leya"
                        },
                        new
                        {
                            Id = 9,
                            City = "São Paulo",
                            Name = "Saraiva"
                        },
                        new
                        {
                            Id = 10,
                            City = "Porto Alegre",
                            Name = "Sextante"
                        });
                });

            modelBuilder.Entity("WDA.ApiDotNet.Application.Models.Rentals", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("BookId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("PrevisionDate")
                        .HasColumnType("date");

                    b.Property<DateTime>("RentalDate")
                        .HasColumnType("date");

                    b.Property<DateTime?>("ReturnDate")
                        .HasColumnType("date");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("BookId");

                    b.HasIndex("UserId");

                    b.ToTable("Rentals", (string)null);
                });

            modelBuilder.Entity("WDA.ApiDotNet.Application.Models.Users", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Address = "Rua A",
                            City = "Fortaleza",
                            Email = "lauro@yahoo.com",
                            Name = "Lauro"
                        },
                        new
                        {
                            Id = 2,
                            Address = "Rua B",
                            City = "Crato",
                            Email = "roberto@gmail.com",
                            Name = "Roberto"
                        },
                        new
                        {
                            Id = 3,
                            Address = "Rua C",
                            City = "Caucaia",
                            Email = "ronaldo@hotmail.com",
                            Name = "Ronaldo"
                        },
                        new
                        {
                            Id = 4,
                            Address = "Rua D",
                            City = "Recife",
                            Email = "rodrigo@gmail.com",
                            Name = "Rodrigo"
                        },
                        new
                        {
                            Id = 5,
                            Address = "Rua E",
                            City = "Rio de Janeiro",
                            Email = "alexandre@yahoo.com",
                            Name = "Alexandre"
                        },
                        new
                        {
                            Id = 6,
                            Address = "Rua F",
                            City = "Fortaleza",
                            Email = "isabela@gmail.com",
                            Name = "Isabela"
                        },
                        new
                        {
                            Id = 7,
                            Address = "Rua G",
                            City = "São Paulo",
                            Email = "pedro@hotmail.com",
                            Name = "Pedro"
                        },
                        new
                        {
                            Id = 8,
                            Address = "Rua H",
                            City = "Rio de Janeiro",
                            Email = "mariana@yahoo.com",
                            Name = "Mariana"
                        },
                        new
                        {
                            Id = 9,
                            Address = "Rua I",
                            City = "Belo Horizonte",
                            Email = "lucas@gmail.com",
                            Name = "Lucas"
                        },
                        new
                        {
                            Id = 10,
                            Address = "Rua J",
                            City = "Recife",
                            Email = "amanda@hotmail.com",
                            Name = "Amanda"
                        });
                });

            modelBuilder.Entity("WDA.ApiDotNet.Application.Models.Books", b =>
                {
                    b.HasOne("WDA.ApiDotNet.Application.Models.Publishers", "Publisher")
                        .WithMany()
                        .HasForeignKey("PublisherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Publisher");
                });

            modelBuilder.Entity("WDA.ApiDotNet.Application.Models.Rentals", b =>
                {
                    b.HasOne("WDA.ApiDotNet.Application.Models.Books", "Book")
                        .WithMany()
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WDA.ApiDotNet.Application.Models.Users", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Book");

                    b.Navigation("User");
                });
#pragma warning restore 612, 618
        }
    }
}
