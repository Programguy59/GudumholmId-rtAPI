﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GudumholmIdærtAPI.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20241215203505_Addsporttoactivemember3")]
    partial class Addsporttoactivemember3
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("GudumholmIdærtAPI.Models.House", b =>
                {
                    b.Property<int>("HouseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("HouseId"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HouseName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("HouseId");

                    b.ToTable("Houses");
                });

            modelBuilder.Entity("GudumholmIdærtAPI.Models.Sport", b =>
                {
                    b.Property<int>("SportId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SportId"));

                    b.Property<int>("ActiveMemberId")
                        .HasColumnType("int");

                    b.Property<string>("SportName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("YearlyFee")
                        .HasColumnType("int");

                    b.HasKey("SportId");

                    b.HasIndex("ActiveMemberId");

                    b.ToTable("Sports");
                });

            modelBuilder.Entity("Member", b =>
                {
                    b.Property<int>("MemberId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MemberId"));

                    b.Property<string>("CprNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("HouseId")
                        .HasColumnType("int");

                    b.Property<string>("MemberType")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("nvarchar(13)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MemberId");

                    b.HasIndex("HouseId");

                    b.ToTable("Members", (string)null);

                    b.HasDiscriminator<string>("MemberType").HasValue("Member");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("ActiveMember", b =>
                {
                    b.HasBaseType("Member");

                    b.HasDiscriminator().HasValue("Active");
                });

            modelBuilder.Entity("BestyrelseMember", b =>
                {
                    b.HasBaseType("Member");

                    b.Property<string>("SportsName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue("Bestyrelse");
                });

            modelBuilder.Entity("ParantMember", b =>
                {
                    b.HasBaseType("Member");

                    b.Property<string>("AmountOfChildren")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue("Parant");
                });

            modelBuilder.Entity("PassiveMember", b =>
                {
                    b.HasBaseType("Member");

                    b.Property<string>("TimeSincePassive")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue("Passive");
                });

            modelBuilder.Entity("GudumholmIdærtAPI.Models.Sport", b =>
                {
                    b.HasOne("ActiveMember", "ActiveMember")
                        .WithMany("Sports")
                        .HasForeignKey("ActiveMemberId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ActiveMember");
                });

            modelBuilder.Entity("Member", b =>
                {
                    b.HasOne("GudumholmIdærtAPI.Models.House", "House")
                        .WithMany("Members")
                        .HasForeignKey("HouseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("House");
                });

            modelBuilder.Entity("GudumholmIdærtAPI.Models.House", b =>
                {
                    b.Navigation("Members");
                });

            modelBuilder.Entity("ActiveMember", b =>
                {
                    b.Navigation("Sports");
                });
#pragma warning restore 612, 618
        }
    }
}
