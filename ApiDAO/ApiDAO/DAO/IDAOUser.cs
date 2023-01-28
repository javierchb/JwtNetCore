using ApiDAO.Models;
namespace ApiDAO.DAO
{
    public interface IDAOUser
    {
        /// <summary>
        /// Get all users info.
        /// </summary>
        /// <returns></returns>
        List<User> GetUsers();
        /// <summary>
        /// Get user info.
        /// </summary>
        /// <param name="email">Email.</param>
        /// <param name="pass">Password.</param>
        /// <returns></returns>
        User GetUserInfo(string email, string pass);
    }
}
