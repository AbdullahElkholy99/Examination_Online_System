using Microsoft.AspNetCore.Mvc.Rendering;

namespace ExamOnline.MVC.ViewModel;

public class CreateExamVM
{
    public int CourseId { get; set; }
    public int TotalMarks { get; set; }
    public string Type { get; set; } = "MCQ";
    public int DurationMinutes { get; set; }

    public List<SelectListItem> Courses { get; set; } = new();
}
//public class SelectListItems
//{
//    public string? Text { get; set; }     // النص اللي يظهر
//    public string? Value { get; set; }    // القيمة اللي تتبعت
//    public bool Selected { get; set; }    // هل مختار افتراضيًا؟
//}




public class ExamDetailsVM
{
    public int ExamId { get; set; }
    public string CourseName { get; set; } = null!;
    public string ExamType { get; set; } = null!;
    public int TotalMarks { get; set; }
    public bool IsClosed { get; set; }

    public List<QuestionDetailsVM> Questions { get; set; } = new();
}

public class QuestionDetailsVM
{
    public int QuestionId { get; set; }
    public string QuestionText { get; set; } = null!;
    public string QuestionType { get; set; } = null!;
    public int Marks { get; set; }
    public List<ChoiceDetailsVM> Choices { get; set; } = new();
}

public class ChoiceDetailsVM
{
    public string Text { get; set; } = null!;
    public bool IsCorrect { get; set; }
}
