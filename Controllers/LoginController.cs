using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net.Http;
using System.Text.Json;

namespace exercise.Controllers
{
    public class LoginController : Controller
    {
        
        public async Task<IActionResult> IndexAsync()
        {

            if (Request.Method == "POST") {
                Debug.WriteLine("Authenticate the credentials");
                await Authenticate();
            }
            
            return View();
        }

        public async Task Authenticate() {
            HttpClient client = new ();
            var user = new { 
                username = Request.Form["username"].ToString(),
                password = Request.Form["password"].ToString()
            };

            HttpResponseMessage? response = await client.PostAsJsonAsync("https://netzwelt-devtest.azurewebsites.net/Account/SignIn", user);

            if (response.IsSuccessStatusCode) {
                Response.Redirect("/home/index");
            }

            ViewBag.AuthMessage = "Invalid username or password";
        }
    }
}