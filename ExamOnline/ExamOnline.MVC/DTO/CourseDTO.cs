namespace ExamOnline.MVC.DTO
{
    public record CourseDTO
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public int? Duration { get; set; }

        public int Instructor_Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }


    }
    public class AssignCourseDTO
    {
        public int StudentId { get; set; }
        public int CourseId { get; set; }
    }

    public class StudentCourseDTO
    {
        public int ID { get; set; }
        public string Name { get; set; } = null!;
        public int? Duration { get; set; }
    }

}
