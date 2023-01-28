using ApiDAO.Connection;
using Dapper;
using System.Data;

namespace ApiDAO.DAO
{
    public class DAOLogin : IDAOLogin
    {
        private readonly IConnectionDB _connection;
        public DAOLogin(IConnectionDB connection)
        {
            _connection = connection;
        }
        public void InsertLogin(int UserId, string Username, string Email, string Token, DateTime Today)
        {
            using (var connection = _connection.GetConnection)
            {
                var Query = "" +                    
                    "INSERT INTO [dbo].UserLogin (UserId, UserName, UserEmail, AccessToken, SesionDate) " +
                    "VALUES (@InId, @InUsername, @InEmail, @InToken, @InDate)";
                var ResultQuery = connection.QueryFirstOrDefault<int>(Query, new { InId = UserId, InUsername = Username, InEmail = Email, InToken = Token, InDate = Today }, commandType: CommandType.Text);
            }
        }

        public void UpdateLogin(int UserId, string Username, string Email, string Token, DateTime Today)
        {
            using (var connection = _connection.GetConnection)
            {
                var Query = "" +
                    "UPDATE [dbo].UserLogin " +
                    "SET UserName = @InUserName, " +
                    "UserEmail = @InEmail, " +
                    "AccessToken = @InToken, " +
                    "SesionDate = @InDate " +
                    "WHERE UserId = @InUserId";
                var ResultQuery = connection.QueryFirstOrDefault<int>(Query, new { InUserName = Username, InEmail = Email, InToken = Token, InDate = Today, InUserId = UserId }, commandType: CommandType.Text);
            }
        }

        public int GetUserLogin(int UserId)
        {
            int Response = 0;
            using (var connection = _connection.GetConnection)
            {
                var Query = "" +
                    "SELECT UserId FROM [dbo].UserLogin " +
                    "WHERE UserId = @InUserId";
                var ResultQuery = connection.QueryFirstOrDefault<int>(Query, new { InUserId = UserId }, commandType: CommandType.Text);
                Response = ResultQuery;
            }
            return Response;
        }

        public void RemoveToken(string Email)
        {
            using (var connection = _connection.GetConnection)
            {
                var Query = "" +
                    "UPDATE [dbo].UserLogin " +
                    "SET AccessToken = @InToken " +
                    "WHERE UserEmail = @InEmail";
                var ResultQuery = connection.QueryFirstOrDefault<int>(Query, new { InToken = "", InEmail = Email }, commandType: CommandType.Text);
            }            
        }
    }
}
