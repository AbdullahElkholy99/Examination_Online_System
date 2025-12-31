namespace ExamOnline.MVC.Repositories.Implement.Select;
using ExamOnline.MVC.Data;
using ExamOnline.MVC.DTO;
using ExamOnline.MVC.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

public class StudentAnswersRepository : IStudentAnswersRepository
{
    private readonly AppDbContext _context;

    public StudentAnswersRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<StudentAnswersDTO>> GetAllAsync()
    {
        return await _context.Database
            .SqlQuery<StudentAnswersDTO>($"EXEC sp_getAllStudentAnswers")
            .ToListAsync();
    }

    public async Task<StudentAnswersDTO?> GetByIdAsync(
        int studentId, int examId, int questionId)
    {
        return await _context.Database
            .SqlQuery<StudentAnswersDTO>(
                $"EXEC sp_getStudentAnswersById @Student_ID={studentId}, @Exam_ID={examId}, @Question_ID={questionId}")
            .FirstOrDefaultAsync();
    }
}

