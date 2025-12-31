namespace ExamOnline.MVC.Repositories.Implement.Select;
using ExamOnline.MVC.Data;
using ExamOnline.MVC.DTO;
using ExamOnline.MVC.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

public class IntakeRepository : IIntakeRepository
{
    private readonly AppDbContext _context;

    public IntakeRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<IntakeDTO>> GetAllAsync()
    {
        return await _context.Database
         .SqlQuery<IntakeDTO>($"EXEC sp_getAllIntake")
         .ToListAsync();  
    }

    public async Task<IntakeDTO?> GetByIdAsync(int id)
    {
          return await _context.Database
            .SqlQuery<IntakeDTO>($"EXEC sp_getIntakeByID @id={id}")
            .FirstOrDefaultAsync();
    }
    public async Task DeleteAsync(int id)
    {
        await _context.Database
            .ExecuteSqlRawAsync("EXEC sp_DeleteIntake @ID={0}", id);
    }

    public Task AddAsync(IntakeDTO t)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(IntakeDTO dto)
    {
        throw new NotImplementedException();
    }
}
