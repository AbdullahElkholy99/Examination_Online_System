using System;
using System.Collections.Generic;

namespace ExamOnline.MVC.Models;

public partial class Instructor
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Email { get; set; }

    public int BranchId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual Branch Branch { get; set; } = null!;

    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();

    public virtual ICollection<ExamCreation> ExamCreations { get; set; } = new List<ExamCreation>();
}
