using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using PostOffice.Models;

namespace PostOffice.Data;

public partial class DataContext : DbContext
{
    public DataContext()
    {
    }

    public DataContext(DbContextOptions<DataContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Branch> Branches { get; set; }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Parcel> Parcels { get; set; }

    public virtual DbSet<ParcelStatus> ParcelStatuses { get; set; }

    public virtual DbSet<ParcelTracking> ParcelTrackings { get; set; }

    public virtual DbSet<ParcelType> ParcelTypes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=DbPostOffice; User Id=sa; Password=sa; TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Branch>(entity =>
        {
            entity.HasKey(e => e.IdBranch);

            entity.ToTable("Branch");

            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.City).HasMaxLength(255);
            entity.Property(e => e.PhoneNumber).HasMaxLength(50);
            entity.Property(e => e.ZipCode).HasMaxLength(50);
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.IdClient);

            entity.ToTable("Client");

            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.City).HasMaxLength(255);
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.PhoneNumber).HasMaxLength(20);
            entity.Property(e => e.Surname).HasMaxLength(255);
            entity.Property(e => e.ZipCode).HasMaxLength(10);
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.IdEmployee);

            entity.ToTable("Employee");

            entity.Property(e => e.Name)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.Position)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.Salary).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Surname)
                .HasMaxLength(10)
                .IsFixedLength();

            entity.HasOne(d => d.IdBranchNavigation).WithMany(p => p.Employees)
                .HasForeignKey(d => d.IdBranch)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Employee_Branch");
        });

        modelBuilder.Entity<Parcel>(entity =>
        {
            entity.HasKey(e => e.IdParcel);

            entity.ToTable("Parcel");

            entity.Property(e => e.Dimensions).HasMaxLength(255);
            entity.Property(e => e.ShippingCost).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Weight).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.IdDepartureBranchNavigation).WithMany(p => p.ParcelIdDepartureBranchNavigations)
                .HasForeignKey(d => d.IdDepartureBranch)
                .HasConstraintName("FK_Parcel_DepartureBranch");

            entity.HasOne(d => d.IdDestinationBranchNavigation).WithMany(p => p.ParcelIdDestinationBranchNavigations)
                .HasForeignKey(d => d.IdDestinationBranch)
                .HasConstraintName("FK_Parcel_DestinationBranch");

            entity.HasOne(d => d.IdParcelStatusNavigation).WithMany(p => p.Parcels)
                .HasForeignKey(d => d.IdParcelStatus)
                .HasConstraintName("FK_Parcel_ParcelStatuses");

            entity.HasOne(d => d.IdParcelTypeNavigation).WithMany(p => p.Parcels)
                .HasForeignKey(d => d.IdParcelType)
                .HasConstraintName("FK_Parcel_ParcelType");

            entity.HasOne(d => d.IdRecipientNavigation).WithMany(p => p.ParcelIdRecipientNavigations)
                .HasForeignKey(d => d.IdRecipient)
                .HasConstraintName("FK_Parcel_RecipientClient");

            entity.HasOne(d => d.IdSenderNavigation).WithMany(p => p.ParcelIdSenderNavigations)
                .HasForeignKey(d => d.IdSender)
                .HasConstraintName("FK_Parcel_SenderClient");
        });

        modelBuilder.Entity<ParcelStatus>(entity =>
        {
            entity.HasKey(e => e.IdParcelStatuses);

            entity.Property(e => e.DescriptionStatus).HasMaxLength(255);
            entity.Property(e => e.StatusName).HasMaxLength(255);
        });

        modelBuilder.Entity<ParcelTracking>(entity =>
        {
            entity.HasKey(e => e.IdTracking);

            entity.ToTable("ParcelTracking");

            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.TrackingDate).HasColumnType("datetime");

            entity.HasOne(d => d.IdBranchNavigation).WithMany(p => p.ParcelTrackings)
                .HasForeignKey(d => d.IdBranch)
                .HasConstraintName("FK_ParcelTracking_Parcel");

            entity.HasOne(d => d.IdParcelNavigation).WithMany(p => p.ParcelTrackings)
                .HasForeignKey(d => d.IdParcel)
                .HasConstraintName("FK_ParcelTracking_Parcel1");

            entity.HasOne(d => d.IdPercelStatusNavigation).WithMany(p => p.ParcelTrackings)
                .HasForeignKey(d => d.IdPercelStatus)
                .HasConstraintName("FK_ParcelTracking_ParcelStatuses");
        });

        modelBuilder.Entity<ParcelType>(entity =>
        {
            entity.HasKey(e => e.IdParcelType);

            entity.ToTable("ParcelType");

            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.TypeName).HasMaxLength(255);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
