using Microsoft.AspNetCore.Mvc;

namespace Proyecto_Final.Controllers
{
    public class VotacionesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
