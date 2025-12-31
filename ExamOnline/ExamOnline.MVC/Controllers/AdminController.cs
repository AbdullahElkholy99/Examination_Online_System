using ExamOnline.MVC.Data;
using Microsoft.AspNetCore.Mvc;

namespace ExamOnline.MVC.Controllers;

public class AdminController : Controller
{
    private readonly AppDbContext _context;

    public AdminController(AppDbContext context)
    {
        _context = context;
    }
    public IActionResult Login()
    {
        return View();
    }

    public IActionResult Dashboard()
    {
        int studentCount = _context.Students.Count();
        int instructorCount = _context.Instructors.Count();
        int courseCount = _context.Courses.Count();
        int examCount = _context.Exams.Count();
        int examAttemptCount = _context.ExamAttempts.Count();


        ViewBag.StudentCount = studentCount;
        ViewBag.InstructorCount = instructorCount;
        ViewBag.CourseCount = courseCount;
        ViewBag.ExamCount = examCount;



        ViewBag.ChartStudents = studentCount;
        ViewBag.ChartExams = examCount;
        ViewBag.ChartAttempts = examAttemptCount;

        return View();
    }
}

