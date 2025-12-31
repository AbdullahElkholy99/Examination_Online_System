using ExamOnline.MVC.Data;
using ExamOnline.MVC.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;
using ExamOnline.MVC.ViewModel;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ExamOnline.MVC.Controllers;

public class AdminStudentCourseController : Controller
{
    private readonly AppDbContext _context;
    private readonly IStudentCoursesRepository _repo;
    private readonly IStudentCoursesRepository _studentCourseRepo;

    public AdminStudentCourseController(
        AppDbContext context,
        IStudentCoursesRepository repo,
        IStudentCoursesRepository studentCourseRepo)
    {
        _context = context;
        _repo = repo;
        _studentCourseRepo = studentCourseRepo;
    }

    [HttpGet]
    public IActionResult Assign()
    {
        var vm = new AssignCourseVM
        {
            Students = _context.Students
                .Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = s.Name
                }).ToList(),

            Courses = _context.Courses
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }).ToList()
        };

        return View(vm);
    }

    [HttpPost]
    public async Task<IActionResult> Assign(AssignCourseVM vm)
    {
        await _repo.AssignCourseAsync(vm.StudentId, vm.CourseId);

        TempData["Success"] = "Course assigned successfully";
        return RedirectToAction("Assign");
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
    [HttpGet]
    public IActionResult AssignByTrack()
    {
        var vm = new AssignByTrackVM
        {
            Students = _context.Students
                .Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = s.Name
                }).ToList(),

            Tracks = _context.Tracks
                .Select(t => new SelectListItem
                {
                    Value = t.Id.ToString(),
                    Text = t.Name
                }).ToList()
        };

        return View(vm);
    }

    [HttpPost]
    public async Task<IActionResult> AssignByTrack(AssignByTrackVM vm)
    {
        await _studentCourseRepo.AssignTrackCoursesAsync(vm.StudentId, vm.TrackId);

        TempData["Success"] = "Courses assigned based on track successfully";
        return RedirectToAction("AssignByTrack");
    }
    [HttpPost]
    public async Task<IActionResult> BulkAssignByTrack(int trackId)
    {
        await _studentCourseRepo.AssignTrackCoursesToAllAsync(trackId);

        TempData["Success"] = "Courses assigned to all students in track";
        return RedirectToAction("AssignByTrack");
    }


}

