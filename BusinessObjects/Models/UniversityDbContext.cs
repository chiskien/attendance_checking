using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace BusinessObjects.Models
{
    public partial class UniversityDbContext : DbContext
    {
        public UniversityDbContext()
        {
        }

        public UniversityDbContext(DbContextOptions<UniversityDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Course?> Courses { get; set; } = null!;
        public virtual DbSet<CourseSchedule> CourseSchedules { get; set; } = null!;
        public virtual DbSet<Instructor> Instructors { get; set; } = null!;
        public virtual DbSet<RollCallBook> RollCallBooks { get; set; } = null!;
        public virtual DbSet<Student> Students { get; set; } = null!;
        public virtual DbSet<Subject> Subjects { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
                optionsBuilder.UseSqlServer(configuration.GetConnectionString("UniversityDb"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>(entity =>
            {
                entity.ToTable("COURSES");

                entity.Property(e => e.CourseCode).HasMaxLength(50);

                entity.Property(e => e.CourseDescription).HasMaxLength(300);

                entity.HasOne(d => d.Instructor)
                    .WithMany(p => p.Courses)
                    .HasForeignKey(d => d.InstructorId)
                    .HasConstraintName("FK_COURSES_INSTRUCTORS");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.Courses)
                    .HasForeignKey(d => d.SubjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_COURSES_SUBJECTS");

                entity.HasMany(d => d.Students)
                    .WithMany(p => p.Courses)
                    .UsingEntity<Dictionary<string, object>>(
                        "StudentCourse",
                        l => l.HasOne<Student>().WithMany().HasForeignKey("StudentId")
                            .OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("STUDENT_COURSE_STUDENTS_fk"),
                        r => r.HasOne<Course>().WithMany().HasForeignKey("CourseId")
                            .OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_STUDENT_COURSE_COURSES"),
                        j =>
                        {
                            j.HasKey("CourseId", "StudentId");

                            j.ToTable("STUDENT_COURSE");
                        });
            });

            modelBuilder.Entity<CourseSchedule>(entity =>
            {
                entity.HasKey(e => e.TeachingScheduleId)
                    .HasName("PK_TEACHING_SCHEDULES");

                entity.ToTable("COURSE_SCHEDULES");

                entity.Property(e => e.Description).HasMaxLength(300);

                entity.Property(e => e.TeachingDate).HasColumnType("datetime");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.CourseSchedules)
                    .HasForeignKey(d => d.CourseId)
                    .HasConstraintName("FK_COURSE_SCHEDULES_COURSES");
            });

            modelBuilder.Entity<Instructor>(entity =>
            {
                entity.ToTable("INSTRUCTORS");

                entity.Property(e => e.InstructorFirstName).HasMaxLength(50);

                entity.Property(e => e.InstructorLastName).HasMaxLength(50);

                entity.Property(e => e.InstructorMidName).HasMaxLength(50);
            });

            modelBuilder.Entity<RollCallBook>(entity =>
            {
                entity.ToTable("ROLL_CALL_BOOKS");

                entity.Property(e => e.Comment).HasMaxLength(300);

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.RollCallBooks)
                    .HasForeignKey(d => d.StudentId)
                    .HasConstraintName("FK_ROLL_CALL_BOOKS_STUDENTS");

                entity.HasOne(d => d.TeachingSchedule)
                    .WithMany(p => p.RollCallBooks)
                    .HasForeignKey(d => d.TeachingScheduleId)
                    .HasConstraintName("FK_ROLL_CALL_BOOKS_COURSE_SCHEDULES");
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.ToTable("STUDENTS");

                entity.Property(e => e.FirstName).HasMaxLength(70);

                entity.Property(e => e.Image)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.LastName).HasMaxLength(70);

                entity.Property(e => e.MiddleName).HasMaxLength(70);

                entity.Property(e => e.Roll).HasMaxLength(20);
            });

            modelBuilder.Entity<Subject>(entity =>
            {
                entity.ToTable("SUBJECTS");

                entity.Property(e => e.SubjectCode).HasMaxLength(50);

                entity.Property(e => e.SubjectName).HasMaxLength(100);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}