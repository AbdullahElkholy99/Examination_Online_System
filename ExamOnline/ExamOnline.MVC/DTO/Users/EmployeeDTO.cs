namespace ExamOnline.MVC.DTO.Users;

public record BaseUserDTO
{
    public int ID { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public int Branch_ID { get; set; } 
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
public record EmployeeDTO : BaseUserDTO
{
    public int? Manager_ID { get; set; } = null!;
}
