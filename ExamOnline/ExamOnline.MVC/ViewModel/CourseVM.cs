using Microsoft.AspNetCore.Mvc.Rendering;

namespace ExamOnline.MVC.ViewModel;

public class AssignCourseVM
{
    public int StudentId { get; set; }
    public int CourseId { get; set; }

    public List<SelectListItem> Students { get; set; } = new();
    public List<SelectListItem> Courses { get; set; } = new();
}

public class AssignByTrackVM
{
    public int StudentId { get; set; }
    public int TrackId { get; set; }

    public List<SelectListItem> Students { get; set; } = new();
    public List<SelectListItem> Tracks { get; set; } = new();
}
public class CreateCourseVM
{
    public string Name { get; set; } = null!;
    public int Duration { get; set; }

    public DateTime? CreatedAt { get; set; } = DateTime.Now;
    public DateTime? UpdatedAt { get; set; } = DateTime.Now;
    public int InstructorId { get; set; }

    public List<SelectListItem> Instructors { get; set; } = new();
}
