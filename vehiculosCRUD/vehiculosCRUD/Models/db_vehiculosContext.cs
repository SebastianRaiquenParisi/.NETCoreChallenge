using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace vehiculosCRUD.Models
{
    public partial class db_vehiculosContext : DbContext
    {
        public db_vehiculosContext()
        {
        }

        public db_vehiculosContext(DbContextOptions<db_vehiculosContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Marca> Marcas { get; set; } = null!;
        public virtual DbSet<Propietario> Propietarios { get; set; } = null!;
        public virtual DbSet<Vehiculo> Vehiculos { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Marca>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("nombre");
            });

            modelBuilder.Entity<Propietario>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Apellido)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("apellido");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("nombre");
            });

            modelBuilder.Entity<Vehiculo>(entity =>
            {
                entity.HasKey(e => e.Patente);

                entity.Property(e => e.Patente)
                    .HasMaxLength(7)
                    .IsUnicode(false)
                    .HasColumnName("patente");

                entity.Property(e => e.CantPuertas).HasColumnName("cantPuertas");

                entity.Property(e => e.IdMarca).HasColumnName("idMarca");

                entity.Property(e => e.IdPropietario).HasColumnName("idPropietario");

                entity.Property(e => e.Modelo)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("modelo");

                entity.HasOne(d => d.Marca)
                    .WithMany(p => p.Vehiculos)
                    .HasForeignKey(d => d.IdMarca)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Vehiculos_Marcas");

                entity.HasOne(d => d.Propietario)
                    .WithMany(p => p.Vehiculos)
                    .HasForeignKey(d => d.IdPropietario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Vehiculos_Propietarios");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
