namespace ExamOnline.MVC.Repositories.Implement.Select;
using ExamOnline.MVC.Data;
using ExamOnline.MVC.DTO;
using ExamOnline.MVC.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

public class TrackRepository : ITrackRepository
{
    private readonly AppDbContext _context;

    public TrackRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<TrackDTO>> GetAllAsync()
    {
        return await _context.Database
            .SqlQuery<TrackDTO>($"EXEC sp_getAllTrack")
            .ToListAsync();
    }

    public async Task<TrackDTO?> GetByIdAsync(int id)
    {
        return await _context.Database
            .SqlQuery<TrackDTO>($"EXEC sp_getTrackById @id={id}")
            .FirstOrDefaultAsync();
    }
    public async Task<List<TracksForBranchDTO>> GetTracksByBranchAsync(int id)
    {
        return await _context.Database
            .SqlQuery<TracksForBranchDTO>(
                $"EXEC sp_getTracksByBranch @branchId={id}"
            )
            .ToListAsync();
    }

    public async Task DeleteAsync(int id)
    {
        await _context.Database
            .ExecuteSqlRawAsync("EXEC sp_DeleteTrack @ID={0}", id);
    }

    public async Task AddAsync(TrackDTO dto)
         => await _context.Database.ExecuteSqlRawAsync(
             "EXEC sp_InsertTrack @Name={0}, @Intake_ID={1}, @IsActive={2}",
             dto.Name, dto.Intake_ID, dto.IsActive);

    public async Task UpdateAsync(TrackDTO dto)
        => await _context.Database.ExecuteSqlRawAsync(
            "EXEC sp_UpdateTrack @ID={0}, @Name={1}, @Intake_ID={2}, @IsActive={3}",
            dto.ID, dto.Name, dto.Intake_ID, dto.IsActive);

}
