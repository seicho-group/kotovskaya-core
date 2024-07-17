﻿// <auto-generated />
using System;
using Kotovskaya.DB.Domain.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Kotovskaya.DB.Migrations
{
    [DbContext(typeof(KotovskayaDbContext))]
    [Migration("20240716173402_Added Pathname")]
    partial class AddedPathname
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0-preview.3.24172.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("CategoryProductEntity", b =>
                {
                    b.Property<Guid>("CategoriesId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ProductsId")
                        .HasColumnType("uuid");

                    b.HasKey("CategoriesId", "ProductsId");

                    b.HasIndex("ProductsId");

                    b.ToTable("CategoryProductEntity");
                });

            modelBuilder.Entity("Kotovskaya.DB.Domain.Entities.DatabaseEntities.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(150)
                        .HasColumnType("uuid");

                    b.Property<bool?>("IsVisible")
                        .HasColumnType("boolean");

                    b.Property<Guid>("MsId")
                        .HasMaxLength(150)
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("character varying(150)");

                    b.Property<Guid?>("ParentCategoryId")
                        .HasMaxLength(150)
                        .HasColumnType("uuid");

                    b.Property<string>("PathName")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("character varying(150)");

                    b.Property<int?>("Type")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ParentCategoryId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Kotovskaya.DB.Domain.Entities.DatabaseEntities.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("AuthorEmail")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("character varying(150)");

                    b.Property<string>("AuthorName")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)");

                    b.Property<string>("AuthorPhone")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)");

                    b.Property<string>("Comment")
                        .HasMaxLength(512)
                        .HasColumnType("character varying(512)");

                    b.Property<string>("DeliveryAddress")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<int>("DeliveryWay")
                        .HasColumnType("integer");

                    b.Property<bool>("HasAuthorDiscount")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsTestOrder")
                        .HasColumnType("boolean");

                    b.Property<string>("MoySkladNumber")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("character varying(150)");

                    b.Property<DateTime>("OrderDateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("PriorityOrderDate")
                        .HasMaxLength(150)
                        .HasColumnType("character varying(150)");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Kotovskaya.DB.Domain.Entities.DatabaseEntities.OrderPosition", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("OrderId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ProductId")
                        .HasMaxLength(150)
                        .HasColumnType("uuid");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderPositions");
                });

            modelBuilder.Entity("Kotovskaya.DB.Domain.Entities.DatabaseEntities.ProductEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Article")
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)");

                    b.Property<string>("Description")
                        .HasMaxLength(2048)
                        .HasColumnType("character varying(2048)");

                    b.Property<string>("ImageLink")
                        .HasMaxLength(512)
                        .HasColumnType("character varying(512)");

                    b.Property<DateTime?>("LastUpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("MsId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("character varying(150)");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Kotovskaya.DB.Domain.Entities.DatabaseEntities.SaleTypes", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int?>("OldPrice")
                        .HasColumnType("integer");

                    b.Property<int>("Price")
                        .HasColumnType("integer");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("ProductId")
                        .IsUnique();

                    b.ToTable("SaleTypes");
                });

            modelBuilder.Entity("CategoryProductEntity", b =>
                {
                    b.HasOne("Kotovskaya.DB.Domain.Entities.DatabaseEntities.Category", null)
                        .WithMany()
                        .HasForeignKey("CategoriesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Kotovskaya.DB.Domain.Entities.DatabaseEntities.ProductEntity", null)
                        .WithMany()
                        .HasForeignKey("ProductsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Kotovskaya.DB.Domain.Entities.DatabaseEntities.Category", b =>
                {
                    b.HasOne("Kotovskaya.DB.Domain.Entities.DatabaseEntities.Category", "ParentCategory")
                        .WithMany()
                        .HasForeignKey("ParentCategoryId");

                    b.Navigation("ParentCategory");
                });

            modelBuilder.Entity("Kotovskaya.DB.Domain.Entities.DatabaseEntities.OrderPosition", b =>
                {
                    b.HasOne("Kotovskaya.DB.Domain.Entities.DatabaseEntities.Order", "Order")
                        .WithMany("OrderPositions")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Kotovskaya.DB.Domain.Entities.DatabaseEntities.ProductEntity", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Kotovskaya.DB.Domain.Entities.DatabaseEntities.SaleTypes", b =>
                {
                    b.HasOne("Kotovskaya.DB.Domain.Entities.DatabaseEntities.ProductEntity", "Product")
                        .WithOne("SaleTypes")
                        .HasForeignKey("Kotovskaya.DB.Domain.Entities.DatabaseEntities.SaleTypes", "ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Kotovskaya.DB.Domain.Entities.DatabaseEntities.Order", b =>
                {
                    b.Navigation("OrderPositions");
                });

            modelBuilder.Entity("Kotovskaya.DB.Domain.Entities.DatabaseEntities.ProductEntity", b =>
                {
                    b.Navigation("SaleTypes");
                });
#pragma warning restore 612, 618
        }
    }
}
