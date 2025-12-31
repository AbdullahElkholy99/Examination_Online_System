namespace ExamOnline.MVC.DTO;

public record IntakeDTO  
{
    public int ID { get; set; }
    public string Status { get; set; } = null!;
    public int Branch_ID { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}