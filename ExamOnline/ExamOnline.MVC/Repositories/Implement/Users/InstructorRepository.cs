namespace ExamOnline.MVC.Repositories.Implement.Users;
using ExamOnline.MVC.Data;
using ExamOnline.MVC.DTO.Users;
using ExamOnline.MVC.Repositories.Interface.Users;
using Microsoft.EntityFrameworkCore;

public class InstructorRepository : IInstructorRepository
{
    private readonly AppDbContext _context;

    public InstructorRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(InstructorDTO dto)
    {
        await _context.Database.ExecuteSqlRawAsync(
            "EXEC sp_InsertInstructor @Name={0}, @Email={1}, @Branch_ID={2}",
            dto.Name, dto.Email, dto.Branch_ID);
    }

    public async Task UpdateAsync(InstructorDTO dto)
    {
        await _context.Database.ExecuteSqlRawAsync(
            "EXEC sp_UpdateInstructor @ID={0}, @Name={1}, @Email={2}, @Branch_ID={3}",
            dto.ID, dto.Name, dto.Email, dto.Branch_ID);
    }
    public async Task<List<InstructorDTO>> GetAllAsync()
    {
        return await _context.Database
         .SqlQuery<InstructorDTO>($"EXEC sp_getAllInsructor")
         .ToListAsync();  
    }

    public async Task<InstructorDTO?> GetByIdAsync(int id)
    {
          return await _context.Database
            .SqlQuery<InstructorDTO>($"EXEC sp_getInsructorByID @id={id}")
            .FirstOrDefaultAsync();
    }
    public async Task DeleteAsync(int id)
    {
        await _context.Database
            .ExecuteSqlRawAsync("EXEC sp_DeleteInstructor @ID={0}", id);
    }

   
}
