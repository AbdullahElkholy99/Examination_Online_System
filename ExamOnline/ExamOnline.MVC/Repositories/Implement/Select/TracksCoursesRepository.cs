namespace ExamOnline.MVC.Repositories.Implement.Select;
using ExamOnline.MVC.Data;
using ExamOnline.MVC.DTO;
using ExamOnline.MVC.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

public class TracksCoursesRepository : ITracksCoursesRepository
{
    private readonly AppDbContext _context;

    public TracksCoursesRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<TracksCoursesDTO>> GetAllAsync()
    {
        return await _context.Database
        .SqlQuery<TracksCoursesDTO>($"EXEC sp_getAllTracksCourses")
        .ToListAsync();
    }

    public async Task<TracksCoursesDTO?> GetByIdAsync(int trackId, int courseId)
    {
        return await _context.Database
             .SqlQuery<TracksCoursesDTO>($"EXEC sp_getTracksCoursesById @Track_ID={trackId},@Course_ID={courseId}")
             .FirstOrDefaultAsync();
    }

}
