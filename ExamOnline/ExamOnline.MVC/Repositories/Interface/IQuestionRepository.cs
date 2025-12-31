using ExamOnline.MVC.DTO;

namespace ExamOnline.MVC.Repositories.Interface;

public interface IQuestionRepository : IBaseRepository<QuestionDTO>
{
    Task<List<QuestionDTO>> GetByExamAsync(int examId);

}
