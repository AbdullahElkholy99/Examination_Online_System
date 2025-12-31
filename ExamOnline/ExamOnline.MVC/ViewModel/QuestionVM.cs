namespace ExamOnline.MVC.ViewModel;
public class QuestionReviewVM
{
    public string QuestionText { get; set; }
    public int Marks { get; set; }
    public int AwardedMarks { get; set; }
    public string? Explanation { get; set; }
    public List<ChoiceReviewVM> Choices { get; set; } = new();
}
public class ChoiceReviewVM
{
    public string ChoiceText { get; set; }
    public bool IsCorrect { get; set; }
    public bool IsStudentChoice { get; set; }

}
