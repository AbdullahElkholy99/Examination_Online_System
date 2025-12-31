using ExamOnline.MVC.DTO.Users;
using ExamOnline.MVC.Models;

namespace ExamOnline.MVC.Repositories.Interface.Users;
public interface IStudentRepository : IBaseRepository<StudentDTO>
{
    Task AddAsync(StudentCreationDTO t);
}
