using ExamOnline.MVC.Data;
using ExamOnline.MVC.DTO;
using ExamOnline.MVC.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace ExamOnline.MVC.Repositories.Implement.Select;

public class ChoiceRepository : IChoiceRepository
{
    private readonly AppDbContext _context;

    public ChoiceRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task DeleteAsync(int id)
    {
        await _context.Database
            .ExecuteSqlRawAsync("EXEC sp_DeleteChoice @ID={0}", id);
    }
    public async Task AddAsync(ChoiceDTO c)
    {
        await _context.Database.ExecuteSqlRawAsync(
            "EXEC sp_InsertChoice @Text={0}, @IsCorrect={1}, @Question_ID={2}",
            c.Text, c.IsCorrect, c.Question_ID);

    }
}
