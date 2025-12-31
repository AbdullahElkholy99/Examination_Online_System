namespace ExamOnline.MVC.DTO;

public record StudentCoursesDTO
{
    public int Student_ID { get; set; }
    public int Course_ID { get; set; }
    public DateTime EnrolledAt { get; set; }
}
