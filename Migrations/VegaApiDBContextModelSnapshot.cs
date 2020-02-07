﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using VegaIoTApi.Data;

namespace VegaIoTApi.Migrations
{
    [DbContext(typeof(VegaApiDBContext))]
    partial class VegaApiDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("VegaIoTWebService.Data.Models.VegaDevice", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("DeviceType")
                        .HasColumnType("integer");

                    b.Property<string>("Eui")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Eui");

                    b.ToTable("TempDevices");
                });

            modelBuilder.Entity("VegaIoTWebService.Data.Models.VegaImpulsDeviceData", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<byte>("AlarmExit")
                        .HasColumnType("smallint");

                    b.Property<short>("BatteryLevel")
                        .HasColumnType("smallint");

                    b.Property<long>("DeviceId")
                        .HasColumnType("bigint");

                    b.Property<long>("InputState1")
                        .HasColumnType("bigint");

                    b.Property<long>("InputState2")
                        .HasColumnType("bigint");

                    b.Property<long>("InputState3")
                        .HasColumnType("bigint");

                    b.Property<long>("InputState4")
                        .HasColumnType("bigint");

                    b.Property<byte>("MainSettings")
                        .HasColumnType("smallint");

                    b.Property<byte>("PackageType")
                        .HasColumnType("smallint");

                    b.Property<double>("Temperature")
                        .HasColumnType("double precision");

                    b.Property<DateTimeOffset>("UpTime")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("DeviceId");

                    b.ToTable("ImpulsDeviceDatas");
                });

            modelBuilder.Entity("VegaIoTWebService.Data.Models.VegaMoveDeviceData", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<short>("BatteryLevel")
                        .HasColumnType("smallint");

                    b.Property<long>("DeviceId")
                        .HasColumnType("bigint");

                    b.Property<short>("Reason")
                        .HasColumnType("smallint");

                    b.Property<double>("Temperature")
                        .HasColumnType("double precision");

                    b.Property<DateTimeOffset>("Uptime")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("DeviceId");

                    b.ToTable("MoveDeviceDatas");
                });

            modelBuilder.Entity("VegaIoTWebService.Data.Models.VegaTempDeviceData", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<short>("BatteryLevel")
                        .HasColumnType("smallint");

                    b.Property<long>("DeviceId")
                        .HasColumnType("bigint");

                    b.Property<short>("HighLimit")
                        .HasColumnType("smallint");

                    b.Property<short>("LowLimit")
                        .HasColumnType("smallint");

                    b.Property<bool>("PushTheLimit")
                        .HasColumnType("boolean");

                    b.Property<double>("Temperature")
                        .HasColumnType("double precision");

                    b.Property<DateTimeOffset>("Uptime")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("DeviceId");

                    b.ToTable("TempDeviceData");
                });

            modelBuilder.Entity("VegaIoTWebService.Data.Models.VegaImpulsDeviceData", b =>
                {
                    b.HasOne("VegaIoTWebService.Data.Models.VegaDevice", "Device")
                        .WithMany()
                        .HasForeignKey("DeviceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("VegaIoTWebService.Data.Models.VegaMoveDeviceData", b =>
                {
                    b.HasOne("VegaIoTWebService.Data.Models.VegaDevice", "Device")
                        .WithMany()
                        .HasForeignKey("DeviceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("VegaIoTWebService.Data.Models.VegaTempDeviceData", b =>
                {
                    b.HasOne("VegaIoTWebService.Data.Models.VegaDevice", "Device")
                        .WithMany()
                        .HasForeignKey("DeviceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
