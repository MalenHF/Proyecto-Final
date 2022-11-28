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
using Proyecto_Final.ViewModels;

namespace Proyecto_Final.Controllers
{
    public class PublicacionesController : Controller
    {
        private readonly ProyectoFinalContext _context;
        private readonly UserManager<AplicationUser> _userManager;

        public PublicacionesController(ProyectoFinalContext context, UserManager<AplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Publicaciones
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var proyectoFinalContext = _context.Publicaciones.Include(p => p.IdUsuarioNavigation).OrderByDescending(x => x.FechaPublicacion);
            return View(await proyectoFinalContext.ToListAsync());
        }


        public IActionResult Galeria()
        {
            IndexViewModel ivm = new IndexViewModel();
            ivm.lstPublicaciones = _context.Publicaciones.Where(x => x.Estatus == true).OrderByDescending(x => x.FechaPublicacion).ToList();            
            return View(ivm);
        }
        
        // GET: Publicaciones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Publicaciones == null)
            {
                return NotFound();
            }

            var publicacione = await _context.Publicaciones
                .Include(p => p.IdUsuarioNavigation)
                .FirstOrDefaultAsync(m => m.PublicacionId == id);
            if (publicacione == null)
            {
                return NotFound();
            }

            return View(publicacione);
        }



        // GET: Publicaciones/Create
        [Authorize(Roles = null)]
        public IActionResult Create()
        {
            ViewData["IdUsuario"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Publicaciones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PublicacioneCreateDTO publicacione)
        {
            if (ModelState.IsValid)
            {
                AplicationUser user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    return BadRequest();
                }
                Publicacione p = new Publicacione
                {
                    PublicacionId = publicacione.PublicacionId,
                    IdUsuario = user.Id,
                    FotoPath = publicacione.FotoPath,
                    Titulo = publicacione.Titulo,
                    Estatus = publicacione.Estatus,
                    FechaPublicacion = DateTime.Now
                };
                _context.Add(p);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdUsuario"] = new SelectList(_context.Users, "Id", "Id", publicacione.IdUsuario);
            return View(publicacione);
        }

        public async Task<IActionResult> CreateUser(PublicacioneCreateDTO publicacione)
        {
            if (ModelState.IsValid)
            {
                AplicationUser user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    return BadRequest();
                }
                Publicacione p = new Publicacione
                {
                    PublicacionId = publicacione.PublicacionId,
                    IdUsuario = user.Id,
                    FotoPath = publicacione.FotoPath,
                    Titulo = publicacione.Titulo,
                    Estatus = publicacione.Estatus,
                    FechaPublicacion = DateTime.Now
                };
                _context.Add(p);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Galeria));
            }
            ViewData["IdUsuario"] = new SelectList(_context.Users, "Id", "Id", publicacione.IdUsuario);
            return View(publicacione);
        }

        // GET: Publicaciones/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Publicaciones == null)
            {
                return NotFound();
            }

            var publicacione = await _context.Publicaciones.FindAsync(id);
            if (publicacione == null)
            {
                return NotFound();
            }
            PublicacioneUpdatesDTO pud = new PublicacioneUpdatesDTO();
            pud.PublicacionId= publicacione.PublicacionId;
            pud.Titulo= publicacione.Titulo;
            pud.Estatus = publicacione.Estatus;
            pud.FechaPublicacion = publicacione.FechaPublicacion;
            pud.FotoPath = publicacione.FotoPath;

            ViewData["IdUsuario"] = new SelectList(_context.Users, "Id", "Id", publicacione.IdUsuario);
            return View(pud);
        }
       

        // POST: Publicaciones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, PublicacioneUpdatesDTO publicacione)
        {
            if (id != publicacione.PublicacionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Publicacione pub = new Publicacione
                    {
                        PublicacionId = publicacione.PublicacionId,
                        IdUsuario = publicacione.IdUsuario,
                        Titulo = publicacione.Titulo,                        
                        Estatus = publicacione.Estatus,
                        FechaPublicacion = DateTime.Now,
                        FotoPath = publicacione.FotoPath
                    };
                    _context.Update(pub);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PublicacioneExists(publicacione.PublicacionId))
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
            ViewData["IdUsuario"] = new SelectList(_context.Users, "Id", "Id", publicacione.IdUsuario);
            return View(publicacione);
        }

        // GET: Publicaciones/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Publicaciones == null)
            {
                return NotFound();
            }

            var publicacione = await _context.Publicaciones
                .Include(p => p.IdUsuarioNavigation)
                .FirstOrDefaultAsync(m => m.PublicacionId == id);
            if (publicacione == null)
            {
                return NotFound();
            }

            return View(publicacione);
        }

        // POST: Publicaciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Publicaciones == null)
            {
                return Problem("Entity set 'ProyectoFinalContext.Publicaciones'  is null.");
            }
            var publicacione = await _context.Publicaciones.FindAsync(id);
            if (publicacione != null)
            {
                _context.Publicaciones.Remove(publicacione);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PublicacioneExists(int id)
        {
          return _context.Publicaciones.Any(e => e.PublicacionId == id);
        }
    }
}
