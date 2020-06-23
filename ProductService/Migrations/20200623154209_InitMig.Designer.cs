﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using ProductService.Contexts;

namespace ProductService.Migrations
{
    [DbContext(typeof(ProductDatabaseContext))]
    [Migration("20200623154209_InitMig")]
    partial class InitMig
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("ProductService.Models.product", b =>
                {
                    b.Property<Guid>("productId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("productDescription")
                        .HasColumnType("text");

                    b.Property<string>("productName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double>("productPrice")
                        .HasColumnType("double precision");

                    b.Property<DateTime>("productRegisterDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<long>("productStock")
                        .HasColumnType("bigint");

                    b.HasKey("productId");

                    b.ToTable("product");
                });
#pragma warning restore 612, 618
        }
    }
}
