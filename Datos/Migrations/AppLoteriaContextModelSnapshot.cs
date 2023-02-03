﻿// <auto-generated />
using System;
using Datos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Datos.Migrations
{
    [DbContext(typeof(AppLoteriaContext))]
    partial class AppLoteriaContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CartaTabla", b =>
                {
                    b.Property<int>("CartasCartaID")
                        .HasColumnType("int");

                    b.Property<int>("TablasTablaID")
                        .HasColumnType("int");

                    b.HasKey("CartasCartaID", "TablasTablaID");

                    b.HasIndex("TablasTablaID");

                    b.ToTable("CartaTabla");
                });

            modelBuilder.Entity("Datos.Models.Carta", b =>
                {
                    b.Property<int>("CartaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CartaID"));

                    b.Property<string>("Imagen")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Lema")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Marcado")
                        .HasColumnType("bit");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CartaID");

                    b.ToTable("Carta", (string)null);
                });

            modelBuilder.Entity("Datos.Models.Ganador", b =>
                {
                    b.Property<int>("GanadorID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("GanadorID"));

                    b.Property<DateTime>("FechaHora")
                        .HasColumnType("datetime2");

                    b.Property<int>("TablaID")
                        .HasColumnType("int");

                    b.HasKey("GanadorID");

                    b.HasIndex("TablaID");

                    b.ToTable("Ganador", (string)null);
                });

            modelBuilder.Entity("Datos.Models.Tabla", b =>
                {
                    b.Property<int>("TablaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TablaID"));

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TablaID");

                    b.ToTable("Tabla", (string)null);
                });

            modelBuilder.Entity("CartaTabla", b =>
                {
                    b.HasOne("Datos.Models.Carta", null)
                        .WithMany()
                        .HasForeignKey("CartasCartaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Datos.Models.Tabla", null)
                        .WithMany()
                        .HasForeignKey("TablasTablaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Datos.Models.Ganador", b =>
                {
                    b.HasOne("Datos.Models.Tabla", "Tabla")
                        .WithMany("Ganadores")
                        .HasForeignKey("TablaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tabla");
                });

            modelBuilder.Entity("Datos.Models.Tabla", b =>
                {
                    b.Navigation("Ganadores");
                });
#pragma warning restore 612, 618
        }
    }
}
