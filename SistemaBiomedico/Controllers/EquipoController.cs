using SistemaBiomedico.Data;
using SistemaBiomedico.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LOGIN_ROLES.Controllers
{

    public class EquipoController : Controller
    {
        private readonly AppDbContext _context;
        public EquipoController(AppDbContext context)
        {
            _context = context;
        }
        [Authorize(Roles = "Administrador, Tecnico")]
        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            List<Equipo> lista = await _context.Equipos.ToListAsync();
            return View(lista);
        }
        [Authorize(Roles = "Administrador")]
        [HttpGet]
        public IActionResult Nuevo()
        {
            ViewBag.Areas = new SelectList(
                _context.Areas.ToList(),
                "IdAreas",
                "NombreArea"
            );
            return View();
        }
        [Authorize(Roles = "Administrador")]
        [HttpPost]
        public async Task<IActionResult> Nuevo(Equipo equipo)
        {
            await _context.Equipos.AddAsync(equipo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Listar));
        }
        [Authorize(Roles = "Administrador")]
        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {
            Equipo equipo = await _context.Equipos.FirstAsync(e => e.IdEquipo == id);
            return View(equipo);
        }
        [Authorize(Roles = "Administrador")]
        [HttpPost]
        public async Task<IActionResult> Editar(Equipo equipo)
        {
            _context.Equipos.Update(equipo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Listar));
        }
        [Authorize(Roles = "Administrador")]
        [HttpGet]
        public async Task<IActionResult> Eliminar(int id)
        {
            Equipo equipo = await _context.Equipos.FirstAsync(e => e.IdEquipo == id);
            _context.Equipos.Remove(equipo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Listar));
        }
    }
}