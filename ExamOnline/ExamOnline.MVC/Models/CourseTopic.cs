using System;
using System.Collections.Generic;

namespace ExamOnline.MVC.Models;

public partial class CourseTopic
{
    public int CourseId { get; set; }

    public string Topic { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public virtual Course Course { get; set; } = null!;
}
