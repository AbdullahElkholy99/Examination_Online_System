using System;
using System.Collections.Generic;

namespace ExamOnline.MVC.Models;

public partial class Course
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int? Duration { get; set; }

    public int InstructorId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<CourseTopic> CourseTopics { get; set; } = new List<CourseTopic>();

    public virtual ICollection<ExamAttempt> ExamAttempts { get; set; } = new List<ExamAttempt>();

    public virtual ICollection<ExamCreation> ExamCreations { get; set; } = new List<ExamCreation>();

    public virtual ICollection<Exam> Exams { get; set; } = new List<Exam>();

    public virtual Instructor Instructor { get; set; } = null!;

    public virtual ICollection<StudentCourse> StudentCourses { get; set; } = new List<StudentCourse>();

    public virtual ICollection<Track> Tracks { get; set; } = new List<Track>();
}
