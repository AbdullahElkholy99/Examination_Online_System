namespace ExamOnline.MVC.Repositories.Implement.Select;
using ExamOnline.MVC.Data;
using ExamOnline.MVC.DTO;
using ExamOnline.MVC.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

//---------------------------- 

public class StudentCoursesRepository : IStudentCoursesRepository
{
    private readonly AppDbContext _context;

    public StudentCoursesRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<StudentCoursesDTO>> GetAllAsync()
    {
        return await _context.Database
            .SqlQuery<StudentCoursesDTO>($"EXEC sp_getAllStudentCourses")
            .ToListAsync();
    }

    public async Task<StudentCoursesDTO?> GetByIdAsync(int studentId, int courseId)
    {
        return await _context.Database
            .SqlQuery<StudentCoursesDTO>(
                $"EXEC sp_getStudentCoursesById @Student_ID={studentId}, @Course_ID={courseId}")
            .FirstOrDefaultAsync();
    }

    public async Task AssignCourseAsync(int studentId, int courseId)
    {
        await _context.Database.ExecuteSqlRawAsync(
            "EXEC sp_AssignStudentToCourse @Student_ID={0}, @Course_ID={1}",
            studentId, courseId);

    }
    public async Task AssignTrackCoursesAsync(int studentId, int trackId)
    {
        await _context.Database.ExecuteSqlRawAsync(
            "EXEC sp_AssignTrackCoursesToStudent @StudentID={0}, @TrackID={1}",
            studentId, trackId);

    }
    public async Task<List<StudentCourseDTO>> GetStudentCoursesAsync(int studentId)
    {
        return await _context.Database
            .SqlQuery<StudentCourseDTO>($"EXEC sp_GetStudentCourses @StudentID={studentId}")
            .ToListAsync();
    }
    public async Task RemoveCourseAsync(int studentId, int courseId)
    {
        await _context.Database.ExecuteSqlRawAsync(
            "EXEC sp_RemoveCourseFromStudent @StudentID={0}, @CourseID={1}",
            studentId, courseId);
    }
    public async Task AssignTrackCoursesToAllAsync(int trackId)
    {
        await _context.Database.ExecuteSqlRawAsync(
            "EXEC sp_AssignTrackCoursesToAllStudents @TrackID={0}",
            trackId);
    }

}

