using ExamOnline.MVC.Models;

namespace ExamOnline.MVC.ViewModel;
public class QuestionExamVM
{
    public int QuestionId { get; set; }
    public string QuestionText { get; set; } = null!;
    public List<ChoiceVM> Choices { get; set; } = new();
}
public class ChoiceVM
{
    public int ChoiceId { get; set; }
    public string ChoiceText { get; set; } = null!;
}
//public class ExamTakeVM
//{
//    public int StudentId { get; set; }
//    public int ExamId { get; set; }
//    public int CourseId { get; set; }

//    public int CurrentIndex { get; set; }
//    public int TotalQuestions { get; set; }

//    public QuestionExamVM CurrentQuestion { get; set; } = null!;

//    public int RemainingSeconds { get; set; }
//}

public class ExamTakeVM
{
    public int ExamId { get; set; }
    public int StudentId { get; set; }
    public int RemainingSeconds { get; set; }

    public List<QuestionExamVM> Questions { get; set; } = new();
}
public class QuestionCreateVM
{
    public int ExamId { get; set; }
    public string QuestionText { get; set; } = null!;
    public string QuestionType { get; set; } = null!; // MCQ | TF
    public int Marks { get; set; }

    public List<ChoiceCreateVM> Choices { get; set; } = new();
}

public class ChoiceCreateVM
{
    public string Text { get; set; } = null!;
    public bool IsCorrect { get; set; }
}
