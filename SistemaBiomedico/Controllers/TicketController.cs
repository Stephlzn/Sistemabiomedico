using SistemaBiomedico.Data;
using SistemaBiomedico.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace SistemaBiomedico.Controllers
{
    [Authorize(Roles = "Operativo, Tecnico")]
    public class TicketController : Controller
    {
        private readonly AppDbContext _context;

        public TicketController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            List<Ticket> lista = await _context.Tickets
                .Include(t => t.Equipo)
                .ToListAsync();

            return View(lista);
        }

        [HttpGet]
        public IActionResult Nuevo()
        {
            ViewBag.Equipos = new SelectList(
                _context.Equipos.ToList(),
                "IdEquipo",
                "NombreEquipo"
            );
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Nuevo(Ticket ticket)
        {
            ticket.FechaCreacion = DateOnly.FromDateTime(DateTime.Now);
            ticket.Estado = "Pendiente";

            await _context.Tickets.AddAsync(ticket);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Listar));
        }
        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {
            Ticket ticket = await _context.Tickets
                .FirstAsync(t => t.IdTicket == id);

            ViewBag.Equipos = _context.Equipos.ToList();

            return View(ticket);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(Ticket ticket)
        {
            _context.Tickets.Update(ticket);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Listar));
        }
        [HttpGet]
        public async Task<IActionResult> Eliminar(int id)
        {
            Ticket ticket = await _context.Tickets
                .FirstAsync(t => t.IdTicket == id);

            _context.Tickets.Remove(ticket);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Listar));
        }
    }
}