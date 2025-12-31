using System;
using System.Collections.Generic;

namespace ExamOnline.MVC.Models;

public partial class ExamCreation
{
    public int InstructorId { get; set; }

    public int ExamId { get; set; }

    public int CourseId { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Course Course { get; set; } = null!;

    public virtual Exam Exam { get; set; } = null!;

    public virtual Instructor Instructor { get; set; } = null!;
}
