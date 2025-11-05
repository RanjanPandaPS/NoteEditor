using Microsoft.AspNetCore.Mvc;
using NoteEditor.Models;
using NoteEditor.Repositories;
using System.Threading.Tasks;

namespace NoteEditor.Controllers
{
    public class NotesController : Controller
    {
        private readonly INoteRepository _repo;
        public NotesController(INoteRepository repo)
        {
            _repo = repo;
        }

        public async Task<IActionResult> Index()
        {
            var notes = await _repo.GetAllAsync();
            return View(notes);
        }

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(Note note)
        {
            if (ModelState.IsValid)
            {
                await _repo.AddAsync(note);
                return RedirectToAction(nameof(Index));
            }
            return View(note);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var note = await _repo.GetByIdAsync(id);
            if (note == null) return NotFound();
            return View(note);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Note note)
        {
            if (ModelState.IsValid)
            {
                await _repo.UpdateAsync(note);
                return RedirectToAction(nameof(Index));
            }
            return View(note);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _repo.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
