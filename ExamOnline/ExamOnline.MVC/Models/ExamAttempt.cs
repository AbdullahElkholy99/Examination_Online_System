using System;
using System.Collections.Generic;

namespace ExamOnline.MVC.Models;

public partial class ExamAttempt
{
    public int StudentId { get; set; }

    public int ExamId { get; set; }

    public int CourseId { get; set; }

    public decimal? Grades { get; set; }

    public DateTime AttemptAt { get; set; }

    public virtual Course Course { get; set; } = null!;

    public virtual Exam Exam { get; set; } = null!;

    public virtual Student Student { get; set; } = null!;
}
