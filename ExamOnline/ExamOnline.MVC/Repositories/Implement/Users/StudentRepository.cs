using ExamOnline.MVC.Data;
using ExamOnline.MVC.DTO.Users;
using ExamOnline.MVC.Repositories.Interface.Users;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace ExamOnline.MVC.Repositories.Implement.Users;
public class StudentRepository : IStudentRepository
{
    private readonly AppDbContext _context;

    public StudentRepository(AppDbContext context)
    {
        _context = context;
    }
   
    public async Task AddAsync(StudentCreationDTO dto)
    {
        await _context.Database.ExecuteSqlRawAsync(
            @"EXEC sp_InsertStudent 
                @name,
                @email,
                @phone,
                @address,
                @branchID,
                @trackId,
                @CreatedAt,
                @UpdatedAt",
            new SqlParameter("@name", dto.Name),
            new SqlParameter("@email", dto.Email ?? (object)DBNull.Value),
            new SqlParameter("@phone", dto.Phone ?? (object)DBNull.Value),
            new SqlParameter("@address", dto.Address ?? (object)DBNull.Value),
            new SqlParameter("@branchID", dto.Branch_ID),
            new SqlParameter("@trackId", dto.Track_ID),
            new SqlParameter("@CreatedAt", DateTime.Now),
            new SqlParameter("@UpdatedAt", DateTime.Now)
        );
    }


    public async Task UpdateAsync(StudentDTO dto)
    {
        await _context.Database.ExecuteSqlRawAsync(
            "EXEC sp_UpdateStudent @Id={0}, @Name={1}, @Email={2}, @BranchId={3}, @TrackId={4}",
            dto.ID, dto.Name, dto.Email, dto.Branch_ID, dto.Track_ID);
    }
    public async Task<List<StudentDTO>> GetAllAsync()
    {
        return await _context.Database
            .SqlQuery<StudentDTO>($"EXEC sp_getAllStudent")
            .ToListAsync();
    }

    public async Task<StudentDTO?> GetByIdAsync(int id)
    {
        return await _context.Database
            .SqlQuery<StudentDTO>($"EXEC sp_getStudentByID @id={id}")
            .FirstOrDefaultAsync();
    }
    public async Task DeleteAsync(int id)
    {
        await _context.Database
            .ExecuteSqlRawAsync("EXEC sp_DeleteStudent @ID={0}", id);
    }

    public Task AddAsync(StudentDTO t)
    {
        throw new NotImplementedException();
    }
}
