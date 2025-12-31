using System;
using System.Collections.Generic;

namespace ExamOnline.MVC.Models;

public partial class Exam
{
    public int Id { get; set; }

    public int Total_Marks { get; set; }

    public string? Type { get; set; }

    public int Course_Id { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual Course Course { get; set; } = null!;

    public virtual ICollection<ExamAttempt> ExamAttempts { get; set; } = new List<ExamAttempt>();

    public virtual ICollection<ExamCreation> ExamCreations { get; set; } = new List<ExamCreation>();

    public virtual ICollection<Question> Questions { get; set; } = new List<Question>();

    public virtual ICollection<StudentAnswer> StudentAnswers { get; set; } = new List<StudentAnswer>();
}
