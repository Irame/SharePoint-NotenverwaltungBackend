using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using NotenverwaltungBackend.Models;
using NotenverwaltungBackend.Model;

namespace NotenverwaltungBackend.Migrations
{
    [DbContext(typeof(SchuelerNotenContext))]
    [Migration("20170706160313_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("NotenverwaltungBackend.Model.Fach", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<string>("SchuelerNotenBenutzername");

                    b.HasKey("ID");

                    b.HasIndex("SchuelerNotenBenutzername");

                    b.ToTable("Fach");
                });

            modelBuilder.Entity("NotenverwaltungBackend.Model.Leistungserhebung", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Datum");

                    b.Property<int?>("FachID");

                    b.Property<double>("Note");

                    b.Property<int>("Typ");

                    b.HasKey("ID");

                    b.HasIndex("FachID");

                    b.ToTable("Leistungserhebung");
                });

            modelBuilder.Entity("NotenverwaltungBackend.Model.SchuelerNoten", b =>
                {
                    b.Property<string>("Benutzername")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Halbjahrenszeugnis");

                    b.Property<string>("Jahreszeugnis");

                    b.Property<string>("Klasse");

                    b.Property<string>("Nachname");

                    b.Property<string>("Vorname");

                    b.HasKey("Benutzername");

                    b.ToTable("SchuelerNoten");
                });

            modelBuilder.Entity("NotenverwaltungBackend.Model.Fach", b =>
                {
                    b.HasOne("NotenverwaltungBackend.Model.SchuelerNoten")
                        .WithMany("Feacher")
                        .HasForeignKey("SchuelerNotenBenutzername");
                });

            modelBuilder.Entity("NotenverwaltungBackend.Model.Leistungserhebung", b =>
                {
                    b.HasOne("NotenverwaltungBackend.Model.Fach")
                        .WithMany("Noten")
                        .HasForeignKey("FachID");
                });
        }
    }
}
