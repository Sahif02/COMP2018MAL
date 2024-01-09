using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace COMP2001MAL.Models;

public partial class Comp2001malMsahifassanibinmohamadContext : DbContext
{
    public Comp2001malMsahifassanibinmohamadContext()
    {
    }

    public Comp2001malMsahifassanibinmohamadContext(DbContextOptions<Comp2001malMsahifassanibinmohamadContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Activity> Activities { get; set; }

    public virtual DbSet<Bookmark> Bookmarks { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<Post> Posts { get; set; }

    public virtual DbSet<TrailService> TrailServices { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=dist-6-505.uopnet.plymouth.ac.uk;Database=COMP2001MAL_MSahifassanibinmohamad;User Id=MSahifassanibinmohamad;Password=UwtN179+;Encrypt=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Activity>(entity =>
        {
            entity.HasKey(e => e.ActivityId).HasName("PK__Activity__45F4A7F178A5C950");

            entity.ToTable("Activity");

            entity.Property(e => e.ActivityId)
                .ValueGeneratedNever()
                .HasColumnName("ActivityID");
            entity.Property(e => e.ActivityType)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TrailId).HasColumnName("TrailID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Trail).WithMany(p => p.Activities)
                .HasForeignKey(d => d.TrailId)
                .HasConstraintName("FK__Activity__TrailI__6754599E");

            entity.HasOne(d => d.User).WithMany(p => p.Activities)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Activity__UserID__76969D2E");
        });

        modelBuilder.Entity<Bookmark>(entity =>
        {
            entity.HasKey(e => e.BookmarkId).HasName("PK__Bookmark__541A3A91B96F3324");

            entity.ToTable("Bookmark");

            entity.Property(e => e.BookmarkId)
                .ValueGeneratedNever()
                .HasColumnName("BookmarkID");
            entity.Property(e => e.TrailId).HasColumnName("TrailID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Trail).WithMany(p => p.Bookmarks)
                .HasForeignKey(d => d.TrailId)
                .HasConstraintName("FK__Bookmark__TrailI__6B24EA82");

            entity.HasOne(d => d.User).WithMany(p => p.Bookmarks)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Bookmark__UserID__778AC167");
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.CommentId).HasName("PK__Comment__C3B4DFAA7E00C726");

            entity.ToTable("Comment");

            entity.Property(e => e.CommentId)
                .ValueGeneratedNever()
                .HasColumnName("CommentID");
            entity.Property(e => e.Comment1)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Comment");
            entity.Property(e => e.PostId).HasColumnName("PostID");
            entity.Property(e => e.Timestamp).HasColumnType("datetime");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Post).WithMany(p => p.Comments)
                .HasForeignKey(d => d.PostId)
                .HasConstraintName("FK__Comment__PostID__60A75C0F");

            entity.HasOne(d => d.User).WithMany(p => p.Comments)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Comment__UserID__75A278F5");
        });

        modelBuilder.Entity<Post>(entity =>
        {
            entity.HasKey(e => e.PostId).HasName("PK__Post__AA126038F04A567B");

            entity.ToTable("Post");

            entity.Property(e => e.PostId)
                .ValueGeneratedNever()
                .HasColumnName("PostID");
            entity.Property(e => e.Content)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Timestamp).HasColumnType("datetime");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.Posts)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Post__UserID__74AE54BC");
        });

        modelBuilder.Entity<TrailService>(entity =>
        {
            entity.HasKey(e => e.TrailId).HasName("PK__TrailSer__8F5236EEA0B73151");

            entity.ToTable("TrailService");

            entity.Property(e => e.TrailId)
                .ValueGeneratedNever()
                .HasColumnName("TrailID");
            entity.Property(e => e.TrailName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__tmp_ms_x__1788CCAC72B42880");

            entity.ToTable(tb => tb.HasTrigger("ArchiveUserProfileTrigger"));

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.ArchiveStatus)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FullName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Location)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ProfileImage)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
