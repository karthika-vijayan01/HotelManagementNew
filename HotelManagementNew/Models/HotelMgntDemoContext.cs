using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace HotelManagementNew.Models;

public partial class HotelMgntDemoContext : DbContext
{
    public HotelMgntDemoContext()
    {
    }

    public HotelMgntDemoContext(DbContextOptions<HotelMgntDemoContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Booking> Bookings { get; set; }

    public virtual DbSet<Guest> Guests { get; set; }

    public virtual DbSet<Room> Rooms { get; set; }

    public virtual DbSet<Service> Services { get; set; }

    public virtual DbSet<ServiceRequest> ServiceRequests { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Booking>(entity =>
        {
            entity.HasKey(e => e.BookingId).HasName("PK__Bookings__73951AED7E9DAD5E");

            entity.Property(e => e.BookingDate).HasColumnType("date");
            entity.Property(e => e.CheckInDate).HasColumnType("date");
            entity.Property(e => e.CheckOutDate).HasColumnType("date");
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Guest).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.GuestId)
                .HasConstraintName("FK__Bookings__GuestI__3D5E1FD2");

            entity.HasOne(d => d.Room).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.RoomId)
                .HasConstraintName("FK__Bookings__RoomId__3E52440B");
        });

        modelBuilder.Entity<Guest>(entity =>
        {
            entity.HasKey(e => e.GuestId).HasName("PK__Guests__0C423C12FA4FCBCD");

            entity.HasIndex(e => e.ContactNumber, "UQ__Guests__570665C61206E6BF").IsUnique();

            entity.HasIndex(e => e.AadhaarNumber, "UQ__Guests__72CF7959D609B58B").IsUnique();

            entity.Property(e => e.AadhaarNumber)
                .HasMaxLength(12)
                .IsUnicode(false);
            entity.Property(e => e.ContactNumber)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.GuestName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Room>(entity =>
        {
            entity.HasKey(e => e.RoomId).HasName("PK__Rooms__3286393968F13144");

            entity.HasIndex(e => e.RoomNo, "UQ__Rooms__328651ABE63D3EB0").IsUnique();

            entity.Property(e => e.PricePerNight).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.RoomNo)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.RoomType)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.HasKey(e => e.ServiceId).HasName("PK__Services__C51BB00A5A9601CA");

            entity.HasIndex(e => e.ServiceName, "UQ__Services__A42B5F990EE92A19").IsUnique();

            entity.Property(e => e.ServiceName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ServicePrice).HasColumnType("decimal(10, 2)");
        });

        modelBuilder.Entity<ServiceRequest>(entity =>
        {
            entity.HasKey(e => e.RequestId).HasName("PK__ServiceR__33A8517AEF3E20D4");

            entity.Property(e => e.RequestDate).HasColumnType("date");

            entity.HasOne(d => d.Booking).WithMany(p => p.ServiceRequests)
                .HasForeignKey(d => d.BookingId)
                .HasConstraintName("FK__ServiceRe__Booki__440B1D61");

            entity.HasOne(d => d.Service).WithMany(p => p.ServiceRequests)
                .HasForeignKey(d => d.ServiceId)
                .HasConstraintName("FK__ServiceRe__Servi__44FF419A");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
