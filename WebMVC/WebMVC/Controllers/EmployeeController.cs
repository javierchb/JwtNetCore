using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using WebMVC.UIModels;
using WebMVC.Delegates;
namespace WebMVC.Controllers
{
    public class EmployeeController : Controller
    {        
        private readonly DelegateEmployee _delegateEmployee;
        private readonly IMemoryCache _memoryCache;
        private UIUser _userUI = new UIUser();
        public EmployeeController(IMemoryCache memoryCache, IConfiguration configuration)
        { 
            _memoryCache = memoryCache;
            _delegateEmployee = new DelegateEmployee(memoryCache, configuration);
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

        [HttpGet("IndexEmployees")]
        public async Task<IActionResult> IndexEmployees()
        {
            GetCacheLogin();
            _userUI.ViewEmployees.Employees = await _delegateEmployee.GetEmployees();
            return View(_userUI);
        }


    }
}
