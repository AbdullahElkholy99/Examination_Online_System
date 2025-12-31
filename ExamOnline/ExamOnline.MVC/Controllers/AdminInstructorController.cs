using ExamOnline.MVC.Data;
using ExamOnline.MVC.DTO;
using ExamOnline.MVC.DTO.Users;
using ExamOnline.MVC.Models;
using ExamOnline.MVC.Repositories;
using ExamOnline.MVC.Repositories.Interface.Users;
using ExamOnline.MVC.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

public class AdminInstructorController : Controller
{
    private readonly IInstructorRepository _repo;
    private readonly AppDbContext _context;

    public AdminInstructorController(IInstructorRepository repo, AppDbContext context)
    {
        _repo = repo;
        _context = context;
    }

    public async Task<IActionResult> Index()
        => View(await _repo.GetAllAsync());

    public IActionResult Create()
    {
        ViewBag.Branches = _context.Branches
            .Select(b => new SelectListItem
            {
                Value = b.Id.ToString(),
                Text = b.Name
            }).ToList();

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateInstructorVM vm)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Branches = _context.Branches
                .Select(b => new SelectListItem
                {
                    Value = b.Id.ToString(),
                    Text = b.Name
                }).ToList();

            return View(vm);
        }

        var instructor = new Instructor
        {
            Name = vm.Name,
            Email = vm.Email,
            BranchId = vm.BranchId,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now 
        };

        _context.Instructors.Add(instructor);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Edit(int id)
    {
        var instructor = await _repo.GetByIdAsync(id);
        if (instructor == null) return NotFound();
        return View(instructor);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(InstructorDTO dto)
    {
        if (!ModelState.IsValid)
            return View(dto);

        await _repo.UpdateAsync(dto);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        await _repo.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }
}
