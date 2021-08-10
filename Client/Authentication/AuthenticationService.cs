using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components.Authorization;

using Blazored.LocalStorage;
using helping_hand.Models.Dto;
using System.Net.Http.Json;
using System.Net;
using helping_hand.Models;
using Microsoft.AspNetCore.WebUtilities;

namespace helping_hand.Client.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly HttpClient _httpClient;
        private readonly AuthenticationStateProvider _authStateProvider;
        private readonly ILocalStorageService _localStorage;

        public AuthenticationService(HttpClient httpClient,
            AuthenticationStateProvider authStateProvider,
            ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _authStateProvider = authStateProvider;
            _localStorage = localStorage;
        }

        public async Task<Error> ConfirmEmail(string token, string email)
        {
            var param = new Dictionary<string, string> { ["token"] = token, ["email"] = email };
            var confirmUrl = QueryHelpers.AddQueryString("/api/auth/confirm-email", param);
            var result = await _httpClient.GetAsync(confirmUrl);

            if (result.StatusCode == HttpStatusCode.OK)
            {
                return null;
            }

            var content = await result.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<Error>(content,
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task<TokenDto> Login(LoginDto userForLogin)
        {
            var authResult = await _httpClient.PostAsJsonAsync("api/auth/login", userForLogin);
            var authContent = await authResult.Content.ReadAsStringAsync();

            if (!authResult.IsSuccessStatusCode)
            {
                if (authResult.StatusCode == HttpStatusCode.Unauthorized)
                {
                    var error = JsonSerializer.Deserialize<Error>(authContent,
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                    return new TokenDto { Error = error.Title };
                }

                return null;
            }

            var result = JsonSerializer.Deserialize<TokenDto>(
                authContent,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            await _localStorage.SetItemAsync("authToken", result.Token);

            ((AuthStateProvider)_authStateProvider).NotifyUserAuthentication(result.Token);

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", result.Token);

            return result;
        }

        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync("authToken");
            ((AuthStateProvider)_authStateProvider).NotifyUserLogout();
            _httpClient.DefaultRequestHeaders.Authorization = null;
        }
    }
}
