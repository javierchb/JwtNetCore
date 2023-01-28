namespace WebMVC.Models
{
    /// <summary>
    /// Class LogIn.
    /// </summary>
    public class LogIn
    {
        /// <summary>
        /// Constructor LogIn.
        /// </summary>
        public LogIn()
        {
            Email = "";
            Username = "";
            Password = "";
        }
        public string Email { get; set; }
        public string Username { get; set; }        
        public string Password { get; set; }
    }
}
