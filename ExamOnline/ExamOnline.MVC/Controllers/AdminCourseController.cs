namespace ExamOnline.MVC.Controllers;

using ExamOnline.MVC.Data;
using ExamOnline.MVC.DTO;
using ExamOnline.MVC.Models;
using ExamOnline.MVC.Repositories;
using ExamOnline.MVC.Repositories.Interface;
using ExamOnline.MVC.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

public class AdminCourseController : Controller
{
    private readonly ICourseRepository _repo;
    private readonly AppDbContext _context;

    public AdminCourseController(ICourseRepository repo, AppDbContext context)
    {
        _repo = repo;
        _context = context;
    }

    public async Task<IActionResult> Index()
        => View(await _repo.GetAllAsync());

    public IActionResult Create()
    {
        var vm = new CreateCourseVM
        {
            Instructors = _context.Instructors
                .Select(i => new SelectListItem
                {
                    Value = i.Id.ToString(),
                    Text = i.Name
                }).ToList()
        };

        return View(vm);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateCourseVM vm)
    {
        if (!ModelState.IsValid)
        {
            vm.Instructors = _context.Instructors
                .Select(i => new SelectListItem
                {
                    Value = i.Id.ToString(),
                    Text = i.Name
                }).ToList();

            return View(vm);
        }

        var course = new Course
        {
            Name = vm.Name,
            Duration = vm.Duration,
            InstructorId = vm.InstructorId,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };

        _context.Courses.Add(course);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index");
    }


    public async Task<IActionResult> Edit(int id)
    {
        var course = await _repo.GetByIdAsync(id);
        if (course == null) return NotFound();
        return View(course);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(CourseDTO dto)
    {
        if (!ModelState.IsValid) return View(dto);
        await _repo.UpdateAsync(dto);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        await _repo.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }
}

