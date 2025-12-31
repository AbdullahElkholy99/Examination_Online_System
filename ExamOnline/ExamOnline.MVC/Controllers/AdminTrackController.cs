using ExamOnline.MVC.DTO;
using ExamOnline.MVC.Repositories;
using ExamOnline.MVC.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;

public class AdminTrackController : Controller
{
    private readonly ITrackRepository _repo;

    public AdminTrackController(ITrackRepository repo)
    {
        _repo = repo;
    }

    public async Task<IActionResult> Index()
        => View(await _repo.GetAllAsync());

    public IActionResult Create() => View();

    [HttpPost]
    public async Task<IActionResult> Create(TrackDTO dto)
    {
        await _repo.AddAsync(dto);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var track = await _repo.GetByIdAsync(id);
        if (track == null) return NotFound();
        return View(track);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(TrackDTO dto)
    {
        await _repo.UpdateAsync(dto);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        await _repo.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }
}
