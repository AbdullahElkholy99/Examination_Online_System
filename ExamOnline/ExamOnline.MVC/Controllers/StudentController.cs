using ExamOnline.MVC.Data;
using ExamOnline.MVC.Repositories.Interface.Users;
using ExamOnline.MVC.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ExamOnline.MVC.Controllers;

public class StudentController : Controller
{
    private readonly AppDbContext _context;
    private readonly IStudentRepository _studentRepo;

    public StudentController(AppDbContext context, IStudentRepository studentRepo)
    {
        _context = context;
        _studentRepo = studentRepo;
    }

    // ===============================
    // Helper: Get StudentId from Session
    // ===============================
    private int? GetStudentId()
    {
        return HttpContext.Session.GetInt32("StudentId");
    }

    // ===============================
    // Dashboard
    // ===============================
    public async Task<IActionResult> Dashboard()
    {

        ViewBag.StudentList = new SelectList(
            _context.Students.ToList(),
            "Id",
            "Name",
            ViewBag.SelectedStudentId
        );


        int studentId = HttpContext.Session.GetInt32("StudentId") ?? 0;

        ViewBag.NotificationCount = _context.StudentCourses
            .Where(sc => sc.StudentId == studentId &&
                   sc.Course.Exams.Any(e =>
                       !_context.ExamAttempts.Any(a =>
                           a.StudentId == studentId &&
                           a.ExamId == e.Id)))
            .Count();


        //-----------------------------------
        var students = await _studentRepo.GetAllAsync();
        ViewBag.Students = students;
        //-----------------------------------

        ViewBag.SelectedStudentId = GetStudentId();


        return View();
    }

    [HttpPost]
    public IActionResult SelectStudent(int studentId)
    {
        HttpContext.Session.SetInt32("StudentId", studentId);
        return RedirectToAction("Dashboard");
    }

    // ===============================
    // Courses
    // ===============================
    public async Task<IActionResult> Courses()
    {
        int? studentId = GetStudentId();
        if (studentId == null)
            return RedirectToAction("Dashboard");

        var student = await _context.Students
            .AsNoTracking()
            .Include(s => s.Branch)
            .Include(s => s.Track)
            .FirstOrDefaultAsync(s => s.Id == studentId);

        var courses = await _context.StudentCourses
            .AsNoTracking()
            .Where(sc => sc.StudentId == studentId)
            .Select(sc => new CourseWithExamVM
            {
                CourseId = sc.Course.Id,
                CourseName = sc.Course.Name,
                Duration = sc.Course.Duration ?? 0,
                ExamId = sc.Course.Exams
                    .Select(e => (int?)e.Id)
                    .FirstOrDefault()
            })
            .ToListAsync();

        return View(new StudentCoursesVM
        {
            Student = student!,
            Courses = courses
        });
    }

    // ===============================
    // Results
    // ===============================
    public async Task<IActionResult> Results()
    {
        int? studentId = GetStudentId();
        if (studentId == null)
            return RedirectToAction("Dashboard");

        var student = await _context.Students
            .AsNoTracking()
            .Include(s => s.Branch)
            .Include(s => s.Track)
            .FirstAsync(s => s.Id == studentId);

        var courses = await _context.StudentCourses
            .AsNoTracking()
            .Where(sc => sc.StudentId == studentId)
            .Select(sc => new StudentCourseResultVM
            {
                CourseId = sc.Course.Id,
                CourseName = sc.Course.Name,

                ExamId = sc.Course.Exams
                    .Select(e => (int?)e.Id)
                    .FirstOrDefault(),

                IsTaken = _context.ExamAttempts
                    .Any(a => a.StudentId == studentId &&
                              a.CourseId == sc.Course.Id),

                Grade = _context.ExamAttempts
                    .Where(a => a.StudentId == studentId &&
                                a.CourseId == sc.Course.Id)
                    .Select(a => a.Grades)
                    .FirstOrDefault()
            })
            .ToListAsync();

        return View(new StudentResultsVM
        {
            Student = student,
            Courses = courses
        });
    }

    // ===============================
    // Review
    // ===============================
    public async Task<IActionResult> Review(int examId)
    {
        int studentId = HttpContext.Session.GetInt32("StudentId")!.Value;

        var questions = await _context.Questions
            .Where(q => q.ExamId == examId)
            .Select(q => new QuestionReviewVM
            {
                QuestionText = q.Text,
                Marks = q.Marks,

                AwardedMarks = _context.StudentAnswers
                    .Where(a => a.StudentId == studentId &&
                                a.QuestionId == q.Id)
                    .Select(a => a.ScoreAwarded)
                    .FirstOrDefault(),

                Explanation = "",

                Choices = q.Choices.Select(c => new ChoiceReviewVM
                {
                    ChoiceText = c.Text,
                    IsCorrect = c.IsCorrect,
                    IsStudentChoice = _context.StudentAnswers.Any(a =>
                        a.StudentId == studentId &&
                        a.QuestionId == q.Id &&
                        a.Choice == c.Text)
                }).ToList()
            })
            .AsNoTracking()
            .ToListAsync();

        return View(questions);
    }

    // ===============================
    // Progress
    // ===============================
    public async Task<IActionResult> Progress()
    {
        int? studentId = GetStudentId();
        if (studentId == null)
            return RedirectToAction("Dashboard");

        var data = await _context.ExamAttempts
            .AsNoTracking()
            .Where(a => a.StudentId == studentId && a.Grades != null)
            .Select(a => new
            {
                Course = a.Course.Name,
                Grade = a.Grades!.Value
            })
            .ToListAsync();

        return View(new StudentProgressVM
        {
            Courses = data.Select(d => d.Course).ToList(),
            Grades = data.Select(d => d.Grade).ToList()
        });
    }

    public IActionResult ResetStudent()
    {
        HttpContext.Session.Remove("StudentId");
        return RedirectToAction("Dashboard");
    }

}
