namespace ExamOnline.MVC.Repositories.Implement.Select;
using ExamOnline.MVC.Data;
using ExamOnline.MVC.DTO;
using ExamOnline.MVC.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

//----------------------------
public class ExamCreationRepository : IExamCreationRepository
{
    private readonly AppDbContext _context;

    public ExamCreationRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<ExamCreationDTO>> GetAllAsync()
    {
        return await _context.Database
            .SqlQuery<ExamCreationDTO>($"EXEC sp_getAllExamCreation")
            .ToListAsync();
    }

    public async Task<ExamCreationDTO?> GetByIdAsync(
        int instructorId, int examId, int courseId)
    {
        return await _context.Database
            .SqlQuery<ExamCreationDTO>(
                $"EXEC sp_getExamCreationById @Instructor_ID={instructorId}, @Exam_ID={examId}, @Course_ID={courseId}")
            .FirstOrDefaultAsync();
    }
}

