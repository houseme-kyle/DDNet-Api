﻿// <auto-generated />
using System;
using DDNet.Infrastructure.SqlServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DDNet.Infrastructure.MySqlServer.Migrations
{
    [DbContext(typeof(DdNetDbContext))]
    [Migration("20210314225053_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.4");

            modelBuilder.Entity("DDNet.Infrastructure.SqlServer.Models.ClanTable", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("CountryCode")
                        .HasMaxLength(3)
                        .HasColumnType("varchar(3)");

                    b.Property<string>("Name")
                        .HasMaxLength(16)
                        .HasColumnType("varchar(16)");

                    b.HasKey("Id");

                    b.ToTable("Clan");
                });

            modelBuilder.Entity("DDNet.Infrastructure.SqlServer.Models.DifficultyTierTable", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Difficulty")
                        .HasColumnType("int");

                    b.Property<int>("Points")
                        .HasColumnType("int");

                    b.Property<int>("Tier")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("DifficultyTier");
                });

            modelBuilder.Entity("DDNet.Infrastructure.SqlServer.Models.MapTable", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("DifficultyTierId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasMaxLength(16)
                        .HasColumnType("varchar(16)");

                    b.Property<int>("TierId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DifficultyTierId");

                    b.ToTable("Map");
                });

            modelBuilder.Entity("DDNet.Infrastructure.SqlServer.Models.PinTable", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("Instantiated")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Lookup")
                        .HasMaxLength(80)
                        .HasColumnType("varchar(80)");

                    b.Property<string>("Salt")
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)");

                    b.HasKey("Id");

                    b.ToTable("Pins");
                });

            modelBuilder.Entity("DDNet.Infrastructure.SqlServer.Models.RaceCheckpointTable", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Checkpoint")
                        .HasColumnType("int");

                    b.Property<int>("RaceId")
                        .HasColumnType("int");

                    b.Property<decimal>("Time")
                        .HasColumnType("decimal(65,30)");

                    b.HasKey("Id");

                    b.HasIndex("RaceId");

                    b.ToTable("RaceCheckpoint");
                });

            modelBuilder.Entity("DDNet.Infrastructure.SqlServer.Models.RaceTable", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("MapId")
                        .HasColumnType("int");

                    b.Property<string>("RaceCode")
                        .HasMaxLength(36)
                        .HasColumnType("varchar(36)");

                    b.Property<int>("ServerId")
                        .HasColumnType("int");

                    b.Property<bool>("Team")
                        .HasColumnType("tinyint(1)");

                    b.Property<decimal>("Time")
                        .HasColumnType("decimal(65,30)");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MapId");

                    b.HasIndex("ServerId");

                    b.HasIndex("UserId");

                    b.ToTable("Race");
                });

            modelBuilder.Entity("DDNet.Infrastructure.SqlServer.Models.ServerTable", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Difficulty")
                        .HasMaxLength(16)
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasMaxLength(16)
                        .HasColumnType("varchar(16)");

                    b.Property<string>("Region")
                        .HasMaxLength(3)
                        .HasColumnType("varchar(3)");

                    b.HasKey("Id");

                    b.ToTable("Server");
                });

            modelBuilder.Entity("DDNet.Infrastructure.SqlServer.Models.UserRoleTable", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("UserRole");
                });

            modelBuilder.Entity("DDNet.Infrastructure.SqlServer.Models.UserTable", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<Guid>("AccessKey")
                        .HasColumnType("char(36)");

                    b.Property<int?>("ClanId")
                        .HasColumnType("int");

                    b.Property<string>("CountryCode")
                        .HasMaxLength(3)
                        .HasColumnType("varchar(3)");

                    b.Property<string>("EmailHash")
                        .HasMaxLength(80)
                        .HasColumnType("varchar(80)");

                    b.Property<string>("Name")
                        .HasMaxLength(16)
                        .HasColumnType("varchar(16)");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("ClanId");

                    b.ToTable("User");
                });

            modelBuilder.Entity("DDNet.Infrastructure.SqlServer.Models.MapTable", b =>
                {
                    b.HasOne("DDNet.Infrastructure.SqlServer.Models.DifficultyTierTable", "DifficultyTier")
                        .WithMany()
                        .HasForeignKey("DifficultyTierId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DifficultyTier");
                });

            modelBuilder.Entity("DDNet.Infrastructure.SqlServer.Models.RaceCheckpointTable", b =>
                {
                    b.HasOne("DDNet.Infrastructure.SqlServer.Models.RaceTable", "Race")
                        .WithMany()
                        .HasForeignKey("RaceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Race");
                });

            modelBuilder.Entity("DDNet.Infrastructure.SqlServer.Models.RaceTable", b =>
                {
                    b.HasOne("DDNet.Infrastructure.SqlServer.Models.MapTable", "Map")
                        .WithMany()
                        .HasForeignKey("MapId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DDNet.Infrastructure.SqlServer.Models.ServerTable", "Server")
                        .WithMany()
                        .HasForeignKey("ServerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DDNet.Infrastructure.SqlServer.Models.UserTable", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Map");

                    b.Navigation("Server");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DDNet.Infrastructure.SqlServer.Models.UserTable", b =>
                {
                    b.HasOne("DDNet.Infrastructure.SqlServer.Models.ClanTable", "Clan")
                        .WithMany()
                        .HasForeignKey("ClanId");

                    b.Navigation("Clan");
                });
#pragma warning restore 612, 618
        }
    }
}
