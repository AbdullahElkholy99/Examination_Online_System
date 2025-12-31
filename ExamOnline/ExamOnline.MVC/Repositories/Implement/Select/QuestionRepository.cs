namespace ExamOnline.MVC.Repositories.Implement.Select;
using ExamOnline.MVC.Data;
using ExamOnline.MVC.DTO;
using ExamOnline.MVC.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

public class QuestionRepository : IQuestionRepository
{
    private readonly AppDbContext _context;

    public QuestionRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<QuestionDTO>> GetAllAsync()
    {
        return await _context.Database
            .SqlQuery<QuestionDTO>($"EXEC sp_getAllQuestion")
            .ToListAsync();
    }

    public async Task<QuestionDTO?> GetByIdAsync(int id)
    {
        return await _context.Database
            .SqlQuery<QuestionDTO>($"EXEC sp_getQuestionByID @id={id}")
            .FirstOrDefaultAsync();
    }
    public async Task DeleteAsync(int id)
    {
        await _context.Database
            .ExecuteSqlRawAsync("EXEC sp_DeleteQuestion @ID={0}", id);
    }

    public async Task AddAsync(QuestionDTO q)
    {
        await _context.Database.ExecuteSqlRawAsync(
            "EXEC sp_InsertQuestion @Text={0}, @Type={1}, @Exam_ID={2}, @Marks={3}",
            q.Text, q.Type, q.Exam_ID, q.Marks);
    }
    public Task UpdateAsync(QuestionDTO dto)
    {
        throw new NotImplementedException();
    }

    public async Task<List<QuestionDTO>> GetByExamAsync(int examId)
          => await _context.Database
              .SqlQuery<QuestionDTO>($"EXEC sp_getQuestionByExamID @Exam_ID={examId}")
              .ToListAsync();
}
