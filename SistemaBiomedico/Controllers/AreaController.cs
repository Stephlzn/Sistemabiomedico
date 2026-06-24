using SistemaBiomedico.Data;
using SistemaBiomedico.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace SistemaBiomedico.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class AreaController : Controller
    {
        private readonly AppDbContext _context;
        public AreaController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            List<Areas> lista = await _context.Areas.ToListAsync();
            return View(lista);
        }
        [HttpGet]
        public IActionResult Nuevo()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Nuevo(Areas area)
        {
            await _context.Areas.AddAsync(area);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Listar));
        }
        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {
            Areas areas = await _context.Areas.FirstAsync(e => e.IdAreas == id);
            return View(areas);
        }
        [HttpPost]
        public async Task<IActionResult> Editar(Areas areas)
        {
            _context.Areas.Update(areas);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Listar));
        }
        [HttpGet]
        public async Task<IActionResult> Eliminar(int id)
        {
            Areas areas = await _context.Areas.FirstAsync(e => e.IdAreas == id);
            _context.Areas.Remove(areas);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Listar));
        }
    }
}