using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Movies.Models;

public partial class DbMoviesContext : DbContext
{
    public DbMoviesContext()
    {
    }

    public DbMoviesContext(DbContextOptions<DbMoviesContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ListFavorite> ListFavorites { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
 
        => optionsBuilder.UseSqlServer("Server=LAPTOP-7GRBDLD0\\SQLEXPRESS;Database=DB_Movies;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ListFavorite>(entity =>
        {
            entity.HasKey(e => new { e.IdUser, e.IdMovie }).HasName("PK__listFavo__BD6849FAABDE22A1");

            entity.ToTable("listFavorites");

            entity.Property(e => e.IdUser).HasColumnName("id_user");
            entity.Property(e => e.IdMovie).HasColumnName("id_movie");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.ListFavorites)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__listFavor__id_us__628FA481");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__usuario__3213E83FEE46A1A3");

            entity.ToTable("usuario");

            entity.HasIndex(e => e.Username, "UQ__usuario__F3DBC572E5952CB9").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Nam)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nam");
            entity.Property(e => e.Pass)
                .HasMaxLength(16)
                .IsUnicode(false)
                .HasColumnName("pass");
            entity.Property(e => e.Username)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("username");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
