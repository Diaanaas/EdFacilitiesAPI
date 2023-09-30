using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace EdFacilitiesAPI.Models;

public partial class EducationalDbContext : DbContext
{
    public EducationalDbContext()
    {
    }

    public EducationalDbContext(DbContextOptions<EducationalDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Facility> Facilities { get; set; }

    public virtual DbSet<History> Histories { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Type");

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Facility>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Places");

            entity.Property(e => e.Description).HasMaxLength(50);
            entity.Property(e => e.Location).HasMaxLength(100);

            entity.HasOne(d => d.Category).WithMany(p => p.Facilities)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Facility_Category");
        });

        modelBuilder.Entity<History>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Ratings");

            entity.ToTable("History");

            entity.Property(e => e.DateEnd).HasColumnType("date");
            entity.Property(e => e.DateStart).HasColumnType("date");

            entity.HasOne(d => d.Facility).WithMany(p => p.Histories)
                .HasForeignKey(d => d.FacilityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_History_Facility");

            entity.HasOne(d => d.Student).WithMany(p => p.Histories)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_History_Student");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Clients");

            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Surname).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
