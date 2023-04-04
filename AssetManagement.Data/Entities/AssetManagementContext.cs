using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AssetManagement.Data;

public partial class AssetManagementContext : DbContext
{
    public AssetManagementContext()
    {
    }

    public AssetManagementContext(DbContextOptions<AssetManagementContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Asset> Assets { get; set; }

    public virtual DbSet<AssetAttribute> AssetAttributes { get; set; }

    public virtual DbSet<AssetCategory> AssetCategories { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<Service> Services { get; set; }

    public virtual DbSet<ServiceCycle> ServiceCycles { get; set; }

    public virtual DbSet<Station> Stations { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Database=AssetManagement;Username=postgres;Password=computers044");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Asset>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Asset_pkey");

            entity.ToTable("Asset");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreationDate).HasColumnType("timestamp without time zone");
            entity.Property(e => e.Make).HasMaxLength(512);
            entity.Property(e => e.Name).HasMaxLength(512);
            entity.Property(e => e.Number).HasMaxLength(128);
            entity.Property(e => e.SerialNumber).HasMaxLength(128);

            entity.HasOne(d => d.Category).WithMany(p => p.Assets)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("CategoryId_fk");

            entity.HasOne(d => d.Creator).WithMany(p => p.Assets)
                .HasForeignKey(d => d.CreatorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("CreatorId_fk");

            entity.HasOne(d => d.ServiceCycle).WithMany(p => p.Assets)
                .HasForeignKey(d => d.ServiceCycleId)
                .HasConstraintName("ServiceCycleId_fk");

            entity.HasOne(d => d.Station).WithMany(p => p.Assets)
                .HasForeignKey(d => d.StationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("StationId_fk");
        });

        modelBuilder.Entity<AssetAttribute>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("AssetAttribute_pkey");

            entity.ToTable("AssetAttribute");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(512);

            entity.HasOne(d => d.Asset).WithMany(p => p.AssetAttributes)
                .HasForeignKey(d => d.AssetId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("AssetId_fk");
        });

        modelBuilder.Entity<AssetCategory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("AssetCategory_pkey");

            entity.ToTable("AssetCategory");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(512);
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Notification_pkey");

            entity.ToTable("Notification");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Asset).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.AssetId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("AssetId_fk");
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Service_pkey");

            entity.ToTable("Service");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Asset).WithMany(p => p.Services)
                .HasForeignKey(d => d.AssetId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("AssetId_fk");
        });

        modelBuilder.Entity<ServiceCycle>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ServiceCycle_pkey");

            entity.ToTable("ServiceCycle");

            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<Station>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Station_pkey");

            entity.ToTable("Station");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(512);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("User_pkey");

            entity.ToTable("User");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.AuthRecoveryCodes).HasMaxLength(512);
            entity.Property(e => e.AuthenticatorKey).HasMaxLength(128);
            entity.Property(e => e.Email).HasMaxLength(128);
            entity.Property(e => e.LoginId).HasMaxLength(32);
            entity.Property(e => e.Mobile).HasMaxLength(32);
            entity.Property(e => e.Name).HasMaxLength(256);
            entity.Property(e => e.PasswordHash).HasMaxLength(256);
            entity.Property(e => e.SecurityStamp).HasMaxLength(256);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
