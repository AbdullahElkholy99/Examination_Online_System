using ExamOnline.MVC.Models;

namespace ExamOnline.MVC.DTO;

public record ExamDTO
{
    public int ID { get; set; }
    public int Total_Marks { get; set; }
    public string? Type { get; set; }
    public int Course_ID { get; set; }
    public string? CourseName { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
public class ExamCreateVM
{
    public string Type { get; set; } = null!;
    public int Total_Marks { get; set; }
    public int Course_Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
    public List<Course> Courses { get; set; } = new();
}
