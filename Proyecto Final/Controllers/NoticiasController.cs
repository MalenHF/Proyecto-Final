using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Proyecto_Final.Models.dbModels;
using Proyecto_Final.Models.DTO;

namespace Proyecto_Final.Controllers
{
    [Authorize(Roles = "Admin")]
    public class NoticiasController : Controller
    {
        private readonly ProyectoFinalContext _context;
        private readonly UserManager<AplicationUser> _userManager;

        public NoticiasController(ProyectoFinalContext context, UserManager<AplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Noticias
        public async Task<IActionResult> Index()
        {
            var proyectoFinalContext = _context.Noticias.Include(n => n.IdUsuarioNavigation).OrderByDescending(x => x.FechaNoticia);
            return View(await proyectoFinalContext.ToListAsync());
        }
        public async Task<IActionResult> IndexUser()
        {
            var proyectoFinalContext = _context.Noticias.OrderByDescending(x => x.FechaNoticia).Where(x => x.NoticiaStatus == true);
            return View(await proyectoFinalContext.ToListAsync());
        }

        // GET: Noticias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Noticias == null)
            {
                return NotFound();
            }

            var noticia = await _context.Noticias
                .Include(n => n.IdUsuarioNavigation)
                .FirstOrDefaultAsync(m => m.IdNoticia == id);
            if (noticia == null)
            {
                return NotFound();
            }

            return View(noticia);
        }

        // GET: Noticias/Create
        public IActionResult Create()
        {
            ViewData["IdUsuario"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Noticias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(NoticiaCreateDTO noticia)
        {
            if (ModelState.IsValid)
            {
                AplicationUser user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    return BadRequest();
                }
                Noticia n = new Noticia
                {
                    IdUsuario = user.Id,
                    TituloNoticia = noticia.tituloNoticia,
                    ContenidoNoticia = noticia.contenidoNoticia,
                    NoticiaStatus = noticia.noticiaStatus,
                    FechaNoticia = DateTime.Now,
                    Foto = noticia.Foto
                };
                _context.Add(n);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdUsuario"] = new SelectList(_context.Users, "Id", "Id", noticia.IdUsuario);
            return View(noticia);
        }

        // GET: Noticias/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Noticias == null)
            {
                return NotFound();
            }

            var noticia = await _context.Noticias.FindAsync(id);
            if (noticia == null)
            {
                return NotFound();
            }
            ViewData["IdUsuario"] = new SelectList(_context.Users, "Id", "Id", noticia.IdUsuario);
            return View(noticia);
        }

        // POST: Noticias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdNoticia,IdUsuario,TituloNoticia,ContenidoNoticia,NoticiaStatus,FechaNoticia,Foto")] Noticia noticia)
        {
            if (id != noticia.IdNoticia)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(noticia);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NoticiaExists(noticia.IdNoticia))
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
            ViewData["IdUsuario"] = new SelectList(_context.Users, "Id", "Id", noticia.IdUsuario);
            return View(noticia);
        }

        // GET: Noticias/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Noticias == null)
            {
                return NotFound();
            }

            var noticia = await _context.Noticias
                .Include(n => n.IdUsuarioNavigation)
                .FirstOrDefaultAsync(m => m.IdNoticia == id);
            if (noticia == null)
            {
                return NotFound();
            }

            return View(noticia);
        }

        // POST: Noticias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Noticias == null)
            {
                return Problem("Entity set 'ProyectoFinalContext.Noticias'  is null.");
            }
            var noticia = await _context.Noticias.FindAsync(id);
            if (noticia != null)
            {
                _context.Noticias.Remove(noticia);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NoticiaExists(int id)
        {
          return _context.Noticias.Any(e => e.IdNoticia == id);
        }
    }
}
