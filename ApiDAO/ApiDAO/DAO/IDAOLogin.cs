namespace ApiDAO.DAO
{
    public interface IDAOLogin
    {
        void InsertLogin(int UserId, string Username, string Email, string Token, DateTime Today);
        void UpdateLogin(int UserId, string Username, string Email, string Token, DateTime Today);
        void RemoveToken(string Email);
        int GetUserLogin(int UserId);
    }
}
