using Microsoft.AspNetCore.Mvc;

namespace bitirmee.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Giris(string username, string pass)
        {
            
            if (username == "admin" && pass == "admin")
            {
                
                return Redirect("/Admin/Index");
            }
            else
            {
                ViewBag.HataMesaji = "Kullanıcı adı veya şifre yanlış.";
                return View("Login");
            }
        }
    }
}
