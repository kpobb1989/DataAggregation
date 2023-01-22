﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Sample.DAL;

#nullable disable

namespace Sample.DAL.Migrations
{
    [DbContext(typeof(SampleDbContext))]
    [Migration("20230117162721_Init")]
    partial class Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Sample.DAL.Entities.License", b =>
                {
                    b.Property<string>("Name")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime?>("LastUpdate")
                        .HasColumnType("datetime2");

                    b.HasKey("Name");

                    b.ToTable("Licenses");
                });

            modelBuilder.Entity("Sample.DAL.Entities.Package", b =>
                {
                    b.Property<string>("Source")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Name")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Version")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime?>("LastUpdate")
                        .HasColumnType("datetime2");

                    b.HasKey("Source", "Name", "Version");

                    b.ToTable("Packages");
                });

            modelBuilder.Entity("Sample.DAL.Entities.PackageToLicense", b =>
                {
                    b.Property<string>("LicenseName")
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("PackageSource")
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("PackageName")
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("PackageVersion")
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("LicenseName", "PackageSource", "PackageName", "PackageVersion");

                    b.HasIndex("PackageSource", "PackageName", "PackageVersion");

                    b.ToTable("PackageToLicense");
                });

            modelBuilder.Entity("Sample.DAL.Entities.PackageToLicense", b =>
                {
                    b.HasOne("Sample.DAL.Entities.License", "License")
                        .WithMany()
                        .HasForeignKey("LicenseName")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Sample.DAL.Entities.Package", "Package")
                        .WithMany()
                        .HasForeignKey("PackageSource", "PackageName", "PackageVersion")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("License");

                    b.Navigation("Package");
                });
#pragma warning restore 612, 618
        }
    }
}