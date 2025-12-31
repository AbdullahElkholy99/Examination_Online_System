using System;
using System.Collections.Generic;

namespace ExamOnline.MVC.Models;

public partial class Choice
{
    public int Id { get; set; }

    public string? Text { get; set; } = null!;

    public bool IsCorrect { get; set; }

    public int QuestionId { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Question Question { get; set; } = null!;
}
