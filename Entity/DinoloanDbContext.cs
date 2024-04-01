﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace dotnet_web_mvc.Entity;

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
            entity.Property(e => e.Amount).HasColumnType("int(11)");
            entity.Property(e => e.DateCreated)
                .HasDefaultValueSql("'current_timestamp()'")
                .HasColumnType("date");
            entity.Property(e => e.Deduction).HasColumnType("int(11)");
            entity.Property(e => e.DueDate).HasColumnType("date");
            entity.Property(e => e.Interest).HasColumnType("int(11)");
            entity.Property(e => e.NoOfPayment).HasColumnType("int(11)");
            entity.Property(e => e.Penalty).HasColumnType("int(11)");
            entity.Property(e => e.Receivable).HasColumnType("int(11)");
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.Type).HasMaxLength(50);
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