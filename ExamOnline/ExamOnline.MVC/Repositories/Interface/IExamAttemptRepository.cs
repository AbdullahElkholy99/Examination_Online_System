using ExamOnline.MVC.DTO;

namespace ExamOnline.MVC.Repositories.Interface;

public interface IExamAttemptRepository
{
    Task<List<ExamAttemptDTO>> GetAllAsync();

    Task<ExamAttemptDTO?> GetByIdAsync(
        int studentId,
        int examId,
        int courseId
    );
}
