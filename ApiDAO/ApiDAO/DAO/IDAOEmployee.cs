using ApiDAO.Models;
namespace ApiDAO.DAO
{
    public interface IDAOEmployee
    {
        /// <summary>
        /// Get all employees.
        /// </summary>
        /// <returns></returns>
        List<Employee> GetEmployees();
    }
}
