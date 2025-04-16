using System;
using System.Collections.Generic;
using final_api_project.Models;
using Microsoft.EntityFrameworkCore;

namespace final_api_project.DBcontext;

public partial class UniversityContext : DbContext
{
    public UniversityContext()
    {
    }

    public UniversityContext(DbContextOptions<UniversityContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Student> Students { get; set; }
    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserPermission> UserPermissions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Studend__3213E83FBB94B457");

            entity.ToTable("Student");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Dateofbrith).HasMaxLength(50);
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Phone).HasMaxLength(50);
        });
        modelBuilder.Entity<User>(entity =>
                {
                    entity.HasKey(e => e.Id).HasName("PK__Users__3213E83F564A0BFC");

                    entity.HasIndex(e => e.Name, "UQ__Users__72E12F1B5F2B10A9").IsUnique();

                    entity.Property(e => e.Id).HasColumnName("id");
                    entity.Property(e => e.Name)
                        .HasMaxLength(50)
                        .HasColumnName("name");
                    entity.Property(e => e.Password).HasMaxLength(50);
                });

        modelBuilder.Entity<UserPermission>(entity =>
        {
            entity.HasKey(e => new { e.Userid, e.Permissionid }).HasName("PK__UserPerm__796CAC23B88E4602");

            entity.HasOne(d => d.User).WithMany(p => p.UserPermissions)
                .HasForeignKey(d => d.Userid)
                .HasConstraintName("FK__UserPermi__Useri__4CA06362");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
