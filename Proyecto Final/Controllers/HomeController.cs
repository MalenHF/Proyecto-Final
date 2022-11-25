using Microsoft.AspNetCore.Mvc;
using Proyecto_Final.Models;
using Proyecto_Final.Models.dbModels;
using Proyecto_Final.ViewModels;
using System.Diagnostics;


namespace Proyecto_Final.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ProyectoFinalContext _context;

        public HomeController(ILogger<HomeController> logger, ProyectoFinalContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            IndexViewModel ivm = new IndexViewModel();
            ivm.lstNoticia = _context.Noticias.OrderByDescending(x => x.FechaNoticia).Take(5).ToList();
            ivm.lstEventos = _context.Eventos.OrderByDescending(x => x.FechaEvento).Take(5).ToList();
           
            return View(ivm);
        }
            
        public IActionResult Informacion()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}