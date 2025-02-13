﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NCU_SE.Data;

namespace NCU_SE.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20211211072432_updateMember_date2string")]
    partial class updateMember_date2string
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.12")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("NCU_SE.Models.Member", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Birthday")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordResetLink")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("RegisterDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("ValidState")
                        .HasColumnType("int");

                    b.Property<string>("ValidationLink")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("profile")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("Member");
                });

            modelBuilder.Entity("NCU_SE.Models.Ticket", b =>
                {
                    b.Property<string>("TicketID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("ActualArrivedTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ActualDepartureDateTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ArriveDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("DepartAirport")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DepartureDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("DestinationAirport")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FlightID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MemberID")
                        .HasColumnType("int");

                    b.HasKey("TicketID");

                    b.ToTable("Ticket");
                });
#pragma warning restore 612, 618
        }
    }
}
