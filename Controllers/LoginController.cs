using exercise.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace exercise.Controllers
{
    public class LoginController : Controller
    {
        public LoginController()
        {
            //todo
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}