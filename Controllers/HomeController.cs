using Microsoft.AspNetCore.Mvc;

namespace exercise.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {
        }

        public async Task<IActionResult> IndexAsync()
        {
            /*
             * initialize to redirect to the login page 
             */
            Boolean redirect = true;
            foreach (KeyValuePair<string, string> cookie in Request.Cookies) 
            {
                /*
                 * reset the redirect to false if we see the cookie with the corresponding
                 */
                if (cookie.Key == "isAuthenticated" && cookie.Value == "true") 
                { 
                    redirect = false;
                    /*
                     * we make sure we clear the cookie 
                     * refreshing the page will redirect back to login
                     */
                    Response.Cookies.Delete(cookie.Key);
                    break;
                }
            }

            if (redirect)
            {
                Response.Redirect("/account/login");
            }
            else 
            {
                await GetData();
            }            

            return View();
        }

        public async Task GetData()
        {
            HttpClient client = new();
            /*
             * get data
             */
            HttpResponseMessage? response = await client.GetAsync("https://netzwelt-devtest.azurewebsites.net/Territories/All");

            if (response.IsSuccessStatusCode)
            {
                string jsonDataString = await response.Content.ReadAsStringAsync();
                ViewBag.Data = jsonDataString;
            }
            else {
                ViewBag.AuthMessage = "Something went wrong";
            }
        }
    }
}