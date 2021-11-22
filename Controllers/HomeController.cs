using Microsoft.AspNetCore.Mvc;

namespace exercise.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {
        }

        public IActionResult Index()
        {
            /*
             * initialize to redirect to the login page 
             */
            Boolean redirect = true;
            foreach (KeyValuePair<string, string> cookie in Request.Cookies) {
                /*
                 * reset the redirect to false if we see the cookie with the corresponding
                 */
                if (cookie.Key == "isAuthenticated" && cookie.Value == "true") { 
                    redirect = false;
                    /*
                     * we make sure we clear the cookie 
                     * refreshing the page will redirect back to login
                     */
                    Response.Cookies.Delete(cookie.Key);
                    break;
                }
            }

            if (redirect) {
                Response.Redirect("/account/login");
            }



            return View();
        }
    }
}