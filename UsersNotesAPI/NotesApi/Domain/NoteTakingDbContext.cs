﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace NotesApi.Domain;

public partial class NoteTakingDbContext : DbContext
{
    public NoteTakingDbContext()
    {
    }

    public NoteTakingDbContext(DbContextOptions<NoteTakingDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Note> Notes { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=MICA\\SQLEXPRESS;Database=NoteTakingDB;Trusted_Connection=True;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Note>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_NoteId");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Text).HasMaxLength(200);

            entity.HasOne(d => d.User).WithMany(p => p.Notes)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Notes__UserId__4D94879B");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_UserId");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.LastName).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
