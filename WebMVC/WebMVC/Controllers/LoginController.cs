using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebMVC.UIModels;
using WebMVC.Models;
using WebMVC.Delegates;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json.Linq;

namespace WebMVC.Controllers
{
    
    public class LoginController : Controller
    {
        private readonly IMemoryCache _memoryCache;
        private readonly DelegateLogin _delegateLogin;
        private UIUser _userUI = new UIUser();
        public LoginController(IMemoryCache memoryCache, IConfiguration configuration)
        {
            _memoryCache = memoryCache;
            _delegateLogin = new DelegateLogin(configuration);            
        }

        private void SetLoginInCacheMemory(string Email, string Token)
        {
            var cacheOptions = new MemoryCacheEntryOptions()
            .SetSlidingExpiration(TimeSpan.FromMinutes(2));
            _memoryCache.Set("Username", Email, cacheOptions);
            _memoryCache.Set("UserToken", Token, cacheOptions);
        }

        /// <summary>
        /// View system Log In.
        /// </summary>
        /// <returns>
        /// Return view Log In.
        /// </returns>
        public IActionResult Index()
        {            
            return View(_userUI);        
        }
        /// <summary>
        /// User login to system.
        /// </summary>
        /// <param name="Params"></param>
        /// <returns>Return to principal view with user authenticated.</returns>
        [HttpPost("UserLogIn")]
        public async Task<IActionResult> UserLogIn(LogIn Params)
        {            
            string token = await _delegateLogin.GetTokenLogIn(Params);
            // If token is generated, redirect to home page.
            if (!String.IsNullOrEmpty(token))
            {
                if (token == "error")
                {
                    ViewData["TitleModalError"] = "System error";
                    ViewData["MsgModalError"] = "Imposible to login.";
                    ViewBag.ModalError = 1;
                    return View("Index", _userUI);
                }
                SetLoginInCacheMemory(Params.Email, token);
                ViewData["TitleModalSuccess"] = "Log In Successfull";
                ViewData["MsgModalSuccess"] = "Welcome " + Params.Email + "!";
                _userUI.Username = Params.Email;
                _userUI.InSesion = 1;                
                ViewBag.ModalSuccess = 1;
                return View("~/Views/Home/Index.cshtml", _userUI);                
            }
            else
            {
                ViewData["TitleModalError"] = "Log In Error";
                ViewData["MsgModalError"] = "Invalid credentials.";
                ViewBag.ModalError = 1;
                return View("Index", _userUI);
            }                         
        }

        /// <summary>
        /// User logout system.
        /// </summary>
        /// <param name="Params"></param>
        /// <returns></returns>
        [HttpPost("LogOut")]
        public async Task<IActionResult> LogOut(LogIn Params)
        {
            _memoryCache.Remove("Username");
            _memoryCache.Remove("UserToken");
            _userUI = new UIUser();
            // Remove Token from database.
            return View("Index", _userUI);
        }
    }
}
