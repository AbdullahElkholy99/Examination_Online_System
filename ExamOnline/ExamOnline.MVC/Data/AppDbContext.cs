using ExamOnline.MVC.Models;
using Microsoft.EntityFrameworkCore;

namespace ExamOnline.MVC.Data;

public partial class AppDbContext : DbContext
{
    #region Ctor
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    } 
    #endregion

    #region DbSets

    public virtual DbSet<Branch> Branches { get; set; }

    public virtual DbSet<Choice> Choices { get; set; }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<CourseTopic> CourseTopics { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Exam> Exams { get; set; }

    public virtual DbSet<ExamAttempt> ExamAttempts { get; set; }

    public virtual DbSet<ExamCreation> ExamCreations { get; set; }

    public virtual DbSet<Instructor> Instructors { get; set; }

    public virtual DbSet<Intake> Intakes { get; set; }

    public virtual DbSet<Question> Questions { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<StudentAnswer> StudentAnswers { get; set; }

    public virtual DbSet<StudentCourse> StudentCourses { get; set; }

    public virtual DbSet<Track> Tracks { get; set; }

    #endregion
   
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("server=.;database=ITI_Project;Trusted_Connection=True;Encrypt=False;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {


        
        modelBuilder.UseCollation("SQL_Latin1_General_CP1_CI_AS");

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);



        modelBuilder.Entity<Branch>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Branch__3214EC271E750389");

            entity.ToTable("Branch");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.BLoc)
                .HasMaxLength(150)
                .HasColumnName("B_Loc");
            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.UpdatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysdatetime())");
        });

        modelBuilder.Entity<Choice>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Choice__3214EC2724DFF716");

            entity.ToTable("Choice");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.QuestionId).HasColumnName("Question_ID");
            entity.Property(e => e.Text).HasMaxLength(255);

            entity.HasOne(d => d.Question).WithMany(p => p.Choices)
                .HasForeignKey(d => d.QuestionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Choice_Question");


        });

        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Course__3214EC2718B0B6B7");

            entity.ToTable("Course");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.InstructorId).HasColumnName("Instructor_ID");
            entity.Property(e => e.Name).HasMaxLength(120);
            entity.Property(e => e.UpdatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysdatetime())");

            entity.HasOne(d => d.Instructor).WithMany(p => p.Courses)
                .HasForeignKey(d => d.InstructorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Course_Instructor");
        });

        modelBuilder.Entity<CourseTopic>(entity =>
        {
            entity.HasKey(e => new { e.CourseId, e.Topic });

            entity.ToTable("Course_Topic");

            entity.Property(e => e.CourseId).HasColumnName("Course_ID");
            entity.Property(e => e.Topic).HasMaxLength(150);
            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysdatetime())");

            entity.HasOne(d => d.Course).WithMany(p => p.CourseTopics)
                .HasForeignKey(d => d.CourseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CourseTopic_Course");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Employee__3214EC2767563103");

            entity.ToTable("Employee");

            entity.HasIndex(e => e.Email, "UX_Employee_Email")
                .IsUnique()
                .HasFilter("([Email] IS NOT NULL)");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.BranchId).HasColumnName("Branch_ID");
            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.Email).HasMaxLength(150);
            entity.Property(e => e.ManagerId).HasColumnName("Manager_ID");
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.UpdatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysdatetime())");

            entity.HasOne(d => d.Branch).WithMany(p => p.Employees)
                .HasForeignKey(d => d.BranchId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Employee_Branch");

            entity.HasOne(d => d.Manager).WithMany(p => p.InverseManager)
                .HasForeignKey(d => d.ManagerId)
                .HasConstraintName("FK_Employee_Manager");
        });

        modelBuilder.Entity<Exam>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Exam__3214EC2774B70D57");

            entity.ToTable("Exam");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Course_Id).HasColumnName("Course_ID");
            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.Total_Marks).HasColumnName("Total_Marks");
            entity.Property(e => e.Type).HasMaxLength(30);
            entity.Property(e => e.UpdatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysdatetime())");

            entity.HasOne(d => d.Course).WithMany(p => p.Exams)
                .HasForeignKey(d => d.Course_Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Exam_Course");
        });

        //modelBuilder.Entity<ExamAttempt>(entity =>
        //{
        //    entity.HasKey(e => new { e.StudentId, e.ExamId, e.CourseId });

        //    entity.ToTable("ExamAttempt");

        //    entity.Property(e => e.StudentId).HasColumnName("Student_ID");
        //    entity.Property(e => e.ExamId).HasColumnName("Exam_ID");
        //    entity.Property(e => e.CourseId).HasColumnName("Course_ID");
        //    entity.Property(e => e.AttemptAt)
        //        .HasPrecision(0)
        //        .HasDefaultValueSql("(sysdatetime())");
        //    entity.Property(e => e.Grades).HasColumnType("decimal(5, 2)");

        //    entity.HasOne(d => d.Course).WithMany(p => p.ExamAttempts)
        //        .HasForeignKey(d => d.CourseId)
        //        .OnDelete(DeleteBehavior.ClientSetNull)
        //        .HasConstraintName("FK_ExamAttempt_Course");

        //    entity.HasOne(d => d.Exam).WithMany(p => p.ExamAttempts)
        //        .HasForeignKey(d => d.ExamId)
        //        .OnDelete(DeleteBehavior.ClientSetNull)
        //        .HasConstraintName("FK_ExamAttempt_Exam");

        //    entity.HasOne(d => d.Student).WithMany(p => p.ExamAttempts)
        //        .HasForeignKey(d => d.StudentId)
        //        .OnDelete(DeleteBehavior.ClientSetNull)
        //        .HasConstraintName("FK_ExamAttempt_Student");
        //});

        modelBuilder.Entity<ExamCreation>(entity =>
        {
            entity.HasKey(e => new { e.InstructorId, e.ExamId, e.CourseId });

            entity.ToTable("Exam_Creation");

            entity.Property(e => e.InstructorId).HasColumnName("Instructor_ID");
            entity.Property(e => e.ExamId).HasColumnName("Exam_ID");
            entity.Property(e => e.CourseId).HasColumnName("Course_ID");
            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysdatetime())");

            entity.HasOne(d => d.Course).WithMany(p => p.ExamCreations)
                .HasForeignKey(d => d.CourseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ExamCreation_Course");

            entity.HasOne(d => d.Exam).WithMany(p => p.ExamCreations)
                .HasForeignKey(d => d.ExamId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ExamCreation_Exam");

            entity.HasOne(d => d.Instructor).WithMany(p => p.ExamCreations)
                .HasForeignKey(d => d.InstructorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ExamCreation_Instructor");
        });

        modelBuilder.Entity<Instructor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Instruct__3214EC27C2305238");

            entity.ToTable("Instructor");

            entity.HasIndex(e => e.Email, "UX_Instructor_Email")
                .IsUnique()
                .HasFilter("([Email] IS NOT NULL)");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.BranchId).HasColumnName("Branch_ID");
            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.Email).HasMaxLength(150);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.UpdatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysdatetime())");

            entity.HasOne(d => d.Branch).WithMany(p => p.Instructors)
                .HasForeignKey(d => d.BranchId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Instructor_Branch");
        });

        modelBuilder.Entity<Intake>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Intake__3214EC27150FEE1D");

            entity.ToTable("Intake");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.BranchId).HasColumnName("Branch_ID");
            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValue("Planned");
            entity.Property(e => e.UpdatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysdatetime())");

            entity.HasOne(d => d.Branch).WithMany(p => p.Intakes)
                .HasForeignKey(d => d.BranchId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Intake_Branch");
        });

        modelBuilder.Entity<Question>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Question__3214EC27477B05CA");

            entity.ToTable("Question");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.ExamId).HasColumnName("Exam_ID");
            entity.Property(e => e.Type).HasMaxLength(30);

            entity.HasOne(d => d.Exam).WithMany(p => p.Questions)
                .HasForeignKey(d => d.ExamId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Question_Exam");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Student__3214EC2783E1A3E6");

            entity.ToTable("Student");

            entity.HasIndex(e => e.Email, "UX_Student_Email")
                .IsUnique()
                .HasFilter("([Email] IS NOT NULL)");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Address).HasMaxLength(200);
            entity.Property(e => e.BranchId).HasColumnName("Branch_ID");
            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.Email).HasMaxLength(150);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Phone).HasMaxLength(30);
            entity.Property(e => e.TrackId).HasColumnName("Track_ID");
            entity.Property(e => e.UpdatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysdatetime())");

            entity.HasOne(d => d.Branch).WithMany(p => p.Students)
                .HasForeignKey(d => d.BranchId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Student_Branch");

            entity.HasOne(d => d.Track).WithMany(p => p.Students)
                .HasForeignKey(d => d.TrackId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Student_Track");
        });

        //modelBuilder.Entity<StudentAnswer>(entity =>
        //{
        //    entity.HasKey(e => new { e.StudentId, e.ExamId, e.QuestionId });

        //    entity.Property(e => e.StudentId).HasColumnName("Student_ID");
        //    entity.Property(e => e.ExamId).HasColumnName("Exam_ID");
        //    entity.Property(e => e.QuestionId).HasColumnName("Question_ID");
        //    entity.Property(e => e.AnsweredAt)
        //        .HasPrecision(0)
        //        .HasDefaultValueSql("(sysdatetime())");
        //    entity.Property(e => e.Choice).HasMaxLength(255);

        //    entity.HasOne(d => d.Exam).WithMany(p => p.StudentAnswers)
        //        .HasForeignKey(d => d.ExamId)
        //        .OnDelete(DeleteBehavior.ClientSetNull)
        //        .HasConstraintName("FK_StudentAnswers_Exam");

        //    entity.HasOne(d => d.Question).WithMany(p => p.StudentAnswers)
        //        .HasForeignKey(d => d.QuestionId)
        //        .OnDelete(DeleteBehavior.ClientSetNull)
        //        .HasConstraintName("FK_StudentAnswers_Question");

        //    entity.HasOne(d => d.Student).WithMany(p => p.StudentAnswers)
        //        .HasForeignKey(d => d.StudentId)
        //        .OnDelete(DeleteBehavior.ClientSetNull)
        //        .HasConstraintName("FK_StudentAnswers_Student");
        //});

        modelBuilder.Entity<StudentCourse>(entity =>
        {
            entity.HasKey(e => new { e.StudentId, e.CourseId });

            entity.ToTable("Student_Courses");

            entity.Property(e => e.StudentId).HasColumnName("Student_ID");
            entity.Property(e => e.CourseId).HasColumnName("Course_ID");
            entity.Property(e => e.EnrolledAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysdatetime())");

            entity.HasOne(d => d.Course).WithMany(p => p.StudentCourses)
                .HasForeignKey(d => d.CourseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StudentCourses_Course");

            entity.HasOne(d => d.Student).WithMany(p => p.StudentCourses)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StudentCourses_Student");
        });

        modelBuilder.Entity<Track>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Track__3214EC277F23A05A");

            entity.ToTable("Track");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.IntakeId).HasColumnName("Intake_ID");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.UpdatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysdatetime())");

            entity.HasOne(d => d.Intake).WithMany(p => p.Tracks)
                .HasForeignKey(d => d.IntakeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Track_Intake");

            entity.HasMany(d => d.Courses).WithMany(p => p.Tracks)
                .UsingEntity<Dictionary<string, object>>(
                    "TracksCourse",
                    r => r.HasOne<Course>().WithMany()
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_TracksCourses_Course"),
                    l => l.HasOne<Track>().WithMany()
                        .HasForeignKey("TrackId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_TracksCourses_Track"),
                    j =>
                    {
                        j.HasKey("TrackId", "CourseId");
                        j.ToTable("Tracks_Courses");
                        j.IndexerProperty<int>("TrackId").HasColumnName("Track_ID");
                        j.IndexerProperty<int>("CourseId").HasColumnName("Course_ID");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
