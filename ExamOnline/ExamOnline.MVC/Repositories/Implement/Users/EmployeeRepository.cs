namespace ExamOnline.MVC.Repositories.Implement.Users;
using ExamOnline.MVC.Data;
using ExamOnline.MVC.DTO.Users;
using ExamOnline.MVC.Repositories.Interface.Users;
using Microsoft.EntityFrameworkCore;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly AppDbContext _context;

    public EmployeeRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<EmployeeDTO>> GetAllAsync()
    {
        return await _context.Database
         .SqlQuery<EmployeeDTO>($"EXEC sp_getAllEmployee")
         .ToListAsync();  
    }

    public async Task<EmployeeDTO?> GetByIdAsync(int id)
    {
          return await _context.Database
            .SqlQuery<EmployeeDTO>($"EXEC sp_getEmployeeByID @id={id}")
            .FirstOrDefaultAsync();
    }
    public async Task DeleteAsync(int id)
    {
        await _context.Database
            .ExecuteSqlRawAsync("EXEC sp_DeleteEmployee @ID={0}", id);
    }

    public Task AddAsync(EmployeeDTO t)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(EmployeeDTO dto)
    {
        throw new NotImplementedException();
    }
}
