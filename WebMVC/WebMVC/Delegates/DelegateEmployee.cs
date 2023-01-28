using WebMVC.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using Microsoft.Extensions.Caching.Memory;

namespace WebMVC.Delegates
{
    public class DelegateEmployee
    {
        private readonly IMemoryCache _memoryCache;
        private readonly IConfiguration _configuration;
        private readonly HttpClientHandler _httpHandler;
        public DelegateEmployee(IMemoryCache memoryCache, IConfiguration configuration)
        {
            _memoryCache = memoryCache;
            _configuration = configuration;
            _httpHandler = new HttpClientHandler();
            _httpHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
        }

        private string GetUserTokenInCache()
        {
            string Token = _memoryCache.Get<string>("UserToken");
            return Token;
        }

        public async Task<List<Employee>> GetEmployees()
        {
            List<Employee> Response = null;
            try
            {
                string url = "https://localhost:7277/Employee/GetEmployees";
                HttpClient client = new HttpClient(_httpHandler);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GetUserTokenInCache());
                var resp = client.GetStringAsync(url);
                string getResp = await resp;
                if (!String.IsNullOrWhiteSpace(getResp))
                {
                    Response = JsonConvert.DeserializeObject<List<Employee>>(getResp);
                }
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);              
            }
            return Response;
        }
        
    }
}
