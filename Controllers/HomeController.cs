using exercise.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace exercise.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {

        }

        public IActionResult Index()
        {
            return View();
        }
    }
}