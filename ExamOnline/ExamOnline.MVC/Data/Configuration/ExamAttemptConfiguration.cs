
using ExamOnline.MVC.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace ExamOnline.MVC.Data.Configuraation;

public class ExamAttemptConfiguration : IEntityTypeConfiguration<ExamAttempt>
{
    public void Configure(EntityTypeBuilder<ExamAttempt> builder)
    {
            builder.HasKey(e => new { e.StudentId, e.ExamId, e.CourseId });

            builder.ToTable("ExamAttempt");

            builder.Property(e => e.StudentId).HasColumnName("Student_ID");
            builder.Property(e => e.ExamId).HasColumnName("Exam_ID");
            builder.Property(e => e.CourseId).HasColumnName("Course_ID");
            builder.Property(e => e.AttemptAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysdatetime())");
            builder.Property(e => e.Grades).HasColumnType("decimal(5, 2)");

            builder.HasOne(d => d.Course).WithMany(p => p.ExamAttempts)
                .HasForeignKey(d => d.CourseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ExamAttempt_Course");

            builder.HasOne(d => d.Exam).WithMany(p => p.ExamAttempts)
                .HasForeignKey(d => d.ExamId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ExamAttempt_Exam");

            builder.HasOne(d => d.Student).WithMany(p => p.ExamAttempts)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ExamAttempt_Student");


        builder.ToTable("ExamAttempt", "dbo" ,tableBuilder =>
        {
            tableBuilder.HasTrigger("TR_ExamAttempt_AutoGrade");
        });
    }



}

public class StudentAnswersConfiguration : IEntityTypeConfiguration<StudentAnswer>
{
    public void Configure(EntityTypeBuilder<StudentAnswer> builder)
    {
        builder.HasKey(e => new { e.StudentId, e.ExamId, e.QuestionId });

        builder.Property(e => e.StudentId).HasColumnName("Student_ID");
        builder.Property(e => e.ExamId).HasColumnName("Exam_ID");
        builder.Property(e => e.QuestionId).HasColumnName("Question_ID");
        builder.Property(e => e.AnsweredAt)
            .HasPrecision(0)
            .HasDefaultValueSql("(sysdatetime())");
        builder.Property(e => e.Choice).HasMaxLength(255);

        builder.HasOne(d => d.Exam).WithMany(p => p.StudentAnswers)
            .HasForeignKey(d => d.ExamId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_StudentAnswers_Exam");

        builder.HasOne(d => d.Question).WithMany(p => p.StudentAnswers)
            .HasForeignKey(d => d.QuestionId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_StudentAnswers_Question");

        builder.HasOne(d => d.Student).WithMany(p => p.StudentAnswers)
            .HasForeignKey(d => d.StudentId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_StudentAnswers_Student");


        builder.ToTable("StudentAnswers", "dbo" ,tableBuilder =>
        {
            tableBuilder.HasTrigger("TR_StudentAnswers_AutoGrade");
        });
    }



}