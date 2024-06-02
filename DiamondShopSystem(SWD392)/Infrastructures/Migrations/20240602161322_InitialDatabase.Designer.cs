﻿// <auto-generated />
using System;
using Infrastructures;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructures.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240602161322_InitialDatabase")]
    partial class InitialDatabase
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Domain.Entities.BaseEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("DeletedBy")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("ModifiedBy")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("BaseEntity");

                    b.UseTptMappingStrategy();
                });

            modelBuilder.Entity("Domain.Entities.OrderProduct", b =>
                {
                    b.Property<int?>("ProductId")
                        .HasColumnType("int");

                    b.Property<int?>("OrderId")
                        .HasColumnType("int");

                    b.Property<decimal?>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("ProductId", "OrderId");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderProducts", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Account", b =>
                {
                    b.HasBaseType("Domain.Entities.BaseEntity");

                    b.Property<string>("Address")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("ConfirmationToken")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Gender")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<decimal?>("Point")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("RoleId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.HasIndex("RoleId");

                    b.ToTable("Accounts", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.CaratWeight", b =>
                {
                    b.HasBaseType("Domain.Entities.BaseEntity");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<double>("Weight")
                        .HasColumnType("float");

                    b.ToTable("CaratWeights", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Category", b =>
                {
                    b.HasBaseType("Domain.Entities.BaseEntity");

                    b.Property<string>("Name")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.ToTable("Categories", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Clarity", b =>
                {
                    b.HasBaseType("Domain.Entities.BaseEntity");

                    b.Property<string>("Color")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Name")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.ToTable("Clarities", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Cut", b =>
                {
                    b.HasBaseType("Domain.Entities.BaseEntity");

                    b.Property<string>("Name")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<long>("Price")
                        .HasColumnType("bigint");

                    b.ToTable("Cuts", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Diamond", b =>
                {
                    b.HasBaseType("Domain.Entities.BaseEntity");

                    b.Property<int>("CaratWeightId")
                        .HasColumnType("int");

                    b.Property<int>("ClarityId")
                        .HasColumnType("int");

                    b.Property<int>("ColorId")
                        .HasColumnType("int");

                    b.Property<int>("CutId")
                        .HasColumnType("int");

                    b.Property<string>("ImageUrl")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<int>("OriginId")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasIndex("CaratWeightId");

                    b.HasIndex("ClarityId");

                    b.HasIndex("CutId");

                    b.HasIndex("OriginId");

                    b.ToTable("Diamonds", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Order", b =>
                {
                    b.HasBaseType("Domain.Entities.BaseEntity");

                    b.Property<int?>("AccountId")
                        .HasColumnType("int");

                    b.Property<int?>("PaymentId")
                        .HasColumnType("int");

                    b.Property<int?>("StatusId")
                        .HasColumnType("int");

                    b.Property<decimal?>("TotalPrice")
                        .HasColumnType("decimal(18,2)");

                    b.HasIndex("AccountId");

                    b.HasIndex("PaymentId");

                    b.HasIndex("StatusId");

                    b.ToTable("Orders", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Origin", b =>
                {
                    b.HasBaseType("Domain.Entities.BaseEntity");

                    b.Property<string>("Name")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.ToTable("Origins", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Payment", b =>
                {
                    b.HasBaseType("Domain.Entities.BaseEntity");

                    b.Property<string>("Name")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int?>("PaymentType")
                        .IsRequired()
                        .HasColumnType("int");

                    b.ToTable("Payments", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Product", b =>
                {
                    b.HasBaseType("Domain.Entities.BaseEntity");

                    b.Property<int?>("CategoryId")
                        .HasColumnType("int");

                    b.Property<int?>("DiamondId")
                        .HasColumnType("int");

                    b.Property<string>("ImageUrl")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("Name")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<decimal?>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("Quantity")
                        .HasColumnType("int");

                    b.Property<decimal?>("Size")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("Wage")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("WarrantyDocumentsId")
                        .HasColumnType("int");

                    b.HasIndex("CategoryId");

                    b.HasIndex("WarrantyDocumentsId")
                        .IsUnique()
                        .HasFilter("[WarrantyDocumentsId] IS NOT NULL");

                    b.ToTable("Products", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Promotion", b =>
                {
                    b.HasBaseType("Domain.Entities.BaseEntity");

                    b.Property<decimal>("DiscountPercentage")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.ToTable("Promotions", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Role", b =>
                {
                    b.HasBaseType("Domain.Entities.BaseEntity");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Domain.Entities.Status", b =>
                {
                    b.HasBaseType("Domain.Entities.BaseEntity");

                    b.Property<int?>("AccountId")
                        .HasColumnType("int");

                    b.Property<int?>("Name")
                        .IsRequired()
                        .HasColumnType("int");

                    b.HasIndex("AccountId");

                    b.ToTable("Statuses", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.WarrantyDocument", b =>
                {
                    b.HasBaseType("Domain.Entities.BaseEntity");

                    b.Property<int?>("Period")
                        .HasColumnType("int");

                    b.Property<string>("TermsAndConditions")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.ToTable("WarrantyDocuments", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.OrderProduct", b =>
                {
                    b.HasOne("Domain.Entities.Order", "Order")
                        .WithMany("OrderProducts")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Product", "Product")
                        .WithMany("OrderProducts")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Domain.Entities.Account", b =>
                {
                    b.HasOne("Domain.Entities.BaseEntity", null)
                        .WithOne()
                        .HasForeignKey("Domain.Entities.Account", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Role", "Role")
                        .WithMany("Accounts")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Domain.Entities.CaratWeight", b =>
                {
                    b.HasOne("Domain.Entities.BaseEntity", null)
                        .WithOne()
                        .HasForeignKey("Domain.Entities.CaratWeight", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Entities.Category", b =>
                {
                    b.HasOne("Domain.Entities.BaseEntity", null)
                        .WithOne()
                        .HasForeignKey("Domain.Entities.Category", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Entities.Clarity", b =>
                {
                    b.HasOne("Domain.Entities.BaseEntity", null)
                        .WithOne()
                        .HasForeignKey("Domain.Entities.Clarity", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Entities.Cut", b =>
                {
                    b.HasOne("Domain.Entities.BaseEntity", null)
                        .WithOne()
                        .HasForeignKey("Domain.Entities.Cut", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Entities.Diamond", b =>
                {
                    b.HasOne("Domain.Entities.CaratWeight", "CaratWeight")
                        .WithMany("Diamonds")
                        .HasForeignKey("CaratWeightId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Clarity", "Claritys")
                        .WithMany("Diamonds")
                        .HasForeignKey("ClarityId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Cut", "Cuts")
                        .WithMany("Diamonds")
                        .HasForeignKey("CutId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Domain.Entities.BaseEntity", null)
                        .WithOne()
                        .HasForeignKey("Domain.Entities.Diamond", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Origin", "Origin")
                        .WithMany("Diamonds")
                        .HasForeignKey("OriginId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("CaratWeight");

                    b.Navigation("Claritys");

                    b.Navigation("Cuts");

                    b.Navigation("Origin");
                });

            modelBuilder.Entity("Domain.Entities.Order", b =>
                {
                    b.HasOne("Domain.Entities.Account", "Account")
                        .WithMany("Orders")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("Domain.Entities.BaseEntity", null)
                        .WithOne()
                        .HasForeignKey("Domain.Entities.Order", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Payment", "Payment")
                        .WithMany("Orders")
                        .HasForeignKey("PaymentId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("Domain.Entities.Status", "Status")
                        .WithMany("Orders")
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("Account");

                    b.Navigation("Payment");

                    b.Navigation("Status");
                });

            modelBuilder.Entity("Domain.Entities.Origin", b =>
                {
                    b.HasOne("Domain.Entities.BaseEntity", null)
                        .WithOne()
                        .HasForeignKey("Domain.Entities.Origin", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Entities.Payment", b =>
                {
                    b.HasOne("Domain.Entities.BaseEntity", null)
                        .WithOne()
                        .HasForeignKey("Domain.Entities.Payment", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Entities.Product", b =>
                {
                    b.HasOne("Domain.Entities.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId");

                    b.HasOne("Domain.Entities.BaseEntity", null)
                        .WithOne()
                        .HasForeignKey("Domain.Entities.Product", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Diamond", "Diamond")
                        .WithMany("Products")
                        .HasForeignKey("Id")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Domain.Entities.WarrantyDocument", "WarrantyDocument")
                        .WithOne("Products")
                        .HasForeignKey("Domain.Entities.Product", "WarrantyDocumentsId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("Category");

                    b.Navigation("Diamond");

                    b.Navigation("WarrantyDocument");
                });

            modelBuilder.Entity("Domain.Entities.Promotion", b =>
                {
                    b.HasOne("Domain.Entities.BaseEntity", null)
                        .WithOne()
                        .HasForeignKey("Domain.Entities.Promotion", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Entities.Role", b =>
                {
                    b.HasOne("Domain.Entities.BaseEntity", null)
                        .WithOne()
                        .HasForeignKey("Domain.Entities.Role", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Entities.Status", b =>
                {
                    b.HasOne("Domain.Entities.Account", "Account")
                        .WithMany("Statuses")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("Domain.Entities.BaseEntity", null)
                        .WithOne()
                        .HasForeignKey("Domain.Entities.Status", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("Domain.Entities.WarrantyDocument", b =>
                {
                    b.HasOne("Domain.Entities.BaseEntity", null)
                        .WithOne()
                        .HasForeignKey("Domain.Entities.WarrantyDocument", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Entities.Account", b =>
                {
                    b.Navigation("Orders");

                    b.Navigation("Statuses");
                });

            modelBuilder.Entity("Domain.Entities.CaratWeight", b =>
                {
                    b.Navigation("Diamonds");
                });

            modelBuilder.Entity("Domain.Entities.Category", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("Domain.Entities.Clarity", b =>
                {
                    b.Navigation("Diamonds");
                });

            modelBuilder.Entity("Domain.Entities.Cut", b =>
                {
                    b.Navigation("Diamonds");
                });

            modelBuilder.Entity("Domain.Entities.Diamond", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("Domain.Entities.Order", b =>
                {
                    b.Navigation("OrderProducts");
                });

            modelBuilder.Entity("Domain.Entities.Origin", b =>
                {
                    b.Navigation("Diamonds");
                });

            modelBuilder.Entity("Domain.Entities.Payment", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("Domain.Entities.Product", b =>
                {
                    b.Navigation("OrderProducts");
                });

            modelBuilder.Entity("Domain.Entities.Role", b =>
                {
                    b.Navigation("Accounts");
                });

            modelBuilder.Entity("Domain.Entities.Status", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("Domain.Entities.WarrantyDocument", b =>
                {
                    b.Navigation("Products")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
