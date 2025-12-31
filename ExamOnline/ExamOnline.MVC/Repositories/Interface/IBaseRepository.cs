using ExamOnline.MVC.DTO;

namespace ExamOnline.MVC.Repositories.Interface
{
    public interface IBaseRepository<T>
    {
        Task<List<T>> GetAllAsync();
        Task UpdateAsync(T dto);
        Task AddAsync(T t);
        Task<T?> GetByIdAsync(int id);
        Task DeleteAsync(int id);

    }
}