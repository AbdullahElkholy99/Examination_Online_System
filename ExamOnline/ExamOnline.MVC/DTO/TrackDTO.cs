namespace ExamOnline.MVC.DTO;

public record TrackDTO  
{
    public int ID { get; set; }
    public string Name { get; set; }
    public int Intake_ID { get; set; }
    public bool IsActive{ get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

}

public record TracksForBranchDTO  
{
    public int ID { get; set; }
    public string Name { get; set; }
}
