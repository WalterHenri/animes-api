using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Core.Models;

public partial class AnimeDbContext : DbContext
{
    public AnimeDbContext()
    {
    }

    public AnimeDbContext(DbContextOptions<AnimeDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Anime> Animes { get; set; }

    public virtual DbSet<Director> Directors { get; set; }

    public virtual DbSet<Genre> Genres { get; set; }

    public virtual DbSet<Studio> Studios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=AnimeDatabase;Trusted_Connection=True;Encrypt=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Anime>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Animes__3214EC07BFFD6CC8");

            entity.HasOne(d => d.Director).WithMany(p => p.Animes).HasConstraintName("FK_Animes_Directors");

            entity.HasOne(d => d.Studio).WithMany(p => p.Animes).HasConstraintName("FK_Animes_Studios");

            entity.HasMany(d => d.Genres).WithMany(p => p.Animes)
                .UsingEntity<Dictionary<string, object>>(
                    "AnimeGenre",
                    r => r.HasOne<Genre>().WithMany()
                        .HasForeignKey("GenreId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_AnimeGenres_Genres"),
                    l => l.HasOne<Anime>().WithMany()
                        .HasForeignKey("AnimeId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_AnimeGenres_Animes"),
                    j =>
                    {
                        j.HasKey("AnimeId", "GenreId");
                        j.ToTable("Anime_Genres");
                    });
        });

        modelBuilder.Entity<Director>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Director__3214EC0735252ADC");
        });

        modelBuilder.Entity<Genre>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Genres__3214EC076AC8A82C");
        });

        modelBuilder.Entity<Studio>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Studios__3214EC07631E5DD2");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
