using Microsoft.AspNetCore.Mvc;

namespace exercise.Controllers
{
    public class LoginController : Controller
    {
        public async Task<IActionResult> IndexAsync()
        {

            if (Request.Method == "POST") {
                /*
                 * form submission, validate the credentials
                 */
                await Authenticate();
            }
            
            return View();
        }

        public async Task Authenticate() {
            HttpClient client = new ();
            /*
             * set the payload from the form submitted
             */
            var user = new { 
                username = Request.Form["username"].ToString(),
                password = Request.Form["password"].ToString()
            };

            /*
             * authentication
             */
            HttpResponseMessage? response = await client.PostAsJsonAsync("https://netzwelt-devtest.azurewebsites.net/Account/SignIn", user);

            /*
             * we only want to redirect to /, /home, /home/index if successful authentication
             */
            if (response.IsSuccessStatusCode) {
                /*
                 * we set the cookie to expire within 1 seconds
                 * more than enough time for the redirect to happen
                 */
                CookieOptions cookieOptions = new();
                cookieOptions.Expires = new DateTimeOffset(DateTime.Now.AddSeconds(1));
                Response.Cookies.Append("isAuthenticated", "true", cookieOptions);
                Response.Redirect("/home/index");
            }

            /*
             * no redirect will show the authentication error message
             */
            ViewBag.AuthMessage = "Invalid username or password";
        }
    }
}