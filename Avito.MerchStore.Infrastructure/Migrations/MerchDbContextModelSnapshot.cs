﻿// <auto-generated />
using System;
using Avito.MerchStore.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Avito.MerchStore.Infrastructure.Migrations
{
    [DbContext(typeof(MerchDbContext))]
    partial class MerchDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Avito.MerchStore.Domain.Repositories.Models.MerchItem", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<int>("Price")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("MerchItems");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Name = "t-shirt",
                            Price = 80
                        },
                        new
                        {
                            Id = 2L,
                            Name = "cup",
                            Price = 20
                        },
                        new
                        {
                            Id = 3L,
                            Name = "book",
                            Price = 50
                        },
                        new
                        {
                            Id = 4L,
                            Name = "pen",
                            Price = 10
                        },
                        new
                        {
                            Id = 5L,
                            Name = "powerbank",
                            Price = 200
                        },
                        new
                        {
                            Id = 6L,
                            Name = "hoody",
                            Price = 300
                        },
                        new
                        {
                            Id = 7L,
                            Name = "umbrella",
                            Price = 200
                        },
                        new
                        {
                            Id = 8L,
                            Name = "socks",
                            Price = 10
                        },
                        new
                        {
                            Id = 9L,
                            Name = "wallet",
                            Price = 50
                        },
                        new
                        {
                            Id = 10L,
                            Name = "pink-hoody",
                            Price = 500
                        });
                });

            modelBuilder.Entity("Avito.MerchStore.Domain.Repositories.Models.Transaction", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<int>("Amount")
                        .HasColumnType("integer");

                    b.Property<string>("ReceiverName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("SenderName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("Avito.MerchStore.Domain.Repositories.Models.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<int>("Balance")
                        .HasColumnType("integer");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Avito.MerchStore.Domain.Repositories.Models.UserInventory", b =>
                {
                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.Property<long>("ItemId")
                        .HasColumnType("bigint");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer");

                    b.HasKey("UserId", "ItemId");

                    b.HasIndex("ItemId");

                    b.ToTable("UserInventories");
                });

            modelBuilder.Entity("Avito.MerchStore.Domain.Repositories.Models.UserInventory", b =>
                {
                    b.HasOne("Avito.MerchStore.Domain.Repositories.Models.MerchItem", "Item")
                        .WithMany("Inventory")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Avito.MerchStore.Domain.Repositories.Models.User", "User")
                        .WithMany("Inventory")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Item");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Avito.MerchStore.Domain.Repositories.Models.MerchItem", b =>
                {
                    b.Navigation("Inventory");
                });

            modelBuilder.Entity("Avito.MerchStore.Domain.Repositories.Models.User", b =>
                {
                    b.Navigation("Inventory");
                });
#pragma warning restore 612, 618
        }
    }
}
