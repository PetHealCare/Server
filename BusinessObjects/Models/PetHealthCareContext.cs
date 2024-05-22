using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BusinessObjects.Models
{
    public partial class PetHealthCareContext : DbContext
    {
        public PetHealthCareContext()
        {
        }

        public PetHealthCareContext(DbContextOptions<PetHealthCareContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Bill> Bills { get; set; } = null!;
        public virtual DbSet<Booking> Bookings { get; set; } = null!;
        public virtual DbSet<Customer> Customers { get; set; } = null!;
        public virtual DbSet<Doctor> Doctors { get; set; } = null!;
        public virtual DbSet<MedicalRecord> MedicalRecords { get; set; } = null!;
        public virtual DbSet<Payment> Payments { get; set; } = null!;
        public virtual DbSet<Pet> Pets { get; set; } = null!;
        public virtual DbSet<Schedule> Schedules { get; set; } = null!;
        public virtual DbSet<Service> Services { get; set; } = null!;
        public virtual DbSet<SlotBooking> SlotBookings { get; set; } = null!;
        public virtual DbSet<staff> staff { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=(local);uid=sa;pwd=12345;database=PetHealthCare;TrustServerCertificate=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bill>(entity =>
            {
                entity.ToTable("Bill");

                entity.HasIndex(e => e.BookingId, "UQ__Bill__C6D03BEC23B57B47")
                    .IsUnique();

                entity.Property(e => e.BillId).HasColumnName("billID");

                entity.Property(e => e.BookingId).HasColumnName("bookingID");

                entity.Property(e => e.InsDate)
                    .HasColumnType("date")
                    .HasColumnName("insDate");

                entity.Property(e => e.PaymentId).HasColumnName("paymentID");

                entity.Property(e => e.TotalAmount).HasColumnName("totalAmount");

                entity.HasOne(d => d.Booking)
                    .WithOne(p => p.Bill)
                    .HasForeignKey<Bill>(d => d.BookingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Bill__bookingID__44FF419A");

                entity.HasOne(d => d.Payment)
                    .WithMany(p => p.Bills)
                    .HasForeignKey(d => d.PaymentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Bill__paymentID__45F365D3");
            });

            modelBuilder.Entity<Booking>(entity =>
            {
                entity.ToTable("Booking");

                entity.Property(e => e.BookingId).HasColumnName("bookingID");

                entity.Property(e => e.BookingDate)
                    .HasColumnType("date")
                    .HasColumnName("bookingDate");

                entity.Property(e => e.CustomerId).HasColumnName("customerID");

                entity.Property(e => e.Note)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("note");

                entity.Property(e => e.PetId).HasColumnName("petID");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Booking__custome__3A81B327");

                entity.HasOne(d => d.Pet)
                    .WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.PetId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Booking__petID__398D8EEE");

                entity.HasMany(d => d.SlotBookings)
                    .WithMany(p => p.Bookings)
                    .UsingEntity<Dictionary<string, object>>(
                        "BookingSlot",
                        l => l.HasOne<SlotBooking>().WithMany().HasForeignKey("SlotBookingId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__BookingSl__slotB__3E52440B"),
                        r => r.HasOne<Booking>().WithMany().HasForeignKey("BookingId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__BookingSl__booki__3D5E1FD2"),
                        j =>
                        {
                            j.HasKey("BookingId", "SlotBookingId").HasName("PK__BookingS__6F8E480AB77B2EA0");

                            j.ToTable("BookingSlot");

                            j.IndexerProperty<int>("BookingId").HasColumnName("bookingID");

                            j.IndexerProperty<int>("SlotBookingId").HasColumnName("slotBookingID");
                        });
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customer");

                entity.Property(e => e.CustomerId).HasColumnName("customerID");

                entity.Property(e => e.Address)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("address");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.FullName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("fullName");

                entity.Property(e => e.Password)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("password");

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("phoneNumber");

                entity.Property(e => e.Status).HasColumnName("status");
            });

            modelBuilder.Entity<Doctor>(entity =>
            {
                entity.ToTable("Doctor");

                entity.Property(e => e.DoctorId).HasColumnName("doctorID");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.FullName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("fullName");

                entity.Property(e => e.Password)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("password");

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("phoneNumber");

                entity.Property(e => e.Speciality)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("speciality");

                entity.Property(e => e.Status).HasColumnName("status");
            });

            modelBuilder.Entity<MedicalRecord>(entity =>
            {
                entity.HasKey(e => e.RecordId)
                    .HasName("PK__MedicalR__D825197E43E81FE7");

                entity.ToTable("MedicalRecord");

                entity.Property(e => e.RecordId).HasColumnName("recordID");

                entity.Property(e => e.Diagnosis)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("diagnosis");

                entity.Property(e => e.DoctorId).HasColumnName("doctorID");

                entity.Property(e => e.Notes)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("notes");

                entity.Property(e => e.PetId).HasColumnName("petID");

                entity.Property(e => e.Treatment)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("treatment");

                entity.Property(e => e.VisitDate)
                    .HasColumnType("date")
                    .HasColumnName("visitDate");

                entity.HasOne(d => d.Doctor)
                    .WithMany(p => p.MedicalRecords)
                    .HasForeignKey(d => d.DoctorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MedicalRe__docto__36B12243");

                entity.HasOne(d => d.Pet)
                    .WithMany(p => p.MedicalRecords)
                    .HasForeignKey(d => d.PetId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MedicalRe__petID__35BCFE0A");
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.ToTable("Payment");

                entity.Property(e => e.PaymentId).HasColumnName("paymentID");

                entity.Property(e => e.Amount).HasColumnName("amount");

                entity.Property(e => e.InsDate)
                    .HasColumnType("date")
                    .HasColumnName("insDate");

                entity.Property(e => e.Method)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("method");
            });

            modelBuilder.Entity<Pet>(entity =>
            {
                entity.ToTable("Pet");

                entity.Property(e => e.PetId).HasColumnName("petID");

                entity.Property(e => e.CustomerId).HasColumnName("customerID");

                entity.Property(e => e.Name)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.Species)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("species");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Pets)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Pet__customerID__286302EC");
            });

            modelBuilder.Entity<Schedule>(entity =>
            {
                entity.ToTable("Schedule");

                entity.Property(e => e.ScheduleId).HasColumnName("scheduleID");

                entity.Property(e => e.EndTime)
                    .HasColumnType("datetime")
                    .HasColumnName("endTime");

                entity.Property(e => e.StartTime)
                    .HasColumnType("datetime")
                    .HasColumnName("startTime");

                entity.Property(e => e.Status).HasColumnName("status");
            });

            modelBuilder.Entity<Service>(entity =>
            {
                entity.ToTable("Service");

                entity.Property(e => e.ServiceId).HasColumnName("serviceID");

                entity.Property(e => e.Description)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.ServiceName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("serviceName");
            });

            modelBuilder.Entity<SlotBooking>(entity =>
            {
                entity.ToTable("SlotBooking");

                entity.Property(e => e.SlotBookingId).HasColumnName("slotBookingID");

                entity.Property(e => e.DoctorId).HasColumnName("doctorID");

                entity.Property(e => e.ScheduleId).HasColumnName("scheduleID");

                entity.Property(e => e.ServiceId).HasColumnName("serviceID");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.HasOne(d => d.Doctor)
                    .WithMany(p => p.SlotBookings)
                    .HasForeignKey(d => d.DoctorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__SlotBooki__docto__30F848ED");

                entity.HasOne(d => d.Schedule)
                    .WithMany(p => p.SlotBookings)
                    .HasForeignKey(d => d.ScheduleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__SlotBooki__sched__32E0915F");

                entity.HasOne(d => d.Service)
                    .WithMany(p => p.SlotBookings)
                    .HasForeignKey(d => d.ServiceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__SlotBooki__servi__31EC6D26");
            });

            modelBuilder.Entity<staff>(entity =>
            {
                entity.ToTable("Staff");

                entity.Property(e => e.StaffId).HasColumnName("staffID");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.FullName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("fullName");

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("phoneNumber");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
