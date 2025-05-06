using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using NutriFitApp.Shared.DTOs;
using Microsoft.Maui.Storage;
using NutriFitApp.Shared.Models;
using NutriFitApp.Shared.Interfaces;

namespace NutriFitApp.Mobile.Services;

public class AuthService : IAuthService
{
    private readonly HttpClient _httpClient;

    public AuthService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri("https://TU_API_URL/api/");
    }

    public async Task<bool> LoginAsync(LoginDTO loginDto)
    {
        var response = await _httpClient.PostAsJsonAsync("auth/login", loginDto);

        if (response.IsSuccessStatusCode)
        {
            var token = await response.Content.ReadFromJsonAsync<TokenDTO>();
            if (token != null)
            {
                Preferences.Set("access_token", token.Token);
                Preferences.Set("expires", token.Expiration.ToString());
                return true;
            }
        }

        return false;
    }

    public async Task<bool> RegisterAsync(Usuario usuario)
    {
        var response = await _httpClient.PostAsJsonAsync("auth/register", usuario);

        if (response.IsSuccessStatusCode)
        {
            var token = await response.Content.ReadFromJsonAsync<TokenDTO>();
            if (token != null)
            {
                Preferences.Set("access_token", token.Token);
                Preferences.Set("expires", token.Expiration.ToString());
                return true;
            }
        }

        return false;
    }

    public string? GetToken() => Preferences.Get("access_token", null);

    public void Logout()
    {
        Preferences.Remove("access_token");
        Preferences.Remove("expires");
    }
}
