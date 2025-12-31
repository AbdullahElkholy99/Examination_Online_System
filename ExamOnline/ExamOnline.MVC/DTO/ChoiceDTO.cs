namespace ExamOnline.MVC.DTO;
public class ChoiceDTO
{
    public int Id { get; set; }
    public string Text { get; set; } = null!;
    public bool IsCorrect { get; set; }
    public int Question_ID { get; set; }
}
