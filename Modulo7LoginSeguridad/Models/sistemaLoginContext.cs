using System;
using System.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Modulo7LoginSeguridad.Models
{
    public partial class sistemaLoginContext : DbContext
    {
        public sistemaLoginContext()
        {
        }

        public sistemaLoginContext(DbContextOptions<sistemaLoginContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TablaUsuarios> TablaUsuarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                    
                optionsBuilder.UseSqlServer(ConfigurationManager.AppSettings["defaultConnection"]);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TablaUsuarios>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.idPruenbas).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.idUsuario).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.idempresa).HasColumnType("decimal(18, 0)");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
