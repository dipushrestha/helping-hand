using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

using Blazored.LocalStorage;
using helping_hand.Models;
using helping_hand.Models.Dto;

namespace helping_hand.Client.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly HttpClient _httpClient;
        private readonly AuthStateProvider _authStateProvider;
        private readonly ILocalStorageService _localStorage;

        public AuthenticationService(HttpClient httpClient,
            AuthStateProvider authStateProvider,
            ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _authStateProvider = authStateProvider;
            _localStorage = localStorage;
        }

        public async Task<TokenDto> Login(LoginDto userForLogin)
        {
            var data = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("grant_type", "password"),
                new KeyValuePair<string, string>("username", userForLogin.UserName),
                new KeyValuePair<string, string>("password", userForLogin.Password)
            });

            var authResult = await _httpClient.PostAsync("api/auth/login", data);
            var authContent = await authResult.Content.ReadAsStringAsync();

            if (!authResult.IsSuccessStatusCode)
            {
                return null;
            }

            var result = JsonSerializer.Deserialize<TokenDto>(
                authContent,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            await _localStorage.SetItemAsync("authToken", result.Token);

            _authStateProvider.NotifyUserAuthentication(result.Token);

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", result.Token);

            return result;
        }

        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync("authToken");
            _authStateProvider.NotifyUserLogout();
            _httpClient.DefaultRequestHeaders.Authorization = null;
        }
    }
}
