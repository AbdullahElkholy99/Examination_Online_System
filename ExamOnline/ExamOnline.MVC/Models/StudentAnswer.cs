using System;
using System.Collections.Generic;

namespace ExamOnline.MVC.Models;

public partial class StudentAnswer
{
    public int StudentId { get; set; }

    public int ExamId { get; set; }

    public int QuestionId { get; set; }

    public bool? IsCorrect { get; set; }

    public string? Choice { get; set; }
    public int ScoreAwarded { get; set; }   // ⭐ مهم
    public DateTime AnsweredAt { get; set; }

    public virtual Exam Exam { get; set; } = null!;

    public virtual Question Question { get; set; } = null!;

    public virtual Student Student { get; set; } = null!;
}
