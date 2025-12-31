namespace ExamOnline.MVC.DTO;

public record ExamAttemptDTO
{
    public int Student_ID { get; set; }
    public int Exam_ID { get; set; }
    public int Course_ID { get; set; }
    public decimal? Grades { get; set; }
    public DateTime AttemptAt { get; set; }
}
