﻿// <auto-generated />
using System;
using GB_API.Server.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace GBAPI.Migrations
{
    [DbContext(typeof(MICDbContext))]
    [Migration("20221113153716_InitialMigration")]
    partial class InitialMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("MIC-DB")
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseSerialColumns(modelBuilder);

            modelBuilder.Entity("GB_API.Server.Domain.Dienst", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseSerialColumn(b.Property<long>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("dienst", "MIC-DB");
                });

            modelBuilder.Entity("GB_API.Server.Domain.Incident", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseSerialColumn(b.Property<long>("Id"));

                    b.Property<long>("LocatieId")
                        .HasColumnType("bigint");

                    b.Property<long>("MeldingsclassificatieId")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("LocatieId");

                    b.HasIndex("MeldingsclassificatieId");

                    b.ToTable("incident", "MIC-DB");
                });

            modelBuilder.Entity("GB_API.Server.Domain.Intensiteit", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseSerialColumn(b.Property<long>("Id"));

                    b.Property<long>("DienstId")
                        .HasColumnType("bigint");

                    b.Property<long?>("IncidentId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("DienstId");

                    b.HasIndex("IncidentId");

                    b.ToTable("intensiteit", "MIC-DB");
                });

            modelBuilder.Entity("GB_API.Server.Domain.Karakteristiek", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseSerialColumn(b.Property<long>("Id"));

                    b.Property<string>("Naam")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("VolgNr")
                        .HasColumnType("integer");

                    b.Property<string>("Waarde")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("karakteristiek", "MIC-DB");
                });

            modelBuilder.Entity("GB_API.Server.Domain.KarakteristiekIntensiteit", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseSerialColumn(b.Property<long>("Id"));

                    b.Property<long>("DienstId")
                        .HasColumnType("bigint");

                    b.Property<long>("KarakteristiekId")
                        .HasColumnType("bigint");

                    b.Property<int>("Punten")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("DienstId");

                    b.HasIndex("KarakteristiekId");

                    b.ToTable("kIntensiteit", "MIC-DB");
                });

            modelBuilder.Entity("GB_API.Server.Domain.Locatie", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseSerialColumn(b.Property<long>("Id"));

                    b.Property<string>("Huisnummer")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double>("Latitude")
                        .HasColumnType("double precision");

                    b.Property<double>("Longtitude")
                        .HasColumnType("double precision");

                    b.Property<string>("Postcode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Straat")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("locatie", "MIC-DB");
                });

            modelBuilder.Entity("GB_API.Server.Domain.Meldingsclassificatie", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseSerialColumn(b.Property<long>("Id"));

                    b.Property<string>("Afkorting")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Definitie")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Niveau1")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Niveau2")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Niveau3")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PresentatieTekst")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("meldingClassificatie", "MIC-DB");
                });

            modelBuilder.Entity("GB_API.Server.Domain.MeldingsclassificatieIntensiteit", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseSerialColumn(b.Property<long>("Id"));

                    b.Property<long>("DienstId")
                        .HasColumnType("bigint");

                    b.Property<long>("MeldingsclassificatieId")
                        .HasColumnType("bigint");

                    b.Property<int>("Punten")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("DienstId");

                    b.HasIndex("MeldingsclassificatieId");

                    b.ToTable("mIntensiteit", "MIC-DB");
                });

            modelBuilder.Entity("IncidentKarakteristiek", b =>
                {
                    b.Property<long>("KarakteristiekenId")
                        .HasColumnType("bigint");

                    b.Property<long>("_incidentsId")
                        .HasColumnType("bigint");

                    b.HasKey("KarakteristiekenId", "_incidentsId");

                    b.HasIndex("_incidentsId");

                    b.ToTable("Karakteristiek_Incident", "MIC-DB");
                });

            modelBuilder.Entity("GB_API.Server.Domain.Incident", b =>
                {
                    b.HasOne("GB_API.Server.Domain.Locatie", "Locatie")
                        .WithMany()
                        .HasForeignKey("LocatieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GB_API.Server.Domain.Meldingsclassificatie", "Meldingsclassificatie")
                        .WithMany()
                        .HasForeignKey("MeldingsclassificatieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Locatie");

                    b.Navigation("Meldingsclassificatie");
                });

            modelBuilder.Entity("GB_API.Server.Domain.Intensiteit", b =>
                {
                    b.HasOne("GB_API.Server.Domain.Dienst", "Dienst")
                        .WithMany()
                        .HasForeignKey("DienstId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GB_API.Server.Domain.Incident", null)
                        .WithMany("Intensiteiten")
                        .HasForeignKey("IncidentId");

                    b.Navigation("Dienst");
                });

            modelBuilder.Entity("GB_API.Server.Domain.KarakteristiekIntensiteit", b =>
                {
                    b.HasOne("GB_API.Server.Domain.Dienst", "Dienst")
                        .WithMany("KarakteristiekIntensiteiten")
                        .HasForeignKey("DienstId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GB_API.Server.Domain.Karakteristiek", "Karakteristiek")
                        .WithMany()
                        .HasForeignKey("KarakteristiekId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Dienst");

                    b.Navigation("Karakteristiek");
                });

            modelBuilder.Entity("GB_API.Server.Domain.MeldingsclassificatieIntensiteit", b =>
                {
                    b.HasOne("GB_API.Server.Domain.Dienst", "Dienst")
                        .WithMany("MeldingsclassificatieIntensiteiten")
                        .HasForeignKey("DienstId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GB_API.Server.Domain.Meldingsclassificatie", "Meldingsclassificatie")
                        .WithMany()
                        .HasForeignKey("MeldingsclassificatieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Dienst");

                    b.Navigation("Meldingsclassificatie");
                });

            modelBuilder.Entity("IncidentKarakteristiek", b =>
                {
                    b.HasOne("GB_API.Server.Domain.Karakteristiek", null)
                        .WithMany()
                        .HasForeignKey("KarakteristiekenId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GB_API.Server.Domain.Incident", null)
                        .WithMany()
                        .HasForeignKey("_incidentsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GB_API.Server.Domain.Dienst", b =>
                {
                    b.Navigation("KarakteristiekIntensiteiten");

                    b.Navigation("MeldingsclassificatieIntensiteiten");
                });

            modelBuilder.Entity("GB_API.Server.Domain.Incident", b =>
                {
                    b.Navigation("Intensiteiten");
                });
#pragma warning restore 612, 618
        }
    }
}
