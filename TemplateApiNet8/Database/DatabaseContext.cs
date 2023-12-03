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

    public virtual DbSet<Network> Networks { get; set; }

    public virtual DbSet<ScheduleDay> ScheduleDays { get; set; }

    public virtual DbSet<Show> Shows { get; set; }

    public virtual DbSet<ShowExternal> ShowExternals { get; set; }

    public virtual DbSet<ShowGenere> ShowGeneres { get; set; }

    public virtual DbSet<ShowImage> ShowImages { get; set; }

    public virtual DbSet<ShowKind> ShowKinds { get; set; }

    public virtual DbSet<ShowLanguage> ShowLanguages { get; set; }

    public virtual DbSet<ShowLink> ShowLinks { get; set; }

    public virtual DbSet<ShowNetwork> ShowNetworks { get; set; }

    public virtual DbSet<ShowRating> ShowRatings { get; set; }

    public virtual DbSet<ShowSchedule> ShowSchedules { get; set; }

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

        modelBuilder.Entity<Network>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<ScheduleDay>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<Show>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<ShowExternal>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<ShowGenere>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<ShowImage>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<ShowKind>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<ShowLanguage>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<ShowLink>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Previous).WithMany(p => p.ShowLinkPrevious).OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(d => d.Self).WithMany(p => p.ShowLinkSelves).OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<ShowNetwork>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<ShowRating>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<ShowSchedule>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<ShowStatus>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
