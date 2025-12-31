using ExamOnline.MVC.Models;

namespace ExamOnline.MVC.ViewModel;

public class StudentCoursesVM
{
    public int? SelectedStudentId { get; set; }
    public string? SearchText { get; set; }

    public List<Student> Students { get; set; } = new();
    public Student? Student { get; set; }

    public List<CourseWithExamVM> Courses { get; set; } = new();
}

public class CourseWithExamVM
{
    public int CourseId { get; set; }
    public string CourseName { get; set; } = null!;
    public int Duration { get; set; }
    public int? ExamId { get; set; }   // null لو مفيش Exam
}


public class StudentResultsVM
{
    public Student Student { get; set; } = null!;

    public List<StudentCourseResultVM> Courses { get; set; } = new();
}

public class StudentCourseResultVM
{
    public int CourseId { get; set; }
    public string CourseName { get; set; } = null!;
    public int? ExamId { get; set; }

    public bool IsTaken { get; set; }
    public decimal? Grade { get; set; }

}


public class ExamReviewVM
{
    public string CourseName { get; set; } = null!;
    public List<QuestionReviewVM> Questions { get; set; } = new();
}

//public class QuestionReviewVM
//{
//    public string QuestionText { get; set; } = null!;
//    public string? StudentAnswer { get; set; }
//    public string CorrectAnswer { get; set; } = null!;
//    public bool IsCorrect { get; set; }
//}

public class StudentProgressVM
{
    public List<string> Courses { get; set; } = new();
    public List<decimal> Grades { get; set; } = new();
}


 