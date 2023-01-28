using Dapper;
using System.Data;
using ApiDAO.Models;
using ApiDAO.Connection;
using System.Reflection.Metadata.Ecma335;

namespace ApiDAO.DAO
{
    public class DAOUser : IDAOUser
    {
        private readonly IConnectionDB _connection;
        public DAOUser(IConnectionDB connection)
        { 
            _connection = connection;
        }
        public List<User> GetUsers()
        { 
            List<User> Response = new List<User>();
            using (var connection = _connection.GetConnection)
            {
                var Query = "SELECT * FROM [dbo].UserInfo";
                var QueryResult = connection.Query<User>(Query, commandType: CommandType.Text);
                Response = QueryResult.ToList();
            }
            return Response;
        }
        public User GetUserInfo(string email, string pass)
        {
            User Response = new User();
            using (var connection = _connection.GetConnection)
            {
                var Query = "" +
                    "SELECT * FROM [dbo].UserInfo " +
                    "WHERE [Email]=@inEmail " +
                    "AND   [Password]=@inPassword";
                var QueryResult = connection.QueryFirstOrDefault<User>(Query, new { inEmail = email, inPassword = pass }, commandType: CommandType.Text);
                Response = QueryResult;
            }                
            return Response;    
        }
    }
}
