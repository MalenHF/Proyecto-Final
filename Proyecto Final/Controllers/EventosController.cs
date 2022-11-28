using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Proyecto_Final.Models.dbModels;
using Proyecto_Final.Models.DTO;
using Proyecto_Final.ViewModels;

namespace Proyecto_Final.Controllers
{
    
    public class EventosController : Controller
    {
        private readonly ProyectoFinalContext _context;
        private readonly UserManager<AplicationUser> _userManager;

        public EventosController(ProyectoFinalContext context, UserManager<AplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Eventos
        public async Task<IActionResult> Index()
        {
            var proyectoFinalContext = _context.Eventos.Include(e => e.IdUsuarioNavigation).OrderByDescending(x => x.FechaEvento);
            return View(await proyectoFinalContext.ToListAsync());
        }
        public async Task<IActionResult> IndexUser()
        {
            var proyectoFinalContext = _context.Eventos.OrderByDescending(x => x.FechaEvento).Where(x => x.EstatusEvento == true);
            return View(await proyectoFinalContext.ToListAsync());           
        }

        public async Task<IActionResult> ultimosEventos()
        {
            var eventos = _context.Eventos.OrderByDescending(x => x.FechaEvento).Take(5);
            return View(await eventos.ToListAsync());
        }

        // GET: Eventos/Details/5f
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Eventos == null)
            {
                return NotFound();
            }

            var evento = await _context.Eventos
                .Include(e => e.IdUsuarioNavigation)
                .FirstOrDefaultAsync(m => m.IdEvento == id);
            if (evento == null)
            {
                return NotFound();
            }

            return View(evento);
        }

        // GET: Eventos/Create
        public IActionResult Create()
        {
            ViewData["IdUsuario"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Eventos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(EventoCreateDTO evento)
        {
            
            if (ModelState.IsValid)
            {
                AplicationUser user = await _userManager.GetUserAsync(User);
                if(user == null)
                {
                    return BadRequest();
                }
                Evento e = new Evento
                {
                    IdUsuario = user.Id,
                    TituloEvento = evento.TituloEvento,
                    ContenidoEvento = evento.ContenidoEvento,
                    EstatusEvento = evento.EstatusEvento,
                    FechaEvento = DateTime.Now,
                    Foto = evento.Foto
                };
                _context.Add(e);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdUsuario"] = new SelectList(_context.Users, "Id", "Id", evento.IdUsuario);
            return View(evento);
        }

        // GET: Eventos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Eventos == null)
            {
                return NotFound();
            }

            var evento = await _context.Eventos.FindAsync(id);
            if (evento == null)
            {
                return NotFound();
            }
            EventoUpdateDTO eud = new EventoUpdateDTO();
            eud.IdEvento = evento.IdEvento;
            eud.TituloEvento = evento.TituloEvento;
            eud.EstatusEvento = evento.EstatusEvento;
            eud.FechaEvento = evento.FechaEvento;
            eud.ContenidoEvento = evento.ContenidoEvento;
            eud.Foto = evento.Foto;

            
            ViewData["IdUsuario"] = new SelectList(_context.Users, "Id", "Id", evento.IdUsuario);
            return View(eud);
        }

        // POST: Eventos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
       
        public async Task<IActionResult> Edit(int id, EventoUpdateDTO evento)
        {
            if (id != evento.IdEvento)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Evento e = new Evento
                    {
                        IdEvento = evento.IdEvento,
                        IdUsuario = evento.IdUsuario,
                        TituloEvento = evento.TituloEvento,
                        ContenidoEvento = evento.ContenidoEvento,
                        EstatusEvento = evento.EstatusEvento,
                        FechaEvento = DateTime.Now,
                        Foto = evento.Foto
                    };
                    _context.Update(e);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventoExists(evento.IdEvento))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdUsuario"] = new SelectList(_context.Users, "Id", "Id", evento.IdUsuario);
            return View(evento);
        }

        // GET: Eventos/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Eventos == null)
            {
                return NotFound();
            }

            var evento = await _context.Eventos
                .Include(e => e.IdUsuarioNavigation)
                .FirstOrDefaultAsync(m => m.IdEvento == id);
            if (evento == null)
            {
                return NotFound();
            }

            return View(evento);
        }

        // POST: Eventos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Eventos == null)
            {
                return Problem("Entity set 'ProyectoFinalContext.Eventos'  is null.");
            }
            var evento = await _context.Eventos.FindAsync(id);
            if (evento != null)
            {
                _context.Eventos.Remove(evento);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventoExists(int id)
        {
          return _context.Eventos.Any(e => e.IdEvento == id);
        }
    }
}
