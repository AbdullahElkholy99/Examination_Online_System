using System;
using System.Collections.Generic;

namespace ExamOnline.MVC.Models;

public partial class Branch
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? BLoc { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    public virtual ICollection<Instructor> Instructors { get; set; } = new List<Instructor>();

    public virtual ICollection<Intake> Intakes { get; set; } = new List<Intake>();

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
