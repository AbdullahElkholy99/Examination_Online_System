namespace ExamOnline.MVC.DTO;

public record BranchDTO
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string B_Loc { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }


}
