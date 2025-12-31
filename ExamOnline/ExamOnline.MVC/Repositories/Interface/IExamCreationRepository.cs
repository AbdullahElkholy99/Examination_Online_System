using ExamOnline.MVC.DTO;

namespace ExamOnline.MVC.Repositories.Interface;

public interface IExamCreationRepository
{
    Task<List<ExamCreationDTO>> GetAllAsync();

    Task<ExamCreationDTO?> GetByIdAsync(
        int instructorId,
        int examId,
        int courseId
    );
}
