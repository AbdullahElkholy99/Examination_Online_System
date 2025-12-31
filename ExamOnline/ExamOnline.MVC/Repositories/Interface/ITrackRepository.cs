using ExamOnline.MVC.DTO;

namespace ExamOnline.MVC.Repositories.Interface;

public interface ITrackRepository : IBaseRepository<TrackDTO>
{
    Task<List<TracksForBranchDTO?>> GetTracksByBranchAsync(int id);

}
