using Microsoft.AspNetCore.Mvc;

namespace Proyecto_Final.Controllers
{
    public class EventosYNoticiasController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
