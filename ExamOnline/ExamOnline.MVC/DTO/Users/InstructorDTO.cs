using Microsoft.AspNetCore.Mvc.Rendering;

namespace ExamOnline.MVC.DTO.Users;

public record InstructorDTO : BaseUserDTO
{
    public string BranchName { get; set; }
}
public record InstructorCreationDTO : BaseUserDTO
{
    public List<SelectListItem>? Branches { get; set; } = new();
}