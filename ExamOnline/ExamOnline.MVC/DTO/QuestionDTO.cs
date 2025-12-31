using ExamOnline.MVC.DTO.Users;

namespace ExamOnline.MVC.DTO;

public record QuestionDTO : BaseUserDTO
{
    public int ID { get; set; }
    public string Text { get; set; } = null!;
    public string Type { get; set; } = null!;
    public int Exam_ID { get; set; }
    public DateTime CreatedAt { get; set; }
    public int Marks { get; set; }


}