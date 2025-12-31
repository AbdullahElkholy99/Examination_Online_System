using ExamOnline.MVC.Data;
using ExamOnline.MVC.Models;
using ExamOnline.MVC.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

public class ExamAttemptController : Controller
{
    private readonly AppDbContext _context;

    public ExamAttemptController(AppDbContext context)
    {
        _context = context;
    }

    // ===============================
    // Start Exam
    // ===============================
    public IActionResult StartExam(int examId)
    {
        int? studentId = HttpContext.Session.GetInt32("StudentId");
        if (studentId == null)
            return RedirectToAction("Dashboard", "Student");

        var exam = _context.Exams
            .Include(e => e.Questions)
            .FirstOrDefault(e => e.Id == examId);

        if (exam == null)
            return NotFound();

        bool alreadyTaken = _context.ExamAttempts
            .Any(a => a.StudentId == studentId && a.ExamId == examId);

        if (alreadyTaken)
            return RedirectToAction("Results", "Student");

        // Save exam session
        HttpContext.Session.SetInt32("ExamId", examId);
        HttpContext.Session.SetInt32("ExamStartTime",
            (int)DateTimeOffset.UtcNow.ToUnixTimeSeconds());

        // Generate fixed random order
        var order = exam.Questions
            .Select(q => q.Id)
            .OrderBy(_ => Guid.NewGuid())
            .ToList();

        HttpContext.Session.SetString(
            "QuestionOrder",
            JsonSerializer.Serialize(order));

        return RedirectToAction("TakeExam", new { q = 0 });
    }

    // ===============================
    // Take Exam
    // ===============================
    public IActionResult TakeExam()
    {
        int? studentId = HttpContext.Session.GetInt32("StudentId");
        int? examId = HttpContext.Session.GetInt32("ExamId");

        if (studentId == null || examId == null)
            return RedirectToAction("Dashboard", "Student");

        var exam = _context.Exams
            .Include(e => e.Questions)
                .ThenInclude(q => q.Choices)
            .First(e => e.Id == examId);

        var startTime = DateTimeOffset.FromUnixTimeSeconds(
            HttpContext.Session.GetInt32("ExamStartTime")!.Value);

        int durationSeconds = 60;
        int elapsed = (int)(DateTimeOffset.UtcNow - startTime).TotalSeconds;
        int remaining = durationSeconds - elapsed;

        if (remaining <= 0)
            return RedirectToAction("Submit");

        var vm = new ExamTakeVM
        {
            ExamId = exam.Id,
            StudentId = studentId.Value,
            RemainingSeconds = remaining,
            Questions = exam.Questions
                .OrderBy(_ => Guid.NewGuid())
                .Select(q => new QuestionExamVM
                {
                    QuestionId = q.Id,
                    QuestionText = q.Text,
                    Choices = q.Choices
                        .OrderBy(_ => Guid.NewGuid())
                        .Select(c => new ChoiceVM
                        {
                            ChoiceId = c.Id,
                            ChoiceText = c.Text
                        }).ToList()
                }).ToList()
        };

        return View(vm);
    }


    // ===============================
    // Save Answer
    // ===============================
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult SaveAnswer(int questionId, int selectedChoiceId, int index)
    {
        int studentId = HttpContext.Session.GetInt32("StudentId")!.Value;
        int examId = HttpContext.Session.GetInt32("ExamId")!.Value;

        var question = _context.Questions
            .FirstOrDefault(q => q.Id == questionId);

        if (question == null)
            return RedirectToAction("TakeExam", new { q = index });

        // ❗ لو مفيش اختيار (سؤال من غير Choices)
        var choice = _context.Choices
            .FirstOrDefault(c =>
                c.Id == selectedChoiceId &&
                c.QuestionId == questionId);

        bool isCorrect = false;
        string? choiceText = null;

        if (choice != null)
        {
            isCorrect = choice.IsCorrect;
            choiceText = choice.Text;
        }

        int score = isCorrect ? question.Marks : 0;

        var answer = _context.StudentAnswers
            .FirstOrDefault(a =>
                a.StudentId == studentId &&
                a.ExamId == examId &&
                a.QuestionId == questionId);

        if (answer == null)
        {
            _context.StudentAnswers.Add(new StudentAnswer
            {
                StudentId = studentId,
                ExamId = examId,
                QuestionId = questionId,
                Choice = choiceText,     // ممكن تبقى null
                IsCorrect = isCorrect,
                ScoreAwarded = score,
                AnsweredAt = DateTime.Now
            });
        }
        else
        {
            answer.Choice = choiceText;
            answer.IsCorrect = isCorrect;
            answer.ScoreAwarded = score;
            answer.AnsweredAt = DateTime.Now;
        }

        _context.SaveChanges();

        return RedirectToAction("TakeExam", new { q = index + 1 });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult SaveAllAnswers(Dictionary<int, int> answers)
    {
        int studentId = HttpContext.Session.GetInt32("StudentId")!.Value;
        int examId = HttpContext.Session.GetInt32("ExamId")!.Value;

        if (!_context.ExamAttempts.Any(a =>
            a.StudentId == studentId && a.ExamId == examId))
        {

            int score = _context.StudentAnswers
             .Where(a => a.StudentId == studentId && a.ExamId == examId)
             .Sum(a => a.ScoreAwarded);
            int courseId = _context.Exams
                .Where(e => e.Id == examId)
                .Select(e => e.Course_Id)
                .First();

            _context.ExamAttempts.Add(new ExamAttempt
            {
                StudentId = studentId,
                ExamId = examId,
                CourseId = courseId,
                Grades = score,
                AttemptAt = DateTime.Now
            });

            _context.SaveChanges();

        }
        foreach (var item in answers)
        {
            int questionId = item.Key;
            int selectedChoiceId = item.Value;

            var question = _context.Questions.First(q => q.Id == questionId);
            var choice = _context.Choices
                .FirstOrDefault(c => c.Id == selectedChoiceId && c.QuestionId == questionId);

            bool isCorrect = choice != null && choice.IsCorrect;
            int score = isCorrect ? question.Marks : 0;

            var answer = _context.StudentAnswers
                .FirstOrDefault(a =>
                    a.StudentId == studentId &&
                    a.ExamId == examId &&
                    a.QuestionId == questionId);

            var studentAnswer = new StudentAnswer()
            {
                Choice = choice?.Text,
                IsCorrect = isCorrect,
                ScoreAwarded = score,
                AnsweredAt = DateTime.Now
            };

            if (answer == null)
            {
                studentAnswer.StudentId = studentId;
                studentAnswer.ExamId = examId;
                studentAnswer.QuestionId = questionId;
            }
            _context.StudentAnswers.Add(studentAnswer);
            _context.SaveChanges();
        }


        return RedirectToAction("Results", "Student");
    }


    // ===============================
    // Submit Exam
    // ===============================
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Submit()
    {
        int studentId = HttpContext.Session.GetInt32("StudentId")!.Value;
        int examId = HttpContext.Session.GetInt32("ExamId")!.Value;

        int score = _context.StudentAnswers
            .Where(a => a.StudentId == studentId && a.ExamId == examId)
            .Sum(a => a.ScoreAwarded);

        if (!_context.ExamAttempts.Any(a =>
            a.StudentId == studentId && a.ExamId == examId))
        {
            int courseId = _context.Exams
                .Where(e => e.Id == examId)
                .Select(e => e.Course_Id)
                .First();

            _context.ExamAttempts.Add(new ExamAttempt
            {
                StudentId = studentId,
                ExamId = examId,
                CourseId = courseId,   
                Grades = score,
                AttemptAt = DateTime.Now
            });

            _context.SaveChanges();
        }

        // Clear session
        HttpContext.Session.Remove("ExamId");
        HttpContext.Session.Remove("ExamStartTime");
        HttpContext.Session.Remove("QuestionOrder");

        return RedirectToAction("Results", "Student");
    }
}
