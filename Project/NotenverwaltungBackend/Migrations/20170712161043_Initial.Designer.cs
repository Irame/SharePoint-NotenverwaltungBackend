using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using NotenverwaltungBackend.Data;

namespace NotenverwaltungBackend.Migrations
{
    [DbContext(typeof(NotenverwaltungBackendContext))]
    [Migration("20170712161043_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("NotenverwaltungBackend.Model.Fach", b =>
                {
                    b.Property<int>("FachID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("JahrgangID");

                    b.Property<string>("Name");

                    b.HasKey("FachID");

                    b.HasIndex("JahrgangID");

                    b.ToTable("Fach");
                });

            modelBuilder.Entity("NotenverwaltungBackend.Model.FachLehrer", b =>
                {
                    b.Property<int>("FachID");

                    b.Property<int>("LehrerID");

                    b.HasKey("FachID", "LehrerID");

                    b.HasIndex("LehrerID");

                    b.ToTable("FachLehrer");
                });

            modelBuilder.Entity("NotenverwaltungBackend.Model.FachSchueler", b =>
                {
                    b.Property<int>("FachID");

                    b.Property<int>("SchuelerID");

                    b.HasKey("FachID", "SchuelerID");

                    b.HasIndex("SchuelerID");

                    b.ToTable("FachSchueler");
                });

            modelBuilder.Entity("NotenverwaltungBackend.Model.Jahrgang", b =>
                {
                    b.Property<int>("JahrgangID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("JahrgangID");

                    b.ToTable("Jahrgang");
                });

            modelBuilder.Entity("NotenverwaltungBackend.Model.Klasse", b =>
                {
                    b.Property<int>("KlasseID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("JahrgangID");

                    b.Property<string>("Name");

                    b.HasKey("KlasseID");

                    b.HasIndex("JahrgangID");

                    b.ToTable("Klasse");
                });

            modelBuilder.Entity("NotenverwaltungBackend.Model.KlasseFach", b =>
                {
                    b.Property<int>("KlasseID");

                    b.Property<int>("FachID");

                    b.HasKey("KlasseID", "FachID");

                    b.HasIndex("FachID");

                    b.ToTable("KlasseFach");
                });

            modelBuilder.Entity("NotenverwaltungBackend.Model.KlasseLehrer", b =>
                {
                    b.Property<int>("KlasseID");

                    b.Property<int>("LehrerID");

                    b.HasKey("KlasseID", "LehrerID");

                    b.HasIndex("LehrerID");

                    b.ToTable("KlasseLehrer");
                });

            modelBuilder.Entity("NotenverwaltungBackend.Model.KlasseSchueler", b =>
                {
                    b.Property<int>("KlasseID");

                    b.Property<int>("SchuelerID");

                    b.HasKey("KlasseID", "SchuelerID");

                    b.HasIndex("SchuelerID");

                    b.ToTable("KlasseSchueler");
                });

            modelBuilder.Entity("NotenverwaltungBackend.Model.Lehrer", b =>
                {
                    b.Property<int>("LehrerID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("PersonID");

                    b.HasKey("LehrerID");

                    b.HasIndex("PersonID");

                    b.ToTable("Lehrer");
                });

            modelBuilder.Entity("NotenverwaltungBackend.Model.Notenerhebung", b =>
                {
                    b.Property<int>("NotenerhebungID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Bemerkung");

                    b.Property<DateTime>("Datum");

                    b.Property<int>("FachID");

                    b.Property<int>("Note");

                    b.Property<int>("SchuelerID");

                    b.Property<string>("Typ");

                    b.HasKey("NotenerhebungID");

                    b.HasIndex("FachID");

                    b.HasIndex("SchuelerID");

                    b.ToTable("Notenerhebung");
                });

            modelBuilder.Entity("NotenverwaltungBackend.Model.Person", b =>
                {
                    b.Property<int>("PersonID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Benutzername");

                    b.Property<string>("Nachname");

                    b.Property<string>("Vorname");

                    b.HasKey("PersonID");

                    b.ToTable("Person");
                });

            modelBuilder.Entity("NotenverwaltungBackend.Model.Schueler", b =>
                {
                    b.Property<int>("SchuelerID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("PersonID");

                    b.HasKey("SchuelerID");

                    b.HasIndex("PersonID");

                    b.ToTable("Schueler");
                });

            modelBuilder.Entity("NotenverwaltungBackend.Model.Fach", b =>
                {
                    b.HasOne("NotenverwaltungBackend.Model.Jahrgang", "Jahrgang")
                        .WithMany()
                        .HasForeignKey("JahrgangID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("NotenverwaltungBackend.Model.FachLehrer", b =>
                {
                    b.HasOne("NotenverwaltungBackend.Model.Fach", "Fach")
                        .WithMany("FachLehrer")
                        .HasForeignKey("FachID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("NotenverwaltungBackend.Model.Lehrer", "Lehrer")
                        .WithMany("FachLehrer")
                        .HasForeignKey("LehrerID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("NotenverwaltungBackend.Model.FachSchueler", b =>
                {
                    b.HasOne("NotenverwaltungBackend.Model.Fach", "Fach")
                        .WithMany()
                        .HasForeignKey("FachID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("NotenverwaltungBackend.Model.Schueler", "Schueler")
                        .WithMany()
                        .HasForeignKey("SchuelerID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("NotenverwaltungBackend.Model.Klasse", b =>
                {
                    b.HasOne("NotenverwaltungBackend.Model.Jahrgang", "Jahrgang")
                        .WithMany()
                        .HasForeignKey("JahrgangID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("NotenverwaltungBackend.Model.KlasseFach", b =>
                {
                    b.HasOne("NotenverwaltungBackend.Model.Fach", "Fach")
                        .WithMany("KlasseFach")
                        .HasForeignKey("FachID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("NotenverwaltungBackend.Model.Klasse", "Klasse")
                        .WithMany("KlasseFach")
                        .HasForeignKey("KlasseID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("NotenverwaltungBackend.Model.KlasseLehrer", b =>
                {
                    b.HasOne("NotenverwaltungBackend.Model.Klasse", "Klasse")
                        .WithMany("KlasseLehrer")
                        .HasForeignKey("KlasseID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("NotenverwaltungBackend.Model.Lehrer", "Lehrer")
                        .WithMany("KlasseLehrer")
                        .HasForeignKey("LehrerID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("NotenverwaltungBackend.Model.KlasseSchueler", b =>
                {
                    b.HasOne("NotenverwaltungBackend.Model.Klasse", "Klasse")
                        .WithMany("KlasseSchueler")
                        .HasForeignKey("KlasseID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("NotenverwaltungBackend.Model.Schueler", "Schueler")
                        .WithMany("KlasseSchueler")
                        .HasForeignKey("SchuelerID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("NotenverwaltungBackend.Model.Lehrer", b =>
                {
                    b.HasOne("NotenverwaltungBackend.Model.Person", "Person")
                        .WithMany()
                        .HasForeignKey("PersonID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("NotenverwaltungBackend.Model.Notenerhebung", b =>
                {
                    b.HasOne("NotenverwaltungBackend.Model.Fach", "Fach")
                        .WithMany()
                        .HasForeignKey("FachID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("NotenverwaltungBackend.Model.Schueler", "Schueler")
                        .WithMany("Noten")
                        .HasForeignKey("SchuelerID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("NotenverwaltungBackend.Model.Schueler", b =>
                {
                    b.HasOne("NotenverwaltungBackend.Model.Person", "Person")
                        .WithMany()
                        .HasForeignKey("PersonID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
