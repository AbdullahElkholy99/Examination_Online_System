namespace ExamOnline.MVC.DTO;

public record ExamCreationDTO
{
    public int Instructor_ID { get; set; }
    public int Exam_ID { get; set; }
    public int Course_ID { get; set; }
    public DateTime CreatedAt { get; set; }
}
