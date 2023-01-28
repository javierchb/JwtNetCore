using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Diagnostics;
using WebMVC.Models;
using WebMVC.UIModels;

namespace WebMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMemoryCache _memoryCache;
        private UIUser _userUI = new UIUser();
        public HomeController(ILogger<HomeController> logger, IMemoryCache memoryCache)
        {
            _logger = logger;
            _memoryCache = memoryCache;
        }

        private void GetCacheLogin()
        {
            string Username = _memoryCache.Get<string>("Username");
            if (!String.IsNullOrEmpty(Username))
            {
                _userUI.Username = Username;
                _userUI.InSesion = 1;
            }
        }

        public IActionResult Index()
        {
            GetCacheLogin();          
            return View(_userUI);
        }

        public IActionResult Privacy()
        {
            GetCacheLogin();
            return View(_userUI);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}