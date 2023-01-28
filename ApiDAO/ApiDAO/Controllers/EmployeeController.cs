using ApiDAO.DAO;
using ApiDAO.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace ApiDAO.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IDAOEmployee _daoEmployee;
        public EmployeeController(IDAOEmployee daoEmployee) 
        {
            _daoEmployee = daoEmployee;
        }
        /// <summary>
        /// Get all employees.
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetEmployees")]
        public List<Employee> GetEmployees( )
        { 
            List<Employee> Response = new List<Employee>();
            Response = _daoEmployee.GetEmployees();
            return Response;
        }
    }
}
