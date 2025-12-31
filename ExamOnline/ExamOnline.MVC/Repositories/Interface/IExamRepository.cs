using ExamOnline.MVC.DTO;

namespace ExamOnline.MVC.Repositories.Interface;

public interface IExamRepository
{
    Task<List<ExamDTO>> GetAllAsync();
    Task AddAsync(ExamDTO examDto);
     Task<ExamDTO?> GetByIdAsync(int id);
}
