using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using WebMVC.Models;
using WebMVC.UIModels;

namespace WebMVC.Delegates
{
    /// <summary>
    /// Class DelegateLogin.
    /// </summary>
    public class DelegateLogin
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClientHandler _httpHandler;
        /// <summary>
        /// Constructor DelegateLogin.
        /// </summary>
        /// <param name="configuration">Injection configuration.</param>
        public DelegateLogin(IConfiguration configuration)        
        {
            _configuration = configuration;
            _httpHandler = new HttpClientHandler();
            _httpHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };            
        }
        /// <summary>
        /// Get token login.
        /// </summary>
        /// <param name="parameters">Login parameters.</param>
        /// <returns>
        /// Return token.
        /// </returns>
        public async Task<string> GetTokenLogIn(LogIn parameters)
        {            
            string token = "";
            try 
            {
                string url = "https://localhost:7277/Token/AuthenticateUser";
                var body = JsonConvert.SerializeObject(parameters);
                HttpClient client = new HttpClient(_httpHandler);
                //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "");            
                var resp = await client.PostAsync(url, new StringContent(body, Encoding.UTF8, "application/json"));
                string getResp = await resp.Content.ReadAsStringAsync();
                if (!String.IsNullOrEmpty(getResp))
                {
                    token = getResp;
                }
            }
            catch (Exception ex) 
            {
                string msg = ex.Message;
                token = "error";            
            }

            return token;
        }

    }
}
