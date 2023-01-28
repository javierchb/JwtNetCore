using Dapper;
using System.Data;
using ApiDAO.Models;
using ApiDAO.Connection;
namespace ApiDAO.DAO
{
    public class DAOEmployee : IDAOEmployee
    {
        private IConnectionDB _connection;
        public DAOEmployee(IConnectionDB connection) 
        {
            _connection = connection;
        }
        public List<Employee> GetEmployees()
        { 
            List<Employee> Response = new List<Employee>();
            using (var connection = _connection.GetConnection)
            {
                var Query = "SELECT * FROM [dbo].Employee";
                var QueryResult = connection.Query<Employee>(Query, commandType: CommandType.Text);
                Response = QueryResult.ToList();
            }
            return Response;        
        }
    }
}
