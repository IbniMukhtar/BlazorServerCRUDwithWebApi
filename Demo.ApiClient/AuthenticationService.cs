using Demo.ApiClient.Models;
using Demo.ApiClient.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;

namespace Demo.ApiClient
{
    public class AuthenticationService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthenticationService(HttpClient httpClient,  IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
        }

        public Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var userIdentity = _httpContextAccessor.HttpContext?.User.Identity;
            var user = userIdentity?.IsAuthenticated == true ?
                new ClaimsPrincipal(new ClaimsIdentity(userIdentity)) :
                new ClaimsPrincipal(new ClaimsIdentity());

            var authenticationStateTask = Task.FromResult(new AuthenticationState(user));
            return authenticationStateTask;
        }

        public async Task<IEnumerable<IdentityUser>> GetAllUsers()
        {
            var response = await _httpClient.GetAsync("/api/Auth/users");
            response.EnsureSuccessStatusCode(); // Throw exception if not successful

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var users = await response.Content.ReadFromJsonAsync<IEnumerable<IdentityUser>>(options);
            return users ?? Enumerable.Empty<IdentityUser>();
        }

        public async Task<IdentityUser?> Register(RegisterModel registerModel)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/Auth/register", registerModel);
            return await response.Content.ReadFromJsonAsync<IdentityUser>();
        }

        public async Task<IdentityUser?> Login(LoginModel loginModel)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/Auth/login", loginModel);

            if (response.IsSuccessStatusCode)
            {
                // Authentication succeeded, return the user
                return await response.Content.ReadFromJsonAsync<IdentityUser>();
            }
            else
            {
                // Authentication failed, return null
                return null;
            }
        }


        public void Dispose()
        {
            _httpClient.Dispose();
        }
    }
}
