using ExamOnline.MVC.DTO;
using ExamOnline.MVC.Models;

namespace ExamOnline.MVC.Repositories.Interface;

public interface ITracksCoursesRepository 
{
    Task<List<TracksCoursesDTO>> GetAllAsync();
    Task<TracksCoursesDTO?> GetByIdAsync(int trackId, int courseId);
}
