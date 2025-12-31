
namespace ExamOnline.MVC.Controllers;

using ExamOnline.MVC.Data;
using ExamOnline.MVC.DTO.Users;
using ExamOnline.MVC.Repositories.Interface;
using ExamOnline.MVC.Repositories.Interface.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

public class AdminStudentController : Controller
{
    private readonly IStudentRepository _repo;
    private readonly ITrackRepository _trackRepo;
    private readonly AppDbContext _context;

    public AdminStudentController(IStudentRepository repo, AppDbContext context, ITrackRepository trackRepo)
    {
        _repo = repo;
        _context = context;
        _trackRepo = trackRepo;
    }

    public async Task<IActionResult> Index()
    {
        var students = await _repo.GetAllAsync();

        return View(students);
    }
    [HttpGet]
    public async Task<IActionResult> GetTracksByBranch(int branchId)
    {
        var tracks = await _trackRepo.GetTracksByBranchAsync(branchId);
        return Json(tracks);
    }


    public IActionResult Create()
    {
        var vm = new StudentCreationDTO
        {
            Branches = _context.Branches
                .Select(b => new SelectListItem
                {
                    Value = b.Id.ToString(),
                    Text = b.Name
                }).ToList(),

            Tracks = new List<SelectListItem>() // فاضي
        };

        return View(vm);
    }


    [HttpPost]
    public async Task<IActionResult> Create(StudentCreationDTO vm)
    {
        if (!ModelState.IsValid)
        {
            // Reload dropdowns لو حصل Validation Error
            vm.Branches = _context.Branches
                .Select(b => new SelectListItem
                {
                    Value = b.Id.ToString(),
                    Text = b.Name
                }).ToList();

            vm.Tracks = _context.Tracks
                .Select(t => new SelectListItem
                {
                    Value = t.Id.ToString(),
                    Text = t.Name
                }).ToList();

            return View(vm);
        }

        var dto = new StudentCreationDTO
        {
            Name = vm.Name,
            Email = vm.Email,
            Phone = vm.Phone,
            Address = vm.Address,
            Branch_ID = vm.Branch_ID,
            Track_ID = vm.Track_ID
        };


        await _repo.AddAsync(vm);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var student = await _repo.GetByIdAsync(id);
        if (student == null) return NotFound();
        return View(student);
    }

    public async Task<IActionResult> Delete(int id)
    {
        await _repo.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }
    public IActionResult Details(int id)
    {
        var student = _context.Students
            .FirstOrDefault(s => s.Id == id);

        return View(student);
    }




}
