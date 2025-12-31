using System;
using System.Collections.Generic;

namespace ExamOnline.MVC.Models;

public partial class StudentCourse
{
    public int StudentId { get; set; }

    public int CourseId { get; set; }

    public DateTime EnrolledAt { get; set; }

    public virtual Course Course { get; set; } = null!;

    public virtual Student Student { get; set; } = null!;
}
