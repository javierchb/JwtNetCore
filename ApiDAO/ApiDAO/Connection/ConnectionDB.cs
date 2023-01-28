using System.Data;
using Microsoft.Data.SqlClient;
namespace ApiDAO.Connection
{
    public class ConnectionDB : IConnectionDB
    {
        private readonly IConfiguration _config;
        public ConnectionDB(IConfiguration config)
        { 
            _config = config;
        }
        public IDbConnection GetConnection
        {
            get
            {
                var sqlConnection = new SqlConnection();
                if (sqlConnection != null)
                {
                    sqlConnection.ConnectionString = _config["ConnectionString"];
                    sqlConnection.Open();
                    return sqlConnection;
                }
                return null;
            }
        }
    }
}
