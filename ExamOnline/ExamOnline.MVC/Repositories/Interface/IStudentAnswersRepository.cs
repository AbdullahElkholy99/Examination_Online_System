using ExamOnline.MVC.DTO;

namespace ExamOnline.MVC.Repositories.Interface;

public interface IStudentAnswersRepository
{
    Task<List<StudentAnswersDTO>> GetAllAsync();

    Task<StudentAnswersDTO?> GetByIdAsync(
        int studentId,
        int examId,
        int questionId
    );
}
