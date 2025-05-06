using NutriFitApp.Shared.Models;
using Microsoft.Maui.Storage;
using System.IdentityModel.Tokens.Jwt;
using System.Windows.Input;
using Microsoft.Maui.Controls;

namespace NutriFitApp.Mobile.ViewModels;

public class DashboardViewModel
{
    public string Nombre { get; set; } = "Usuario";

    public ICommand LogoutCommand { get; }

    public DashboardViewModel()
    {
        LogoutCommand = new Command(Logout);
        CargarNombreDesdeToken();
    }

    private void CargarNombreDesdeToken()
    {
        var token = Preferences.Get("access_token", null);
        if (string.IsNullOrEmpty(token))
            return;

        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadJwtToken(token);
        var claim = jwtToken.Claims.FirstOrDefault(c => c.Type == "Nombre");

        if (claim != null)
            Nombre = claim.Value;
    }

    private void Logout()
    {
        Preferences.Clear();
        Shell.Current.GoToAsync("//Login");
    }
}
