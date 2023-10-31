using CrudUsingADO.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CrudUsingADO.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        IConfiguration configuration;
        UsersDAL dal;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            this.configuration = configuration;
            dal=new UsersDAL(this.configuration);
        }

        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Privacy()
        {
            return View();
        }

       
        //Login & Registration

        [HttpGet]
            public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(Users users)
        {
            try
            {
                int result = dal.Register(users);
                if (result >= 1)
                {
                    return RedirectToAction("Login");
                }
                else
                {
                    ViewBag.Error = "Somethig went wrong";
                    return View();
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }

            
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(Users users)
        {
            try
            {
                int result = dal.Login(users);
                if (result >= 1)
                {
                    return RedirectToAction("Index", "Book");
                }
                else
                {
                    ViewBag.Error = "User name or Pasword is Wrong...";
                    return View();
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }

            
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}