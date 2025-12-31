using System;
using System.Collections.Generic;

namespace ExamOnline.MVC.Models;

public partial class Question
{
    public int Id { get; set; }

    public string Text { get; set; } = null!;

    public string? Type { get; set; }

    public int ExamId { get; set; }
    public int Marks { get; set; }   

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<Choice> Choices { get; set; } = new List<Choice>();

    public virtual Exam Exam { get; set; } = null!;

    public virtual ICollection<StudentAnswer> StudentAnswers { get; set; } = new List<StudentAnswer>();
}
