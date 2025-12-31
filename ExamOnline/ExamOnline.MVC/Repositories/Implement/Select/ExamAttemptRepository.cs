namespace ExamOnline.MVC.Repositories.Implement.Select;
using ExamOnline.MVC.Data;
using ExamOnline.MVC.DTO;
using ExamOnline.MVC.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

//----------------------------
public class ExamAttemptRepository : IExamAttemptRepository
{
    private readonly AppDbContext _context;

    public ExamAttemptRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<ExamAttemptDTO>> GetAllAsync()
    {
        return await _context.Database
            .SqlQuery<ExamAttemptDTO>($"EXEC sp_getAllExamAttempt")
            .ToListAsync();
    }

    public async Task<ExamAttemptDTO?> GetByIdAsync(
        int studentId, int examId, int courseId)
    {
        return await _context.Database
            .SqlQuery<ExamAttemptDTO>(
                $"EXEC sp_getExamAttemptById @Student_ID={studentId}, @Exam_ID={examId}, @Course_ID={courseId}")
            .FirstOrDefaultAsync();
    }
}

