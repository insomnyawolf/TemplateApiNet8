using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TemplateApiNet6.Database.Models;

namespace TemplateApiNet6.Database
{

    public partial class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Album> Albums { get; set; }

        public virtual DbSet<Artist> Artists { get; set; }

        public virtual DbSet<Customer> Customers { get; set; }

        public virtual DbSet<Employee> Employees { get; set; }

        public virtual DbSet<Genre> Genres { get; set; }

        public virtual DbSet<Invoice> Invoices { get; set; }

        public virtual DbSet<InvoiceLine> InvoiceLines { get; set; }

        public virtual DbSet<MediaType> MediaTypes { get; set; }

        public virtual DbSet<Playlist> Playlists { get; set; }

        public virtual DbSet<Track> Tracks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Album>(entity =>
            {
                entity.Property(e => e.AlbumId).ValueGeneratedNever();

                entity.HasOne(d => d.Artist).WithMany(p => p.Albums).OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Artist>(entity =>
            {
                entity.Property(e => e.ArtistId).ValueGeneratedNever();
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(e => e.CustomerId).ValueGeneratedNever();
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.Property(e => e.EmployeeId).ValueGeneratedNever();
            });

            modelBuilder.Entity<Genre>(entity =>
            {
                entity.Property(e => e.GenreId).ValueGeneratedNever();
            });

            modelBuilder.Entity<Invoice>(entity =>
            {
                entity.Property(e => e.InvoiceId).ValueGeneratedNever();

                entity.HasOne(d => d.Customer).WithMany(p => p.Invoices).OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<InvoiceLine>(entity =>
            {
                entity.Property(e => e.InvoiceLineId).ValueGeneratedNever();

                entity.HasOne(d => d.Invoice).WithMany(p => p.InvoiceLines).OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Track).WithMany(p => p.InvoiceLines).OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<MediaType>(entity =>
            {
                entity.Property(e => e.MediaTypeId).ValueGeneratedNever();
            });

            modelBuilder.Entity<Playlist>(entity =>
            {
                entity.Property(e => e.PlaylistId).ValueGeneratedNever();

                entity.HasMany(d => d.Tracks).WithMany(p => p.Playlists)
                    .UsingEntity<Dictionary<string, object>>(
                        "PlaylistTrack",
                        r => r.HasOne<Track>().WithMany()
                            .HasForeignKey("TrackId")
                            .OnDelete(DeleteBehavior.ClientSetNull),
                        l => l.HasOne<Playlist>().WithMany()
                            .HasForeignKey("PlaylistId")
                            .OnDelete(DeleteBehavior.ClientSetNull),
                        j =>
                        {
                            j.HasKey("PlaylistId", "TrackId");
                            j.ToTable("PlaylistTrack");
                            j.HasIndex(new[] { "TrackId" }, "IFK_PlaylistTrackTrackId");
                        });
            });

            modelBuilder.Entity<Track>(entity =>
            {
                entity.Property(e => e.TrackId).ValueGeneratedNever();

                entity.HasOne(d => d.MediaType).WithMany(p => p.Tracks).OnDelete(DeleteBehavior.ClientSetNull);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}