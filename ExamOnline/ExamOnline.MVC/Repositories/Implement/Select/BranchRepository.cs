namespace ExamOnline.MVC.Repositories.Implement.Select;
using ExamOnline.MVC.Data;
using ExamOnline.MVC.DTO;
using ExamOnline.MVC.Repositories.Interface;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

public class BranchRepository : IBranchRepository
{
    private readonly AppDbContext _context;

    public BranchRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<BranchDTO>> GetAllAsync()
    {
        return await _context.Database
        .SqlQuery<BranchDTO>($"EXEC sp_getAllBranch")
        .ToListAsync();
    }

    public async Task<BranchDTO?> GetByIdAsync(int id)
    {
        return await _context.Database
             .SqlQuery<BranchDTO>($"EXEC sp_getBranchByID @id={id}")
             .FirstOrDefaultAsync();
    }
    public async Task DeleteAsync(int id)
    {
        await _context.Database
            .ExecuteSqlRawAsync("EXEC sp_DeleteBranch @ID={0}", id);
    }

    public Task AddAsync(BranchDTO t)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(BranchDTO dto)
    {
        throw new NotImplementedException();
    }
}
