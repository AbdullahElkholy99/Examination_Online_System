namespace ExamOnline.MVC.Repositories.Implement.Select;
using ExamOnline.MVC.Data;
using ExamOnline.MVC.DTO;
using ExamOnline.MVC.Repositories.Interface;
using ExamOnline.MVC.ViewModel;
using Microsoft.EntityFrameworkCore;

public class CourseRepository : ICourseRepository
{
    private readonly AppDbContext _context;

    public CourseRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task AddAsync(CreateCourseVM dto)
     {
        await _context.Database.ExecuteSqlRawAsync(
            "EXEC sp_insertCourse @Name={0}, @Duration={1},@Instructor_ID={2} , @UpdatedAt={3} , @CreatedAt={4}",
            dto.Name, dto.Duration,dto.InstructorId,dto.UpdatedAt , dto.CreatedAt);
    }

    public async Task UpdateAsync(CourseDTO dto)
    {
        await _context.Database.ExecuteSqlRawAsync(
            "EXEC sp_UpdateCourse @ID={0}, @Name={1}, @Duration={2}, @Instructor_ID={3}",
            dto.Id, dto.Name, dto.Duration, dto.Instructor_Id);
    }

    public async Task<List<CourseDTO>> GetAllAsync()
    {
        return await _context.Database
         .SqlQuery<CourseDTO>($"EXEC sp_getAllCourse")
         .ToListAsync();
    }

    public async Task<CourseDTO?> GetByIdAsync(int id)
    {
        return await _context.Database
           .SqlQuery<CourseDTO>($"EXEC sp_getCourseByID @id={id} ")
           .FirstOrDefaultAsync();
      
    }
    public async Task DeleteAsync(int id)
    {
        await _context.Database
            .ExecuteSqlRawAsync("EXEC sp_DeleteCourse @ID={0}", id);
    }

    public Task AddAsync(CourseDTO t)
    {
        throw new NotImplementedException();
    }
}
