using ExamOnline.MVC.DTO;

namespace ExamOnline.MVC.Repositories.Interface;

public interface IStudentCoursesRepository
{
    Task<List<StudentCoursesDTO>> GetAllAsync();

    Task<StudentCoursesDTO?> GetByIdAsync(int studentId,int courseId);
    Task AssignCourseAsync(int studentId, int courseId);
    Task AssignTrackCoursesAsync(int studentId, int trackId);
    Task RemoveCourseAsync(int studentId, int courseId);
    Task<List<StudentCourseDTO>> GetStudentCoursesAsync(int studentId);
    Task AssignTrackCoursesToAllAsync(int trackId);

}
