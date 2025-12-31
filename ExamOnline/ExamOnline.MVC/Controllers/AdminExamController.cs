using ExamOnline.MVC.Data;
using ExamOnline.MVC.DTO;
using ExamOnline.MVC.Models;
using ExamOnline.MVC.Repositories.Implement.Select;
using ExamOnline.MVC.Repositories.Interface;
using ExamOnline.MVC.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ExamOnline.MVC.Controllers;

public class AdminExamController : Controller
{
    private readonly IExamRepository _examRepo;
    private readonly IQuestionRepository _questionRepo;
    private readonly IChoiceRepository _choiceRepo;
    private readonly ICourseRepository _courseRepo;
    private readonly AppDbContext _context;

    public AdminExamController(
        IExamRepository examRepo,
        IQuestionRepository questionRepo,
        IChoiceRepository choiceRepo,
        ICourseRepository courseRepo,
        AppDbContext context)
    {
        _examRepo = examRepo;
        _questionRepo = questionRepo;
        _choiceRepo = choiceRepo;
        _courseRepo = courseRepo;
        _context = context;
    }

    public async Task<IActionResult> Index()
        => View(await _examRepo.GetAllAsync());

    [HttpGet]
    public IActionResult CreateExam()
    {
        var vm = new CreateExamVM
        {
            Courses = _context.Courses
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                })
                .ToList()
        };

        return View(vm);
    }

    [HttpPost]
    public IActionResult CreateExam(CreateExamVM vm)
    {
        if (!ModelState.IsValid)
        {
            vm.Courses = _context.Courses
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                })
                .ToList();

            return View(vm);
        }

        var exam = new Exam
        {
            Course_Id = vm.CourseId,
            Total_Marks = vm.TotalMarks,
            Type = vm.Type,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };

        _context.Exams.Add(exam);
        _context.SaveChanges();

        return RedirectToAction("Details", new { id = exam.Id });
    }

    //-----------------------------------------------------------------------
    // Create Exam 
    public IActionResult Create()
    {
        var courses = _courseRepo.GetAllAsync().Result;
        return View(courses);
    }

    [HttpPost]
    public IActionResult Create(ExamCreateVM vm)
    {
        //var courses = _courseRepo.GetAllAsync().Result;
        if (!ModelState.IsValid)
        {
            vm.Courses = _context.Courses.ToList();
            return View(vm);
        }

        _context.Exams.Add(new Exam
        {
            Type = vm.Type,
            Total_Marks = vm.Total_Marks,
            CreatedAt = vm.CreatedAt,
            UpdatedAt = vm.UpdatedAt,
            Course_Id = vm.Course_Id,
        });

        _context.SaveChanges();
        return RedirectToAction("Index");
    }

    //-----------------------------------------------------------------------
    // Create Question
    public IActionResult AddQuestion(int examId)
    {
        return View(new QuestionCreateVM
        {
            ExamId = examId,
            QuestionType = "MCQ",
            Choices = new List<ChoiceCreateVM>
            {
                new(), new(), new(), new()
            }
        });
    }

    [HttpPost]
    public IActionResult AddQuestion(QuestionCreateVM vm)
    {
        var question = new Question
        {
            ExamId = vm.ExamId,
            Text = vm.QuestionText,
            Type = vm.QuestionType,
            Marks = vm.Marks,
        };

        _context.Questions.Add(question);
        _context.SaveChanges();

        foreach (var c in vm.Choices)
        {
            if(c.Text != "")
                _context.Choices.Add(new Choice
                {
                    QuestionId = question.Id,
                    Text = c.Text,
                    IsCorrect = c.IsCorrect
                });
        }

        _context.SaveChanges();
        return RedirectToAction("Details", new { id = vm.ExamId });
    }
    //-----------------------------------------------------------------------
    public IActionResult Details(int id)
    {
        var exam = _context.Exams
            .Include(e => e.Course)
            .Include(e => e.Questions)
                .ThenInclude(q => q.Choices)
            .FirstOrDefault(e => e.Id == id);

        if (exam == null)
            return NotFound();

        var vm = new ExamDetailsVM
        {
            ExamId = exam.Id,
            CourseName = exam.Course.Name,
            ExamType = exam.Type,
            TotalMarks = exam.Total_Marks,
            IsClosed = false,
            Questions = exam.Questions.Select(q => new QuestionDetailsVM
            {
                QuestionId = q.Id,
                QuestionText = q.Text,
                QuestionType = q.Type!,
                Marks = q.Marks,
                Choices = q.Choices.Select(c => new ChoiceDetailsVM
                {
                    Text = c.Text,
                    IsCorrect = c.IsCorrect
                }).ToList()
            }).ToList()
        };

        return View(vm);
    }
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _questionRepo.DeleteAsync(id);
            return RedirectToAction("Details");

        }
        catch
        {
            return View("Error");
        }
 

    }


}
