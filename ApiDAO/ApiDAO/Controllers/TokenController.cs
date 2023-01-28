using ApiDAO.DAO;
using ApiDAO.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ApiDAO.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TokenController : ControllerBase
    {        
        private readonly IDAOUser _userDAO;
        private readonly IDAOLogin _loginDAO;
        private readonly IConfiguration _configuration;        
        public TokenController(IDAOUser userDAO, IDAOLogin loginDAO, IConfiguration configuration)
        { 
            _userDAO = userDAO;
            _loginDAO = loginDAO;
            _configuration = configuration;
        }
        /// <summary>
        /// User authentication.
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns>
        /// Return token authorization.
        /// </returns>
        [HttpPost("AuthenticateUser")]
        public string AuthenticateUser([FromBody] LogIn parameters)
        {            
            User UserInfo = _userDAO.GetUserInfo(parameters.Email, parameters.Password);
            if (UserInfo is not null)
            {
                // claims.
                var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("UserId", UserInfo.UserId.ToString()),
                        new Claim("DisplayName", UserInfo.DisplayName),
                        new Claim("UserName", UserInfo.UserName),
                        new Claim("Email", UserInfo.Email)
                };
                // key.
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                // sign.
                var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    _configuration["Jwt:Issuer"],
                    _configuration["Jwt:Audience"],
                    claims,
                    expires: DateTime.UtcNow.AddMinutes(10),
                    signingCredentials: signIn);
                // token.                
                string respTkn = new JwtSecurityTokenHandler().WriteToken(token);
                InsertUpdateAccess(UserInfo.UserId, UserInfo.UserName, UserInfo.Email, respTkn, DateTime.Today);
                return respTkn;
            }            
            return "";
        }
        /// <summary>
        /// Insert or update user data and token after being authorized.
        /// </summary>
        private void InsertUpdateAccess(int UserId, string Username, string Email, string Token, DateTime Today)
        {
            int userLogin = _loginDAO.GetUserLogin(UserId);
            if (userLogin != 0)
            {
                _loginDAO.UpdateLogin(UserId, Username, Email, Token, Today);
            }
            else
            {
                _loginDAO.InsertLogin(UserId, Username, Email, Token, Today);                
            }
        }        
    }
}
