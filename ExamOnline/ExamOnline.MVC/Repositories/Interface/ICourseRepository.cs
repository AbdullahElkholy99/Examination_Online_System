using ExamOnline.MVC.DTO;
using ExamOnline.MVC.Models;
using ExamOnline.MVC.ViewModel;

namespace ExamOnline.MVC.Repositories.Interface;

public interface ICourseRepository : IBaseRepository<CourseDTO>
{
    Task AddAsync(CreateCourseVM t);

}
