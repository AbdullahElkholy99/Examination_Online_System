using System;
using System.Collections.Generic;

namespace ExamOnline.MVC.Models;

public partial class Track
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int IntakeId { get; set; }

    public bool IsActive { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual Intake Intake { get; set; } = null!;

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();

    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();
}
