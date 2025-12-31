namespace ExamOnline.MVC.ViewModel;

public class CreateInstructorVM
{
    public string Name { get; set; } = null!;
    public string? Email { get; set; }

    public int BranchId { get; set; }
    public DateTime? CreatedAt { get; set; } = DateTime.Now;
    public DateTime? UpdatedAt { get; set; } = DateTime.Now;
}

