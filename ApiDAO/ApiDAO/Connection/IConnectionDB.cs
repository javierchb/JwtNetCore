using System.Data;
namespace ApiDAO.Connection
{
    public interface IConnectionDB
    {
        public IDbConnection GetConnection { get; }
    }
}
