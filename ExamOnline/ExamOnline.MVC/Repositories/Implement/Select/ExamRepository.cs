namespace ExamOnline.MVC.Repositories.Implement.Select;
using ExamOnline.MVC.Data;
using ExamOnline.MVC.DTO;
using ExamOnline.MVC.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

//----------------------------

public class ExamRepository : IExamRepository
{
    private readonly AppDbContext _context;

    public ExamRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<ExamDTO>> GetAllAsync()
    {
        return await _context.Database
            .SqlQuery<ExamDTO>($"EXEC sp_getAllExam")
            .ToListAsync();
    }

    public async Task<ExamDTO?> GetByIdAsync(int id)
    {
        return await _context.Database
            .SqlQuery<ExamDTO>($"EXEC sp_getExamById @id={id}")
            .FirstOrDefaultAsync();
    }
  
    public async Task AddAsync(ExamDTO dto)
    {
        await _context.Database.ExecuteSqlRawAsync(
            "EXEC sp_InsertExam @Total_Marks={0}, @Type={1}, @Course_ID={2}",
            dto.Total_Marks, dto.Type, dto.Course_ID);
    }

    public async Task DeleteAsync(int id)
    {
        await _context.Database.ExecuteSqlRawAsync(
            "EXEC sp_DeleteExam @ID={0}", id);
    }
}

