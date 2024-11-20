using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Harmony.Repositories.Entities;

public partial class HarmonySalonContext : DbContext
{
    public HarmonySalonContext()
    {
    }

    public HarmonySalonContext(DbContextOptions<HarmonySalonContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Appointment> Appointments { get; set; }

    public virtual DbSet<Cart> Carts { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Dashboard> Dashboards { get; set; }

    public virtual DbSet<Manager> Managers { get; set; }

    public virtual DbSet<SalonStaff> SalonStaffs { get; set; }

    public virtual DbSet<Service> Services { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Voucher> Vouchers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=HOANGDZ\\SQLSV;Initial Catalog=HarmonySalon;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Appointment>(entity =>
        {
            entity.HasKey(e => e.AppointmentId).HasName("PK__Appointm__8ECDFCC2785B5EF8");

            entity.ToTable("Appointment");

            entity.Property(e => e.AppointmentDate).HasMaxLength(50);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValue("Waiting");

            entity.HasOne(d => d.Customer).WithMany(p => p.AppointmentCustomers)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK__Appointme__Custo__0C85DE4D");

            entity.HasOne(d => d.Service).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.ServiceId)
                .HasConstraintName("FK__Appointme__Servi__0B91BA14");

            entity.HasOne(d => d.Stylist).WithMany(p => p.AppointmentStylists)
                .HasForeignKey(d => d.StylistId)
                .HasConstraintName("FK__Appointme__Styli__0A9D95DB");
        });

        modelBuilder.Entity<Cart>(entity =>
        {
            entity.HasKey(e => e.CartId).HasName("PK__Cart__51BCD7B7B046855A");

            entity.ToTable("Cart");

            entity.Property(e => e.Items).HasMaxLength(255);
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Customer).WithMany(p => p.Carts)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK__Cart__CustomerId__52593CB8");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK__Customer__A4AE64D873D31108");

            entity.ToTable("Customer");

            entity.Property(e => e.CustomerId).ValueGeneratedNever();
            entity.Property(e => e.Appointments).HasMaxLength(200);
            entity.Property(e => e.LoyaltyPoints).HasDefaultValue(0);

            entity.HasOne(d => d.CustomerNavigation).WithOne(p => p.Customer)
                .HasForeignKey<Customer>(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Customer__Custom__3C69FB99");
        });

        modelBuilder.Entity<Dashboard>(entity =>
        {
            entity.HasKey(e => e.DashboardId).HasName("PK__Dashboar__C711E1D00E49EB84");

            entity.ToTable("Dashboard");

            entity.Property(e => e.Reports).HasMaxLength(255);
            entity.Property(e => e.Statistics).HasMaxLength(255);
        });

        modelBuilder.Entity<Manager>(entity =>
        {
            entity.HasKey(e => e.ManagerId).HasName("PK__Manager__3BA2AAE1ED99B6A4");

            entity.ToTable("Manager");

            entity.Property(e => e.ManagerId).ValueGeneratedNever();
            entity.Property(e => e.Permissions).HasMaxLength(255);

            entity.HasOne(d => d.ManagerNavigation).WithOne(p => p.Manager)
                .HasForeignKey<Manager>(d => d.ManagerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Manager__Manager__5CD6CB2B");
        });

        modelBuilder.Entity<SalonStaff>(entity =>
        {
            entity.HasKey(e => e.StaffId).HasName("PK__SalonSta__96D4AB17B36CF8D5");

            entity.ToTable("SalonStaff");

            entity.Property(e => e.StaffId).ValueGeneratedNever();
            entity.Property(e => e.Availability).HasMaxLength(200);

            entity.HasOne(d => d.Staff).WithOne(p => p.SalonStaff)
                .HasForeignKey<SalonStaff>(d => d.StaffId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SalonStaf__Staff__3F466844");
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.HasKey(e => e.ServiceId).HasName("PK__Service__C51BB00ACA85BAF1");

            entity.ToTable("Service");

            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__User__1788CC4C35781383");

            entity.ToTable("User");

            entity.HasIndex(e => e.Email, "UQ__User__A9D1053435B10410").IsUnique();

            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Password).HasMaxLength(100);
            entity.Property(e => e.Phone).HasMaxLength(15);
            entity.Property(e => e.UserType).HasMaxLength(50);
        });

        modelBuilder.Entity<Voucher>(entity =>
        {
            entity.HasKey(e => e.VoucherId).HasName("PK__Voucher__3AEE79219C738560");

            entity.ToTable("Voucher");

            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.DiscountRate).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.ValidityPeriod).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
