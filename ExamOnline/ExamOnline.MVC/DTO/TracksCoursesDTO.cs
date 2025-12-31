namespace ExamOnline.MVC.DTO;

public record TracksCoursesDTO  
{
    public int? Track_ID { get; set; } = null!;
    public int? Course_ID { get; set; } = null!;
}
