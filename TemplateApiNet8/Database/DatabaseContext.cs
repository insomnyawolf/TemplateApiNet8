using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TemplateApiNet8.Database.Models;

namespace TemplateApiNet8.Database;

public partial class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<CountryNetwork> CountryNetworks { get; set; }

    public virtual DbSet<Day> Days { get; set; }

    public virtual DbSet<Episode> Episodes { get; set; }

    public virtual DbSet<External> Externals { get; set; }

    public virtual DbSet<Genere> Generes { get; set; }

    public virtual DbSet<Image> Images { get; set; }

    public virtual DbSet<Kind> Kinds { get; set; }

    public virtual DbSet<Language> Languages { get; set; }

    public virtual DbSet<Link> Links { get; set; }

    public virtual DbSet<Network> Networks { get; set; }

    public virtual DbSet<Rating> Ratings { get; set; }

    public virtual DbSet<Schedule> Schedules { get; set; }

    public virtual DbSet<ScheduleDay> ScheduleDays { get; set; }

    public virtual DbSet<Show> Shows { get; set; }

    public virtual DbSet<ShowExternal> ShowExternals { get; set; }

    public virtual DbSet<ShowGenere> ShowGeneres { get; set; }

    public virtual DbSet<ShowKind> ShowKinds { get; set; }

    public virtual DbSet<ShowLanguage> ShowLanguages { get; set; }

    public virtual DbSet<ShowNetwork> ShowNetworks { get; set; }

    public virtual DbSet<ShowStatus> ShowStatuses { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Country>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<CountryNetwork>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Country).WithMany(p => p.CountryNetworks).OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Network).WithMany(p => p.CountryNetworks).OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Day>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<Episode>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<External>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<Genere>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<Image>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<Kind>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<Language>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<Link>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<Network>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<Rating>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Show).WithMany(p => p.Ratings).OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Schedule>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Show).WithMany(p => p.Schedules).OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<ScheduleDay>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Day).WithMany(p => p.ScheduleDays).OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Schedule).WithMany(p => p.ScheduleDays).OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Show>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<ShowExternal>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.External).WithMany(p => p.ShowExternals).OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Show).WithMany(p => p.ShowExternals).OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<ShowGenere>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Genere).WithMany(p => p.ShowGeneres).OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Show).WithMany(p => p.ShowGeneres).OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<ShowKind>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Kind).WithMany(p => p.ShowKinds).OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Show).WithMany(p => p.ShowKinds).OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<ShowLanguage>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Language).WithMany(p => p.ShowLanguages).OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Show).WithMany(p => p.ShowLanguages).OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<ShowNetwork>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Network).WithMany(p => p.ShowNetworks).OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Show).WithMany(p => p.ShowNetworks).OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<ShowStatus>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Show).WithMany(p => p.ShowStatuses).OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Status).WithMany(p => p.ShowStatuses).OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
