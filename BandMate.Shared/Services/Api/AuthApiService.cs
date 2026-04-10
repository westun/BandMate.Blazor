using System.Net.Http.Json;

namespace BandMate.Shared.Services.Api
{
    public class AuthApiService : IAuthApiService
    {
        private readonly HttpClient httpClient;

        public AuthApiService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<bool> Login(string email, string password)
        {
            var response = await httpClient.PostAsJsonAsync("login?useCookies=true", new { email = email, password = password });

            return response.IsSuccessStatusCode;
        }
    }
}
