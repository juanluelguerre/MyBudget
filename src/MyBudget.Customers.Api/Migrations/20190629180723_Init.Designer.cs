﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using MyBudget.Customers.Api.Application.Data;

namespace MyBudget.Customers.Api.Migrations
{
	[DbContext(typeof(DataContext))]
    [Migration("20190629180723_Init")]
    partial class Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.1-servicing-10028");

            modelBuilder.Entity("MyBudget.Customers.Api.Application.Domain.Aggregates.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<byte>("Active")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("FirstName")
                        .HasMaxLength(50);

                    b.Property<string>("LastName")
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("Customers");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Active = (byte)1,
                            FirstName = "Juan Luis",
                            LastName = "Guerrero Minero"
                        },
                        new
                        {
                            Id = 2,
                            Active = (byte)1,
                            FirstName = "Francisco",
                            LastName = "Ruiz Vázquez"
                        },
                        new
                        {
                            Id = 3,
                            Active = (byte)1,
                            FirstName = "Eva",
                            LastName = "Perez Moreno"
                        },
                        new
                        {
                            Id = 4,
                            Active = (byte)1,
                            FirstName = "Maria",
                            LastName = "Serrano Sanchez"
                        });
                });

            modelBuilder.Entity("MyBudget.Customers.Api.Application.Domain.Aggregates.CustomerAccount", b =>
                {
                    b.Property<int>("CustomerId");

                    b.Property<string>("BankAccount");

                    b.Property<byte>("MarkAsDefault")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("CustomerId", "BankAccount");

                    b.HasAlternateKey("BankAccount");

                    b.ToTable("CustomersAccounts");

                    b.HasData(
                        new
                        {
                            CustomerId = 1,
                            BankAccount = "ES12-1234-1234-1234567890",
                            MarkAsDefault = (byte)1
                        },
                        new
                        {
                            CustomerId = 1,
                            BankAccount = "ES13-4321-4321-0987654321",
                            MarkAsDefault = (byte)0
                        });
                });

            modelBuilder.Entity("MyBudget.Customers.Api.Application.Domain.Aggregates.CustomerAccount", b =>
                {
                    b.HasOne("MyBudget.Customers.Api.Application.Domain.Aggregates.Customer")
                        .WithMany("BankAccounts")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}