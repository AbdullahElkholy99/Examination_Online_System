using Microsoft.AspNetCore.Mvc.Rendering;

namespace ExamOnline.MVC.DTO.Users;

/*
 
public record BaseUserDTO
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}*/
public record StudentDTO 
{
    public int ID { get; set; }

    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public int Branch_ID { get; set; } 
    public string Phone { get; set; } = null!;
    public string Address { get; set; } = null!;
    public string? BranchName { get; set; }
     public int? Track_ID { get; set; } = null!;
    public string? TrackName { get; set; } = null!;
}
public record StudentCreationDTO : BaseUserDTO
{
    public string Phone { get; set; } = null!;
    public string Address { get; set; } = null!;
    public int? Track_ID { get; set; } = null!;
    public List<SelectListItem>? Branches { get; set; } = new();
    public List<SelectListItem>? Tracks { get; set; } = new();
}

public class StudentIndexVM
{
    public List<StudentDTO> Students { get; set; } = new();
    public List<SelectListItem>? Branches { get; set; } = new();
    public List<SelectListItem>? Tracks { get; set; } = new();
}
