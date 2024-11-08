using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace HarmonySalon.Reponsitories.Entities;

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

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<SalonStaff> SalonStaffs { get; set; }

    public virtual DbSet<Service> Services { get; set; }

    public virtual DbSet<Stylist> Stylists { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Voucher> Vouchers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=HOANGDZ\\SQLSV;Initial Catalog=HarmonySalon;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Appointment>(entity =>
        {
            entity.HasKey(e => e.AppointmentId).HasName("PK__Appointm__8ECDFCC2511DE8FF");

            entity.ToTable("Appointment");

            entity.Property(e => e.AppointmentDate).HasColumnType("datetime");
            entity.Property(e => e.Status).HasMaxLength(50);

            entity.HasOne(d => d.Customer).WithMany(p => p.AppointmentsNavigation)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK__Appointme__Custo__48CFD27E");

            entity.HasOne(d => d.Service).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.ServiceId)
                .HasConstraintName("FK__Appointme__Servi__4AB81AF0");

            entity.HasOne(d => d.Stylist).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.StylistId)
                .HasConstraintName("FK__Appointme__Styli__49C3F6B7");
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

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PK__Payment__9B556A389226035D");

            entity.ToTable("Payment");

            entity.Property(e => e.PaymentDate).HasColumnType("datetime");
            entity.Property(e => e.PaymentMethod).HasMaxLength(50);
            entity.Property(e => e.PaymentStatus).HasMaxLength(50);

            entity.HasOne(d => d.Appointment).WithMany(p => p.Payments)
                .HasForeignKey(d => d.AppointmentId)
                .HasConstraintName("FK__Payment__Appoint__4F7CD00D");
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

        modelBuilder.Entity<Stylist>(entity =>
        {
            entity.HasKey(e => e.StylistId).HasName("PK__Stylist__A822EA213AD4A1A7");

            entity.ToTable("Stylist");

            entity.Property(e => e.StylistId).ValueGeneratedNever();
            entity.Property(e => e.Availability).HasMaxLength(200);
            entity.Property(e => e.SkillLevel).HasMaxLength(50);

            entity.HasOne(d => d.StylistNavigation).WithOne(p => p.Stylist)
                .HasForeignKey<Stylist>(d => d.StylistId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Stylist__Stylist__4222D4EF");
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
