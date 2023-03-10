﻿// <auto-generated />
using System;
using ApiMvno.Infra.Data.Contexts.MvnoDb;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ApiMvno.Infra.Data.Contexts.MvnoDb.Migrations
{
    [DbContext(typeof(MvnoDbContext))]
    partial class MvnoDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("ApiMvno.Domain.Entities.Address", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnOrder(1);

                    b.Property<long>("AddressTypeId")
                        .HasColumnType("bigint")
                        .HasColumnOrder(2);

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnOrder(6);

                    b.Property<Guid>("CountryId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnOrder(3);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2")
                        .HasColumnOrder(11);

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2")
                        .HasColumnOrder(13);

                    b.Property<string>("Details")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnOrder(10);

                    b.Property<string>("Neighborhood")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnOrder(7);

                    b.Property<string>("State")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnOrder(5);

                    b.Property<string>("StreetName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnOrder(8);

                    b.Property<int>("StreetNumber")
                        .HasColumnType("int")
                        .HasColumnOrder(9);

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2")
                        .HasColumnOrder(12);

                    b.Property<string>("ZipCode")
                        .IsRequired()
                        .HasMaxLength(9)
                        .HasColumnType("nvarchar(9)")
                        .HasColumnOrder(4);

                    b.HasKey("Id");

                    b.HasIndex("AddressTypeId");

                    b.HasIndex("CountryId");

                    b.ToTable("Addresses", (string)null);
                });

            modelBuilder.Entity("ApiMvno.Domain.Entities.AddressType", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnOrder(1);

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<bool>("Active")
                        .HasColumnType("bit")
                        .HasColumnOrder(4);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2")
                        .HasColumnOrder(5);

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2")
                        .HasColumnOrder(7);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnOrder(3);

                    b.Property<int>("Type")
                        .HasColumnType("int")
                        .HasColumnOrder(2);

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2")
                        .HasColumnOrder(6);

                    b.HasKey("Id");

                    b.HasIndex("Type")
                        .IsUnique();

                    b.ToTable("AddressTypes", (string)null);
                });

            modelBuilder.Entity("ApiMvno.Domain.Entities.Company", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnOrder(1);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2")
                        .HasColumnOrder(19);

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2")
                        .HasColumnOrder(21);

                    b.Property<string>("Document")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnOrder(4);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnOrder(3);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnOrder(2);

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2")
                        .HasColumnOrder(20);

                    b.HasKey("Id");

                    b.HasIndex("Document")
                        .IsUnique();

                    b.ToTable("Companies", (string)null);
                });

            modelBuilder.Entity("ApiMvno.Domain.Entities.CompanyAddress", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnOrder(1);

                    b.Property<Guid>("AddressId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnOrder(3);

                    b.Property<Guid>("CompanyId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnOrder(2);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2")
                        .HasColumnOrder(4);

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2")
                        .HasColumnOrder(6);

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2")
                        .HasColumnOrder(5);

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.HasIndex("CompanyId", "AddressId")
                        .IsUnique();

                    b.ToTable("CompanyAddresses", (string)null);
                });

            modelBuilder.Entity("ApiMvno.Domain.Entities.CompanyPhone", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnOrder(1);

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<Guid>("CompanyId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnOrder(3);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2")
                        .HasColumnOrder(5);

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2")
                        .HasColumnOrder(7);

                    b.Property<Guid>("PhoneId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnOrder(4);

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2")
                        .HasColumnOrder(6);

                    b.HasKey("Id");

                    b.HasIndex("PhoneId");

                    b.HasIndex("CompanyId", "PhoneId")
                        .IsUnique();

                    b.ToTable("CompanyPhones", (string)null);
                });

            modelBuilder.Entity("ApiMvno.Domain.Entities.Country", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnOrder(1);

                    b.Property<bool>("Active")
                        .HasColumnType("bit")
                        .HasColumnOrder(4);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2")
                        .HasColumnOrder(5);

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2")
                        .HasColumnOrder(7);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnOrder(2);

                    b.Property<string>("PhoneCode")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)")
                        .HasColumnOrder(3);

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2")
                        .HasColumnOrder(6);

                    b.HasKey("Id");

                    b.ToTable("Countries", (string)null);
                });

            modelBuilder.Entity("ApiMvno.Domain.Entities.Phone", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnOrder(1);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2")
                        .HasColumnOrder(4);

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2")
                        .HasColumnOrder(6);

                    b.Property<long>("Number")
                        .HasColumnType("bigint")
                        .HasColumnOrder(3);

                    b.Property<long>("PhoneTypeId")
                        .HasColumnType("bigint")
                        .HasColumnOrder(2);

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2")
                        .HasColumnOrder(5);

                    b.HasKey("Id");

                    b.HasIndex("PhoneTypeId");

                    b.ToTable("Phones", (string)null);
                });

            modelBuilder.Entity("ApiMvno.Domain.Entities.PhoneType", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnOrder(1);

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<bool>("Active")
                        .HasColumnType("bit")
                        .HasColumnOrder(4);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2")
                        .HasColumnOrder(5);

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2")
                        .HasColumnOrder(7);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnOrder(3);

                    b.Property<int>("Type")
                        .HasColumnType("int")
                        .HasColumnOrder(2);

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2")
                        .HasColumnOrder(6);

                    b.HasKey("Id");

                    b.ToTable("PhoneTypes", (string)null);
                });

            modelBuilder.Entity("ApiMvno.Domain.Entities.Address", b =>
                {
                    b.HasOne("ApiMvno.Domain.Entities.AddressType", "AddressType")
                        .WithMany("Addresses")
                        .HasForeignKey("AddressTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ApiMvno.Domain.Entities.Country", "Country")
                        .WithMany("Addresses")
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AddressType");

                    b.Navigation("Country");
                });

            modelBuilder.Entity("ApiMvno.Domain.Entities.CompanyAddress", b =>
                {
                    b.HasOne("ApiMvno.Domain.Entities.Address", "Address")
                        .WithMany("CompanyAddresses")
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ApiMvno.Domain.Entities.Company", "Company")
                        .WithMany("CompanyAddresses")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Address");

                    b.Navigation("Company");
                });

            modelBuilder.Entity("ApiMvno.Domain.Entities.CompanyPhone", b =>
                {
                    b.HasOne("ApiMvno.Domain.Entities.Company", "Company")
                        .WithMany("CompanyPhones")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ApiMvno.Domain.Entities.Phone", "Phone")
                        .WithMany("CompanyPhones")
                        .HasForeignKey("PhoneId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");

                    b.Navigation("Phone");
                });

            modelBuilder.Entity("ApiMvno.Domain.Entities.Phone", b =>
                {
                    b.HasOne("ApiMvno.Domain.Entities.PhoneType", "PhoneType")
                        .WithMany("Phones")
                        .HasForeignKey("PhoneTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PhoneType");
                });

            modelBuilder.Entity("ApiMvno.Domain.Entities.Address", b =>
                {
                    b.Navigation("CompanyAddresses");
                });

            modelBuilder.Entity("ApiMvno.Domain.Entities.AddressType", b =>
                {
                    b.Navigation("Addresses");
                });

            modelBuilder.Entity("ApiMvno.Domain.Entities.Company", b =>
                {
                    b.Navigation("CompanyAddresses");

                    b.Navigation("CompanyPhones");
                });

            modelBuilder.Entity("ApiMvno.Domain.Entities.Country", b =>
                {
                    b.Navigation("Addresses");
                });

            modelBuilder.Entity("ApiMvno.Domain.Entities.Phone", b =>
                {
                    b.Navigation("CompanyPhones");
                });

            modelBuilder.Entity("ApiMvno.Domain.Entities.PhoneType", b =>
                {
                    b.Navigation("Phones");
                });
#pragma warning restore 612, 618
        }
    }
}
