using ApiDAO.DAO;
using ApiDAO.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
namespace ApiDAO.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IDAOUser _daoUser;
        public UserController(IDAOUser daoUser)
        {
            _daoUser = daoUser;
        }
        [HttpGet("GetUsers")]
        public List<User> GetUsers() 
        {
            List<User> Response = new List<User>();
            Response = _daoUser.GetUsers();
            return Response;
        }
    }
}
