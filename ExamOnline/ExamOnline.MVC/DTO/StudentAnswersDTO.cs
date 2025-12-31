namespace ExamOnline.MVC.DTO;

public record StudentAnswersDTO
{
    public int Student_ID { get; set; }
    public int Exam_ID { get; set; }
    public int Question_ID { get; set; }
    public bool? IsCorrect { get; set; }
    public string? Choice { get; set; }
    public DateTime AnsweredAt { get; set; }
}
