using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DinoLoan.Entity;

public partial class DinoloanDbContext : DbContext
{
    public DinoloanDbContext()
    {
    }

    public DinoloanDbContext(DbContextOptions<DinoloanDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<Clientinfo> Clientinfos { get; set; }

    public virtual DbSet<Loan> Loans { get; set; }

    public virtual DbSet<Usertype> Usertypes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySQL("Server=localhost;Database=dinoloan_db;User Id=root;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("books");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.Author).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Clientinfo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("clientinfo");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.Address).HasMaxLength(100);
            entity.Property(e => e.Age).HasColumnType("int(11)");
            entity.Property(e => e.Birthday).HasColumnType("date");
            entity.Property(e => e.CivilStatus).HasMaxLength(100);
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.MiddleName).HasMaxLength(100);
            entity.Property(e => e.NameOfFather).HasMaxLength(100);
            entity.Property(e => e.NameOfMother).HasMaxLength(100);
            entity.Property(e => e.Occupation).HasMaxLength(100);
            entity.Property(e => e.Religion).HasMaxLength(100);
            entity.Property(e => e.UserType).HasColumnType("int(11)");
            entity.Property(e => e.ZipCode).HasColumnType("int(11)");
        });

        modelBuilder.Entity<Loan>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("loan");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.Amount).HasPrecision(10);
            entity.Property(e => e.ClientId).HasColumnType("int(25)");
            entity.Property(e => e.Collectable).HasPrecision(10);
            entity.Property(e => e.Collected).HasPrecision(10);
            entity.Property(e => e.DateCreated).HasColumnType("date");
            entity.Property(e => e.Deduction).HasPrecision(10);
            entity.Property(e => e.DueDate)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("date");
            entity.Property(e => e.Interest).HasPrecision(10);
            entity.Property(e => e.InterestAmount).HasPrecision(10);
            entity.Property(e => e.NoOfPayment).HasColumnType("int(11)");
            entity.Property(e => e.Receivable).HasPrecision(10);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.TotalPayable).HasPrecision(10);
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .HasDefaultValueSql("'NULL'");
        });

        modelBuilder.Entity<Usertype>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("usertype");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
