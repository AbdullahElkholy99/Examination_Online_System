using ExamOnline.MVC.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;

namespace ExamOnline.MVC.Controllers;
public class CourseController : Controller
{
    private readonly ICourseRepository _repo;
    private readonly IStudentCoursesRepository _studentCourseRepo;

    public CourseController(ICourseRepository repo, IStudentCoursesRepository studentCourseRepo)
    {
        _repo = repo;
        _studentCourseRepo = studentCourseRepo;
    }

    public async Task<IActionResult> Index()
    {
        var courses = await _repo.GetAllAsync();
        return View(courses);
    }

    public async Task<IActionResult> Details(int id)
    {
        var course = await _repo.GetByIdAsync(id);
        if (course == null)
            return NotFound();

        return View(course);
    }
    public async Task<IActionResult> Courses(int id)
    {
        var courses = await _studentCourseRepo.GetStudentCoursesAsync(id);
        ViewBag.StudentId = id;
        return View(courses);
    }
    public async Task<IActionResult> RemoveCourse(int studentId, int courseId)
    {
        await _studentCourseRepo.RemoveCourseAsync(studentId, courseId);

        TempData["Success"] = "Course removed successfully";
        return RedirectToAction("Courses", new { id = studentId });
    }



}
